using System.Net.Sockets;
using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Titan.Graphic;

namespace BSL.v41.Proxy.Network;

public class ProxySocketTransportClient(string domain)
{
    private bool _isConnected;
    private Socket _proxySocketTransportClient = null!;

    public void StartTransportClient()
    {
        var cts = new CancellationTokenSource();

        _proxySocketTransportClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Task.Run(() =>
        {
            _isConnected = false;

            try
            {
                _proxySocketTransportClient.Connect(Helper.GetIpFromDomain(domain), Helper.GetPortFromDomain(domain));
                _isConnected = true;
                ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Info,
                    $"Connected to proxy-transport! Information: " +
                    $"client protocol type: {_proxySocketTransportClient.ProtocolType}; " +
                    $"client address: {Helper.GetIpFromDomain(domain)}; " +
                    $"client port: {Helper.GetPortFromDomain(domain)}.");
                cts.Cancel();
            }
            catch
            {
                while (true)
                    try
                    {
                        _proxySocketTransportClient.Connect(Helper.GetIpFromDomain(domain),
                            Helper.GetPortFromDomain(domain));
                        _isConnected = true;
                        ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Info,
                            $"Connected to proxy-transport! Information: " +
                            $"client protocol type: {_proxySocketTransportClient.ProtocolType}; " +
                            $"client address: {Helper.GetIpFromDomain(domain)}; " +
                            $"client port: {Helper.GetPortFromDomain(domain)}.");
                        cts.Cancel();
                        break;
                    }
                    catch
                    {
                        // ignored.
                    }
            }

            cts.Token.ThrowIfCancellationRequested();
        }, cts.Token);
    }

    public void StopTransportClient()
    {
        _proxySocketTransportClient.Disconnect(true);
    }

    public int OnSend(byte[] bytesToSend)
    {
        int v1;

        try
        {
            _proxySocketTransportClient.Send(bytesToSend);
            return 1;
        }
        catch
        {
            v1 = -2;
            if (_isConnected) StartTransportClient();
        }

        return v1;
    }
}