using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;

public class GoHomeFromOfflinePractiseMessage : PiranhaMessage
{
    public GoHomeFromOfflinePractiseMessage()
    {
        Helper.Skip();
    }

    public override void Decode()
    {
        base.Decode();

        _ = ByteStream.ReadBoolean();
    }

    public override void Clear()
    {
        base.Clear();
    }

    public override int GetMessageType()
    {
        return 14109;
    }

    public override int GetServiceNodeType()
    {
        return 9;
    }
}