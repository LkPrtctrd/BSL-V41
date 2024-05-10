using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_1;

public class KeepAliveMessage : PiranhaMessage
{
    public KeepAliveMessage()
    {
        Helper.Skip();
    }

    public override int GetMessageType()
    {
        return 10108;
    }

    public override int GetServiceNodeType()
    {
        return 1;
    }
}