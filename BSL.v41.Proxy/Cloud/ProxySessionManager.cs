using System.Net.Sockets;
using BSL.v41.Logic.Environment.LaserMessage;
using BSL.v41.Proxy.Network;

namespace BSL.v41.Proxy.Cloud;

public static class ProxySessionManager
{
    private static readonly Dictionary<int, Socket> ProxySessionSeedSaverX = new();
    private static readonly Dictionary<Socket, int> ProxySessionSeedSaverY = new();
    private static readonly Dictionary<int, PiranhaMessage?> ProxySessionSeedSaverW = new();

    public static int ProxySessionSeedAuto { get; set; }

    public static int CreateNewProxySession(Socket socket)
    {
        ProxySessionSeedAuto++;

        ProxySessionSeedSaverX.TryAdd(ProxySessionSeedAuto, socket);
        ProxySessionSeedSaverY.TryAdd(socket, ProxySessionSeedAuto);
        ProxySessionSeedSaverW.TryAdd(ProxySessionSeedAuto, null);

        return ProxySessionSeedAuto;
    }

    public static Socket CreateNewMessageBySessionId(int sessionId, PiranhaMessage piranhaMessage)
    {
        ProxySessionSeedSaverW[sessionId] = piranhaMessage;
        return ProxySessionSeedSaverX[sessionId];
    }

    public static Socket DeleteMessageBySessionId(int sessionId)
    {
        ProxySessionSeedSaverW[sessionId] = null;
        return ProxySessionSeedSaverX[sessionId];
    }

    public static PiranhaMessage? GetMessageBySessionId(int sessionId)
    {
        return ProxySessionSeedSaverW[sessionId];
    }

    public static Socket WriteMessageBySessionId(int sessionId, PiranhaMessage piranhaMessage)
    {
        WriteMessage(piranhaMessage, ProxySessionSeedSaverX[sessionId]);
        return ProxySessionSeedSaverX[sessionId];
    }

    public static Socket WriteCustomMessageBySessionId(int sessionId, byte[] messageBytes)
    {
        WriteMessage(messageBytes, ProxySessionSeedSaverX[sessionId]);
        return ProxySessionSeedSaverX[sessionId];
    }

    public static void WriteMessageToProxySocket(int sessionId, PiranhaMessage message)
    {
        message.SetMessageVersion(sessionId);
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
            var buffer = stream.ToArray();
            {
                if (ProxyStaticalClient.ProxySocketTransportClient.OnSend(buffer) >= 0) return;
                var cts = new CancellationTokenSource();

                Task.Run(() =>
                {
                    try
                    {
                        if (ProxyStaticalClient.ProxySocketTransportClient.OnSend(buffer) >= 0)
                        {
                            cts.Cancel();
                        }
                        else
                        {
                            while (ProxyStaticalClient.ProxySocketTransportClient.OnSend(buffer) < 0) ;
                            cts.Cancel();
                        }
                    }
                    catch
                    {
                        while (ProxyStaticalClient.ProxySocketTransportClient.OnSend(buffer) < 0) ;
                        cts.Cancel();
                    }

                    cts.Token.ThrowIfCancellationRequested();
                }, cts.Token);
            }
        }
    }

    /// private sector. ///
    private static void WriteMessage(PiranhaMessage message, Socket socket)
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
                WriteMessage(buffer, socket);
            }
        }
    }

    private static void WriteMessage(byte[] messageBytes, Socket socket)
    {
        socket.BeginSend(messageBytes, 0, messageBytes.Length, SocketFlags.None, ProxySocketTransportListener.OnSend,
            socket);
    }
}