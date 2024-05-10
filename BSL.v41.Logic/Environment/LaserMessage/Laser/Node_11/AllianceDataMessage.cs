using BSL.v41.Logic.Environment.LaserMessage.Sepo.Alliance;
using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_11;

public class AllianceDataMessage : PiranhaMessage
{
    private AllianceFullEntry _allianceFullEntry = null!;

    public AllianceDataMessage()
    {
        Helper.Skip();
    }

    public bool IsMyAlliance { get; set; }
    public long MyAccountId { get; set; }

    public override void Encode()
    {
        base.Encode();

        ByteStream.WriteBoolean(IsMyAlliance);
        _allianceFullEntry.Encode(ByteStream, MyAccountId);
    }

    public override void Destruct()
    {
        base.Destruct();
    }

    public void SetAllianceFullEntry(AllianceFullEntry allianceFullEntry)
    {
        _allianceFullEntry = allianceFullEntry;
    }

    public override int GetMessageType()
    {
        return 24301;
    }

    public override int GetServiceNodeType()
    {
        return 11;
    }
}