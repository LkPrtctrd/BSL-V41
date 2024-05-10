using System.Net.Sockets;
using BSL.v41.General.Networking.Msg;
using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.Graphic;

namespace BSL.v41.General.Networking;

public class Connection
{
    private Socket? _listener;
    private Messaging? _messaging;
    private Socket? _userListener;

    public byte[]? ByteBuffer { get; set; } = new byte[1024];
    public MemoryStream? MemoryStream { get; set; } = new();
    public Timer? Timer { get; set; }

    public Socket? GetListener()
    {
        return _listener;
    }

    public Socket? SetListener(Socket? socket)
    {
        return _listener = socket;
    }

    public ProtocolType GetInterfaceName()
    {
        return _listener!.ProtocolType;
    }

    public Socket? GetUserListener()
    {
        return _userListener;
    }

    public Socket? SetUserListener(Socket? socket)
    {
        return _userListener = socket;
    }

    public Messaging? GetMessaging()
    {
        return _messaging;
    }

    public Messaging SetMessaging(Messaging messaging)
    {
        return _messaging = messaging;
    }

    public void Close()
    {
        try
        {
            ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Tcp, "New user-disconnected! Information: " +
                                                                          $"server address: {Helper.GetIpFromDomain(GetListener()!.LocalEndPoint!.ToString()!)}; " +
                                                                          $"server port: {Helper.GetPortFromDomain(GetListener()!.LocalEndPoint!.ToString()!)}; " +
                                                                          $"client address: {Helper.GetIpBySocket(GetUserListener()!)}; " +
                                                                          $"client port: {Helper.GetPortBySocket(GetUserListener()!)}.");

            _userListener?.Close();
            Timer!.Dispose();
        }
        catch (Exception)
        {
            // ignored.
        }
    }
}