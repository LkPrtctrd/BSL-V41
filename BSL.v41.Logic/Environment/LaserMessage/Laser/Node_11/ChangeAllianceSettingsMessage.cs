using BSL.v41.Supercell.Titan.CommonUtils;
using BSL.v41.Supercell.Titan.CommonUtils.Utils;
using BSL.v41.Titan.DataStream.Helps;
using BSL.v41.Titan.Mathematical.Data;

namespace BSL.v41.Logic.Environment.LaserMessage.Laser.Node_11;

public class ChangeAllianceSettingsMessage : PiranhaMessage
{
    private int _allianceBadgeData;
    private string _allianceDescription = null!;
    private int _allianceRegion;
    private int _allianceType;
    private bool _isFamilyType;
    private int _requiredScore;

    public ChangeAllianceSettingsMessage()
    {
        Helper.Skip();
    }

    public override void Decode()
    {
        base.Decode();

        _allianceDescription = ByteStream.ReadString(1024 * 150);
        _allianceBadgeData = ByteStreamHelper.ReadDataReference(ByteStream);
        _allianceRegion = ByteStreamHelper.ReadDataReference(ByteStream);
        _allianceType = ByteStream.ReadVInt();
        _requiredScore = ByteStream.ReadVInt();
        _isFamilyType = ByteStream.ReadBoolean();
    }

    public override void Clear()
    {
        base.Clear();
    }

    public string GetAllianceDescription()
    {
        return _allianceDescription;
    }

    public int GetAllianceBadgeData()
    {
        return _allianceBadgeData < 1000000
            ? GlobalId.CreateGlobalId(CsvHelperTable.AllianceBadges.GetId(), 0)
            : _allianceBadgeData;
    }

    public int GetAllianceRegion()
    {
        return _allianceRegion < 1000000 ? GlobalId.CreateGlobalId(CsvHelperTable.Regions.GetId(), 0) : _allianceRegion;
    }

    public int GetAllianceType()
    {
        return _allianceType;
    }

    public int GetRequiredScore()
    {
        return _requiredScore;
    }

    public bool GetIsFamilyType()
    {
        return _isFamilyType;
    }

    public override int GetMessageType()
    {
        return 14316;
    }

    public override int GetServiceNodeType()
    {
        return 11;
    }
}