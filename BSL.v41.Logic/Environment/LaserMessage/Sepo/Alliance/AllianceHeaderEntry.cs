using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.DataStream.Helps;
using BSL.v41.Tools.LaserCsv;

namespace BSL.v41.Logic.Environment.LaserMessage.Sepo.Alliance;

public class AllianceHeaderEntry
{
    private int _allianceBadgeId;
    private long _allianceId;
    private string _allianceName = null!;
    private int _alliancePreferredLanguageId;
    private int _allianceRegionId;
    private int _allianceType;
    private bool _isFamilyType;
    private int _nowTrophies;
    private int _numberOfMembers;
    private int _requiredTrophies;

    public void Encode(ByteStream byteStream)
    {
        byteStream.WriteLong(_allianceId);
        byteStream.WriteString(_allianceName);
        ByteStreamHelper.WriteDataReference(byteStream, _allianceBadgeId);
        byteStream.WriteVInt(_allianceType);
        byteStream.WriteVInt(_numberOfMembers);
        byteStream.WriteVInt(_nowTrophies);
        byteStream.WriteVInt(_requiredTrophies);
        ByteStreamHelper.WriteDataReference(byteStream, _alliancePreferredLanguageId);
        byteStream.WriteString(LogicDataTables.GetDataById(_allianceRegionId).GetName());
        byteStream.WriteVInt(0);
        byteStream.WriteBoolean(_isFamilyType);
    }

    public void SetPreferredLanguage(int value)
    {
        _alliancePreferredLanguageId = value;
    }

    public void SetAllianceId(long value)
    {
        _allianceId = value;
    }

    public void SetNumberOfMembers(int value)
    {
        _numberOfMembers = value;
    }

    public void SetAllianceBadgeData(int value)
    {
        _allianceBadgeId = value;
    }

    public void SetRequiredScore(int value)
    {
        _requiredTrophies = value;
    }

    public void SetAllianceType(int value)
    {
        _allianceType = value;
    }

    public void SetScore(int value)
    {
        _nowTrophies = value;
    }

    public void SetAllianceRegionData(int value)
    {
        _allianceRegionId = value;
    }

    public void SetAllianceName(string value)
    {
        _allianceName = value;
    }

    public void SetIsFamilyType(bool value)
    {
        _isFamilyType = value;
    }

    public bool IsJoinable()
    {
        return _numberOfMembers < 100;
    }

    public bool IsDeleted()
    {
        return _numberOfMembers == 0;
    }
}