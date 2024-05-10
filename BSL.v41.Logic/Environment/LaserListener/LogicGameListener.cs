using System.Net.Sockets;
using BSL.v41.Logic.Environment.LaserMessage;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Mode;
using BSL.v41.Titan.Graphic;

namespace BSL.v41.Logic.Environment.LaserListener;

public class LogicGameListener(Socket socket, LogicHomeMode logicHomeMode)
{
    public void Send(PiranhaMessage message)
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

        try
        {
            Buffer.BlockCopy(payload, 0, stream, 7, payload.Length);
            {
                var buffer = stream.Concat(new byte[] { 0xFF, 0xFF, 0x0, 0x0, 0x0, 0x0, 0x0 }).ToArray();
                {
                    socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, OnSend, socket);
                }
            }
        }
        catch
        {
            // ignored.
        }

        ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Send,
            $"Message with type: {message.GetMessageType()} ({message.GetMessageTypeName()}) " +
            $"sent to account with id: {logicHomeMode.AccountModel.GetAccountId()}!");
    }

    public static void OnSend(IAsyncResult iAsyncResult)
    {
        try
        {
            ((Socket)iAsyncResult.AsyncState!).EndSend(iAsyncResult);
        }
        catch (Exception)
        {
            // ignored.
        }
    }

    public LogicHomeMode GetLogicHomeMode()
    {
        return logicHomeMode;
    }
}