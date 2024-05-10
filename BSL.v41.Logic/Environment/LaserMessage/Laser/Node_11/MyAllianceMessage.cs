using BSL.v41.Logic.Environment.LaserMessage.Sepo.Alliance;
using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Supercell.Titan.CommonUtils.Utils;
using BSL.v41.Titan.DataStream.Helps;
using BSL.v41.Tools.LaserCsv;
using BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_11;

public class MyAllianceMessage : PiranhaMessage
{
    public MyAllianceMessage()
    {
        Helper.Skip();
    }

    public int OnlineMembers { get; set; }
    public AllianceRoleHelperTable AllianceRole { get; set; }
    public AllianceHeaderEntry AllianceHeaderEntry { get; set; } = null!;

    public override void Encode()
    {
        base.Encode();

        ByteStream.WriteVInt(OnlineMembers);
        if (!ByteStream.WriteBoolean(AllianceHeaderEntry != null!)) return;
        ByteStreamHelper.WriteDataReference(ByteStream, ((LogicAllianceRoleData)LogicDataTables
            .GetDataFromTable(CsvHelperTable.AllianceRoles.GetId())
            .GetDataByName(AllianceRole.GetCsvName())).GetGlobalId());
        AllianceHeaderEntry!.Encode(ByteStream);
    }

    public override void Destruct()
    {
        base.Destruct();
    }

    public override int GetMessageType()
    {
        return 24399;
    }

    public override int GetServiceNodeType()
    {
        return 11;
    }
}