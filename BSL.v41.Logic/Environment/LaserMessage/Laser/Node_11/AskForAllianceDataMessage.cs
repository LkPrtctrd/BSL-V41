using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_11;

public class AskForAllianceDataMessage : PiranhaMessage
{
    private long _allianceId;

    public AskForAllianceDataMessage()
    {
        Helper.Skip();
    }

    public override void Decode()
    {
        base.Decode();

        _allianceId = ByteStream.ReadLong();
        if (ByteStream.ReadBoolean()) _ = ByteStream.ReadLong();
    }

    public override void Clear()
    {
        base.Clear();
    }

    public long GetAllianceId()
    {
        return _allianceId;
    }

    public override int GetMessageType()
    {
        return 14302;
    }

    public override int GetServiceNodeType()
    {
        return 11;
    }
}