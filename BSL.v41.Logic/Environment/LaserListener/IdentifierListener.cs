namespace BSL.v41.Logic.Environment.LaserListener;

public static class IdentifierListener
{
    private static readonly Dictionary<long, LogicGameListener> LogicGameListeners = new();

    public static LogicGameListener GetLogicGameListenerByAccountId(long accountId)
    {
        return LogicGameListeners[accountId];
    }

    public static bool GetV2LogicGameListenerByAccountId(long accountId)
    {
        return LogicGameListeners.TryGetValue(accountId, out _);
    }

    public static LogicGameListener SetLogicGameListenerByAccountId(long accountId, LogicGameListener logicGameListener)
    {
        LogicGameListeners.TryAdd(accountId, logicGameListener);
        return logicGameListener;
    }

    public static void RemoveLogicGameListenerByAccountId(long accountId)
    {
        LogicGameListeners.Remove(accountId);
    }

    public static Dictionary<long, LogicGameListener> GetLogicGameListeners()
    {
        return LogicGameListeners;
    }
}