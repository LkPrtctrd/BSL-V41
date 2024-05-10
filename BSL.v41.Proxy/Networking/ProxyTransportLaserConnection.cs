using System.Net.Sockets;
using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.Graphic;

namespace BSL.v41.Proxy.Networking;

public class ProxyTransportLaserConnection(ProxyTransportConnection connection)
{
    private readonly ProxyTransportConnection? _proxyTransportConnection = connection;
    private int _proxySessionId;
    private ProxyTransportMessaging? _proxyTransportMessaging;

    public void Initialize()
    {
        _proxyTransportMessaging = new ProxyTransportMessaging(_proxyTransportConnection!.GetUserListener(),
            _proxyTransportConnection, this, _proxySessionId);
        _proxyTransportConnection.SetProxyTransportMessaging(_proxyTransportMessaging);

        ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Tcp, "New user-connected! Information: " +
                                                                      $"server address: {Helper.GetIpFromDomain(_proxyTransportConnection?.GetListener()!.LocalEndPoint!.ToString()!)}; " +
                                                                      $"server port: {Helper.GetPortFromDomain(_proxyTransportConnection?.GetListener()!.LocalEndPoint!.ToString()!)}; " +
                                                                      $"client address: {Helper.GetIpBySocket(_proxyTransportConnection?.GetUserListener()!)}; " +
                                                                      $"client port: {Helper.GetPortBySocket(_proxyTransportConnection?.GetUserListener()!)}.");

        _proxySessionId = -1;
    }

    public void SetProxySessionId(int proxySessionId)
    {
        _proxySessionId = proxySessionId;
    }

    public ProxyTransportMessaging? GetProxyTransportMessaging(ProxyTransportMessaging? messaging = null!)
    {
        return messaging ?? _proxyTransportMessaging;
    }

    public ProxyTransportConnection? GetProxyTransportConnection(ProxyTransportConnection? connection = null!)
    {
        return connection ?? _proxyTransportConnection;
    }

    public string? GetServerTypeString()
    {
        return Enum.GetName(typeof(SocketType), _proxyTransportConnection!.GetListener()!.SocketType);
    }

    public bool Disconnect()
    {
        ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Tcp, "New user-transport-disconnected! Information: " +
                                                                      $"server address: {Helper.GetIpFromDomain(_proxyTransportConnection?.GetListener()!.LocalEndPoint!.ToString()!)}; " +
                                                                      $"server port: {Helper.GetPortFromDomain(_proxyTransportConnection?.GetListener()!.LocalEndPoint!.ToString()!)}; " +
                                                                      $"client address: {Helper.GetIpBySocket(_proxyTransportConnection?.GetUserListener()!)}; " +
                                                                      $"client port: {Helper.GetPortBySocket(_proxyTransportConnection?.GetUserListener()!)}.");
        return _proxyTransportMessaging!.Disconnect();
    }
}