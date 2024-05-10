using BSL.v41.General.Cloud;
using BSL.v41.General.Network;
using BSL.v41.Logic.Database;
using BSL.v41.Proxy.Cloud;
using BSL.v41.Proxy.Network;
using BSL.v41.Service.Laser;
using BSL.v41.Tools.LaserData;

namespace BSL.v41.Manage.Laser;

public class Loader
{
    private List<int> _tcpPorts = [];
    private string _udpHost = null!;
    private List<int> _udpPorts = [];

    public void LoadNet()
    {
        Business.HttpLaserSocketListener = new HttpLaserSocketListener("localhost", 9119);
        Business.ProxySocketTransportListener = new ProxySocketTransportListener("0.0.0.0:1001");
        ProxyStaticalClient.ProxySocketTransportClient = new ProxySocketTransportClient("localhost:1002");

        foreach (var data in _tcpPorts)
            Business.TcpLaserSocketListeners.Add(data, new TcpLaserSocketListener($"0.0.0.0:{data}", true));

        foreach (var data in _udpPorts) Business.UdpLaserSocketListeners.Add(data, new UdpLaserSocketListener(data));
    }

    public void ManageNet()
    {
        Business.HttpLaserSocketListener!.StartSocket();
        Business.ProxySocketTransportListener!.StartSocket();

        foreach (var data in _tcpPorts) Business.TcpLaserSocketListeners[data]!.StartSocket();
        foreach (var data in _udpPorts) Business.UdpLaserSocketListeners[data]!.StartSocket();

        Saver.UdpInfoDomain = _udpHost;
        Saver.UdpInfoPorts = _udpPorts;
        Saver.TcpInfoPorts = _tcpPorts;

        Saver.TcpLaserSocketListeners = Business.TcpLaserSocketListeners;
        Saver.UdpLaserSocketListeners = Business.UdpLaserSocketListeners;

        ProxyStaticalClient.ProxySocketTransportClient.StartTransportClient();
    }

    public void ManageDb()
    {
        Databases.AccountDatabase.LoadDataFromFile();
        Databases.AccountDatabase.CyclingLoadDataFromFile(30);

        Databases.AllianceDatabase.LoadDataFromFile();
        Databases.AllianceDatabase.CyclingLoadDataFromFile(30);

        Databases.NotificationIntraSignedDatabase.Debug();
    }

    public void ManageTools()
    {
        Fingerprint.Parse();
    }

    public void SetParameters(string udpHost, List<int> udpInfoPorts, List<int> tcpInfoPorts)
    {
        _udpHost = udpHost;
        _udpPorts = udpInfoPorts;
        _tcpPorts = tcpInfoPorts;
    }
}