namespace BSL.v41.Titan.Message;

public abstract class TitanLoginFailedMessage
{
    public static int GetMessageType()
    {
        return 20100 + 1 + 2;
    }

    public static int GetServiceNodeType()
    {
        return 1;
    }
}