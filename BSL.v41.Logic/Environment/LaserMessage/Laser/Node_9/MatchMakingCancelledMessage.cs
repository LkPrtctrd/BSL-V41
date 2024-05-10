using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;

public class MatchMakingCancelledMessage : PiranhaMessage
{
    public MatchMakingCancelledMessage()
    {
        Helper.Skip();
    }

    public override int GetMessageType()
    {
        return 20406;
    }

    public override int GetServiceNodeType()
    {
        return 9;
    }
}