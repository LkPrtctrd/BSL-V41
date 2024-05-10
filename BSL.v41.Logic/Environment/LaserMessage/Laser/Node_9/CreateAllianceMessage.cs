using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Supercell.Titan.CommonUtils.Utils;
using BSL.v41.Titan.DataStream.Helps;
using BSL.v41.Titan.Mathematical.Data;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_9;

public class CreateAllianceMessage : PiranhaMessage
{
    private int _allianceBadgeData;
    private int _allianceRegion;
    private int _allianceType;
    private int _requiredScore;

    public CreateAllianceMessage()
    {
        Helper.Skip();
    }

    public int AllianceRegion
    {
        get => _allianceRegion < 1000000 ? GlobalId.CreateGlobalId(CsvHelperTable.Regions.GetId(), 0) : _allianceRegion;
        set => _allianceRegion = value;
    }

    public bool IsFamilyType { get; set; }

    public string AllianceName { get; set; } = null!;

    public string AllianceDescription { get; set; } = null!;

    public override void Decode()
    {
        base.Decode();

        AllianceName = ByteStream.ReadString(15 * 1024);
        AllianceDescription = ByteStream.ReadString(100 * 1024);
        _allianceBadgeData = ByteStreamHelper.ReadDataReference(ByteStream);
        _allianceRegion = ByteStreamHelper.ReadDataReference(ByteStream);
        _allianceType = ByteStream.ReadVInt();
        _requiredScore = ByteStream.ReadVInt();
        IsFamilyType = ByteStream.ReadBoolean();
    }

    public override void Clear()
    {
        base.Clear();
    }

    public int GetAllianceBadgeData()
    {
        return _allianceBadgeData < 1000000
            ? GlobalId.CreateGlobalId(CsvHelperTable.AllianceBadges.GetId(), 0)
            : _allianceBadgeData;
    }

    public int GetAllianceType()
    {
        return _allianceType;
    }

    public int GetRequiredScore()
    {
        return _requiredScore;
    }

    public override int GetMessageType()
    {
        return 14301;
    }

    public override int GetServiceNodeType()
    {
        return 9;
    }
}