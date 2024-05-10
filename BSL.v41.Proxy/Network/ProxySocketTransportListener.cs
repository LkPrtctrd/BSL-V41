using System.Net.Sockets;
using BSL.v41.Proxy.Networking;
using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.Graphic;

namespace BSL.v41.Proxy.Network;

public class ProxySocketTransportListener(string domain)
{
    private ManualResetEvent _manualResetEvent = null!;
    private bool _manualResetEventStarted;
    private Socket _proxyTransportSocketListener = null!;

    public void StartSocket()
    {
        _proxyTransportSocketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _proxyTransportSocketListener.Bind(Helper.GetFullyEndPointByDomain(domain));
        _proxyTransportSocketListener.Listen(128);

        _manualResetEvent = new ManualResetEvent(false);

        ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Start,
            $"Network-transport-server started! Information: " +
            $"server protocol type: {_proxyTransportSocketListener.ProtocolType}; " +
            $"server address: {Helper.GetIpFromDomain(domain)}; " +
            $"server port: {Helper.GetPortFromDomain(domain)}.");

        if (!_manualResetEventStarted) ManualResetUpdate();
    }

    public void StopSocket()
    {
        _manualResetEventStarted = false;
        _proxyTransportSocketListener.Close();

        _manualResetEvent.Dispose();
        _manualResetEvent.Close();
        _manualResetEvent.Reset();

        ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Stop,
            "Network-transport-server stopped! Information: " +
            $"server protocol type: {_proxyTransportSocketListener.ProtocolType}; " +
            $"server address: {Helper.GetIpFromDomain(domain)}; " +
            $"server port: {Helper.GetPortFromDomain(domain)}.");
    }

    public void ManualResetUpdate()
    {
        _manualResetEventStarted = true;

        new Thread(() =>
        {
            while (true)
            {
                _manualResetEvent.Reset();
                _proxyTransportSocketListener.BeginAccept(StartAccepting, null);
                _manualResetEvent.WaitOne();
            }
        }).Start();
    }

    public void StartAccepting(IAsyncResult iAsyncResult)
    {
        try
        {
            var socket = _proxyTransportSocketListener.EndAccept(iAsyncResult);
            {
                ProcessInAcceptMethod(socket);
            }
        }
        catch (Exception)
        {
            // ignored.
        }
    }

    public void ProcessInAcceptMethod(Socket socket)
    {
        var connection = new ProxyTransportConnection();
        {
            connection.SetListener(_proxyTransportSocketListener);
            connection.SetUserListener(socket);
        }

        var serverConnection = new ProxyTransportLaserConnection(connection);
        {
            serverConnection.Initialize();
        }

        socket.BeginReceive(connection.ByteBuffer!, 0, 32762, SocketFlags.None, OnReceive, connection);
        {
            _manualResetEvent.Set();
        }
    }

    public static void OnReceive(IAsyncResult iAsyncResult)
    {
        try
        {
            var connection = (ProxyTransportConnection)iAsyncResult.AsyncState!;
            {
                var data = connection.GetListener()!.EndReceive(iAsyncResult);
                {
                    if (data <= 0)
                    {
                        connection.Close();
                        return;
                    }

                    connection.MemoryStream!.Write(connection.ByteBuffer!, 0, data);
                    {
                        if (connection.GetProxyTransportMessaging()!.NextMessage() != 0)
                        {
                            connection.Close();
                            return;
                        }
                    }
                }
            }

            connection.GetUserListener()!.BeginReceive(connection.ByteBuffer!, 0, 32762, SocketFlags.None, OnReceive,
                connection);
        }
        catch
        {
            // ignored.
        }
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
}