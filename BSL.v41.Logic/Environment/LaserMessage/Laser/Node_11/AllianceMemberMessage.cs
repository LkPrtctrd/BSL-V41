using BSL.v41.Logic.Environment.LaserMessage.Sepo.Alliance;
using BSL.v41.Supercell.Titan.CommonUtils;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_11;

public class AllianceMemberMessage : PiranhaMessage
{
    public AllianceMemberMessage()
    {
        Helper.Skip();
    }

    public long MemberAccountId { get; set; }
    public AllianceMemberEntry AllianceMemberEntry { get; set; } = null!;

    public override void Encode()
    {
        base.Encode();

        ByteStream.WriteLong(MemberAccountId);
        AllianceMemberEntry.Encode(ByteStream);
    }

    public override void Destruct()
    {
        base.Destruct();
    }

    public override int GetMessageType()
    {
        return 24308;
    }

    public override int GetServiceNodeType()
    {
        return 11;
    }
}