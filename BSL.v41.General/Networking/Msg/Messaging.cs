using System.Net.Sockets;
using BSL.v41.Logic.Environment.LaserMessage;
using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.Graphic;

namespace BSL.v41.General.Networking.Msg;

public class Messaging(
    Socket? userListener,
    Connection? connection,
    ServerConnection? serverConnection,
    bool connected = true)
{
    private int _proxySessionId;

    public bool IsConnected(int proxySessionId)
    {
        _proxySessionId = proxySessionId;
        return connected;
    }

    public bool SetIsConnected(bool value)
    {
        return connected = value;
    }

    public bool Disconnect()
    {
        return false;
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
        var position = connection!.MemoryStream!.Position;
        {
            connection.MemoryStream.Position = 0;
        }

        var headerBuffer = new byte[7];
        {
            _ = connection.MemoryStream.Read(headerBuffer, 0, 7);
        }

        var header = ReadHeader(headerBuffer);

        var type = header[0];
        var length = header[1];
        var version = header[2];

        var payload = new byte[length];

        if (connection.MemoryStream.Read(payload, 0, length) < length)
        {
            connection.MemoryStream.Position = position;
            return 0;
        }
        
        ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Receive,
            $"Message with type: {type} ({Helper.GetPacketNameByType(type)}) received!"); 

        HttpMessaging.ReadMessage(type, length, version, payload, serverConnection!, _proxySessionId);
        
        var all = connection.MemoryStream.ToArray();
        var buffer = all.Skip(length + 7).ToArray();

        connection.MemoryStream = new MemoryStream();
        connection.MemoryStream.Write(buffer, 0, buffer.Length);

        if (buffer.Length >= 7) NextMessage();
        return 0;
    }

    public void Send(PiranhaMessage message)
    {
        try
        {
            message.SetProxySessionId(_proxySessionId);

            HttpMessaging.WriteMessage(message, serverConnection!);

            if (message.GetServiceNodeType() != 4 && message.GetServiceNodeType() != 27)
                ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Send,
                    $"Message with type: {message.GetMessageType()} ({message.GetMessageTypeName()}) sent!");
        }
        catch (Exception)
        {
            // ignored.
        }
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
}