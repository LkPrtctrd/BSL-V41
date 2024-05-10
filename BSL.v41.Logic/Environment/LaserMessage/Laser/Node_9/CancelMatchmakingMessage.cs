using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;

public class CancelMatchmakingMessage : PiranhaMessage
{
    public CancelMatchmakingMessage()
    {
        Helper.Skip();
    }

    public override int GetMessageType()
    {
        return 14106;
    }

    public override int GetServiceNodeType()
    {
        return 9;
    }
}