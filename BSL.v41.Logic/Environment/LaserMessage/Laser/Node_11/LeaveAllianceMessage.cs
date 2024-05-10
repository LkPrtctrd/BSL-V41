using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_11;

public class LeaveAllianceMessage : PiranhaMessage
{
    public LeaveAllianceMessage()
    {
        Helper.Skip();
    }

    public override int GetMessageType()
    {
        return 14308;
    }

    public override int GetServiceNodeType()
    {
        return 11;
    }
}