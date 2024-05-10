using BSL.v41.General.NetIsland;
using BSL.v41.General.Network;

namespace BSL.v41.General.Cloud;

public static class Saver
{
    public static int SearchTimeFactor = 10;
    public static int LastUdpSessionId = 6001000;
    public static string UdpInfoDomain = null!;
    public static List<int> TcpInfoPorts = [];
    public static List<int> UdpInfoPorts = [];
    public static Dictionary<int, UdpLaserSocketListener?> UdpLaserSocketListeners = null!;
    public static Dictionary<int, TcpLaserSocketListener?> TcpLaserSocketListeners = null!;
    public static readonly Dictionary<string, List<object>> MapStructureDictionary = new();
    public static OwnMatchmakingManager OwnMatchmakingManager { get; set; } = null!;
}