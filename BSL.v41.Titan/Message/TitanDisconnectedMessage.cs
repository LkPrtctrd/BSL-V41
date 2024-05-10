namespace BSL.v41.Titan.Message;

public abstract class TitanDisconnectedMessage
{
    public static int GetMessageType()
    {
        return 25892;
    }

    public static int GetServiceNodeType()
    {
        return 1;
    }
}