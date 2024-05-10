using System.Net.Sockets;
using BSL.v41.General.Network;
using BSL.v41.Logic.Environment;
using BSL.v41.Logic.Environment.LaserMessage;
using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.Graphic;

namespace BSL.v41.General.Networking.Msg;

public static class HttpMessaging
{
    public static void ReadMessage(int type, int length, int version, byte[] payload, ServerConnection serverConnection,
        int proxySessionId)
    {
        var message = LogicLaserMessageFactory.CreateMessageByType(type);
        {
            if (message == null) return;
            {
                message.SetProxySessionId(proxySessionId);
            }
        }

        if (length > 0)
        {
            message.ByteStream.SetByteArray(payload, payload.Length);
            {
                message.Decode();
            }
        }

        var state = serverConnection.GetMessageManager()!.ReceiveMessage(message);

        if (state < 300 - 1)
            ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Info,
                $"Message with type: {type} ({Helper.GetPacketNameByType(type)}), " +
                $"was processed and returned this state: '{(state >= 1 ? "successfully" : "failed")}'. PSI: {proxySessionId}");
        // todo.
    }

    public static void WriteMessage(PiranhaMessage message, ServerConnection serverConnection)
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
                serverConnection.GetConnection()!.GetUserListener()!.BeginSend(buffer, 0, buffer.Length,
                    SocketFlags.None,
                    TcpLaserSocketListener.OnSend,
                    serverConnection.GetConnection()!.GetUserListener()!);
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
                socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, TcpLaserSocketListener.OnSend, socket);
            }
        }
    }
}