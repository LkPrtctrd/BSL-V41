using System.Net.Sockets;
using BSL.v41.Logic.Environment.LaserMessage;
using BSL.v41.Proxy.Cloud;
using BSL.v41.Proxy.Network;
using BSL.v41.Titan.Graphic;

namespace BSL.v41.Proxy.Networking;

public class ProxyTransportMessaging(
    Socket? userListener,
    ProxyTransportConnection? proxyTransportConnection,
    ProxyTransportLaserConnection? proxyTransportLaserConnection,
    int proxySessionId = 0)
{
    public bool Disconnect()
    {
        try
        {
            userListener!.Close();
        }
        catch
        {
            return false;
        }

        return true;
    }

    public int NextMessage()
    {
        var headerBuffer = new byte[7];
        {
            _ = proxyTransportConnection!.MemoryStream!.Position = 0;
            _ = proxyTransportConnection.MemoryStream.Read(headerBuffer, 0, 7);
        }

        ProxySessionManager.WriteCustomMessageBySessionId(ReadHeader(headerBuffer)[2],
            proxyTransportConnection.MemoryStream.ToArray());
        return 0;
    }

    public List<int> ReadHeader(byte[] headerBuffer)
    {
        var list = new List<int>();
        {
            list.Add((headerBuffer[0] << 8) | headerBuffer[1]);
            list.Add((headerBuffer[2] << 16) | (headerBuffer[3] << 8) | headerBuffer[4]);
            list.Add((headerBuffer[5] << 8) | headerBuffer[6]);
        }

        return list;
    }

    public static void WriteMessage(PiranhaMessage message, ProxyTransportLaserConnection serverConnection)
    {
        message.Encode();

        var payload = new byte[message.GetEncodingLength()];
        {
            Buffer.BlockCopy(message.GetMessageBytes(), 0, payload, 0, payload.Length);
        }

        var stream = new byte[payload.Length + 7];
        {
            stream[0] = (byte)(message.GetMessageType() >> 8);
            stream[1] = (byte)message.GetMessageType();
            stream[2] = (byte)(payload.Length >> 16);
            stream[3] = (byte)(payload.Length >> 8);
            stream[4] = (byte)payload.Length;
            stream[5] = (byte)(message.GetMessageVersion() >> 8);
            stream[6] = (byte)message.GetMessageVersion();
        }

        Buffer.BlockCopy(payload, 0, stream, 7, payload.Length);
        {
            var buffer = stream.Concat(new byte[] { 0xFF, 0xFF, 0x0, 0x0, 0x0, 0x0, 0x0 }).ToArray(); // pornography
            {
                serverConnection.GetProxyTransportConnection()!.GetUserListener()!.BeginSend(buffer, 0, buffer.Length,
                    SocketFlags.None,
                    ProxySocketTransportListener.OnSend,
                    serverConnection.GetProxyTransportConnection()!.GetUserListener()!);
            }
        }
    }

    public static void WriteMessage(PiranhaMessage message, Socket socket)
    {
        message.Encode();

        var payload = new byte[message.GetEncodingLength()];
        {
            Buffer.BlockCopy(message.GetMessageBytes(), 0, payload, 0, payload.Length);
        }

        var stream = new byte[payload.Length + 7];
        {
            stream[0] = (byte)(message.GetMessageType() >> 8);
            stream[1] = (byte)message.GetMessageType();
            stream[2] = (byte)(payload.Length >> 16);
            stream[3] = (byte)(payload.Length >> 8);
            stream[4] = (byte)payload.Length;
            stream[5] = (byte)(message.GetMessageVersion() >> 8);
            stream[6] = (byte)message.GetMessageVersion();
        }

        Buffer.BlockCopy(payload, 0, stream, 7, payload.Length);
        {
            var buffer = stream.Concat(new byte[] { 0xFF, 0xFF, 0x0, 0x0, 0x0, 0x0, 0x0 }).ToArray(); // pornography
            {
                socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, ProxySocketTransportListener.OnSend,
                    socket);
            }
        }
    }

    public void Send(PiranhaMessage message)
    {
        message.SetProxySessionId(proxySessionId);

        WriteMessage(message, proxyTransportLaserConnection!);
        {
            ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Send,
                $"Transport-message with type: {message.GetMessageType()} ({message.GetMessageTypeName()}) sent!");
        }
    }
}