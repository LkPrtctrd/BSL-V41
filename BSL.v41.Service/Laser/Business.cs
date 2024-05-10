using BSL.v41.General.Network;
using BSL.v41.Proxy.Network;

namespace BSL.v41.Service.Laser;

public static class Business
{
    public static HttpLaserSocketListener? HttpLaserSocketListener;
    public static readonly Dictionary<int, TcpLaserSocketListener?> TcpLaserSocketListeners = new();
    public static readonly Dictionary<int, UdpLaserSocketListener?> UdpLaserSocketListeners = new();
    public static ProxySocketTransportListener? ProxySocketTransportListener;
}