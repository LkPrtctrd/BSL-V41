using BSL.v41.General.NetIsland.LaserBattle;
using BSL.v41.General.Networking;
using BSL.v41.Logic.Environment.LaserMessage;

namespace BSL.v41.General.Cloud;

public static class InteractiveModule
{
    public static readonly Dictionary<long, ServerConnection>? ServerConnections = new();
    public static readonly Dictionary<long, long> UdpSessionIdsMassive = new();
    public static readonly Dictionary<long, LogicBattleModeServer>? LogicBattleModeServersMassive = new();

    public static void SendMessageToAllActivePlayersByTcpProtocol(PiranhaMessage piranhaMessage)
    {
        if (ServerConnections == null) return;
        foreach (var serverConnection in
                 ServerConnections.Where(serverConnection => serverConnection.Key > 0))
            serverConnection.Value.GetMessaging()!.Send(piranhaMessage);
    }

    public static void CloseAllPlayersActiveSessions()
    {
        if (ServerConnections == null) return;
        foreach (var serverConnection in
                 ServerConnections.Where(serverConnection => serverConnection.Key > 0))
            serverConnection.Value.Disconnect();
    }

    public static void ClearUdpSessionIdsMassive()
    {
        UdpSessionIdsMassive.Clear();
    }

    public static void ClearLogicBattleModeServersMassive()
    {
        LogicBattleModeServersMassive!.Clear();
    }
}