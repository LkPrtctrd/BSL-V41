using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicMilestoneData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private int _index;
    private int _league;
    private int _primaryLvlUpRewardCount;
    private string _primaryLvlUpRewardData = null!;
    private int _primaryLvlUpRewardExtraData;
    private int _primaryLvlUpRewardType;
    private int _progress;
    private int _progressStart;
    private int _season;
    private int _seasonEndRewardKeys;
    private int _secondaryLvlUpRewardCount;
    private string _secondaryLvlUpRewardData = null!;
    private int _secondaryLvlUpRewardExtraData;
    private int _secondaryLvlUpRewardType;
    private int _tier;
    private int _type;

    // LogicMilestoneData.

    public override void CreateReferences()
    {
        _type = GetIntegerValue("Type", 0);
        _index = GetIntegerValue("Index", 0);
        _progressStart = GetIntegerValue("ProgressStart", 0);
        _progress = GetIntegerValue("Progress", 0);
        _league = GetIntegerValue("League", 0);
        _tier = GetIntegerValue("Tier", 0);
        _season = GetIntegerValue("Season", 0);
        _seasonEndRewardKeys = GetIntegerValue("SeasonEndRewardKeys", 0);
        _primaryLvlUpRewardType = GetIntegerValue("PrimaryLvlUpRewardType", 0);
        _primaryLvlUpRewardCount = GetIntegerValue("PrimaryLvlUpRewardCount", 0);
        _primaryLvlUpRewardExtraData = GetIntegerValue("PrimaryLvlUpRewardExtraData", 0);
        _primaryLvlUpRewardData = GetValue("PrimaryLvlUpRewardData", 0);
        _secondaryLvlUpRewardType = GetIntegerValue("SecondaryLvlUpRewardType", 0);
        _secondaryLvlUpRewardCount = GetIntegerValue("SecondaryLvlUpRewardCount", 0);
        _secondaryLvlUpRewardExtraData = GetIntegerValue("SecondaryLvlUpRewardExtraData", 0);
        _secondaryLvlUpRewardData = GetValue("SecondaryLvlUpRewardData", 0);
    }

    public new int GetType()
    {
        return _type;
    }

    public int GetIndex()
    {
        return _index;
    }

    public int GetProgressStart()
    {
        return _progressStart;
    }

    public int GetProgress()
    {
        return _progress;
    }

    public int GetLeague()
    {
        return _league;
    }

    public int GetTier()
    {
        return _tier;
    }

    public int GetSeason()
    {
        return _season;
    }

    public int GetSeasonEndRewardKeys()
    {
        return _seasonEndRewardKeys;
    }

    public int GetPrimaryLvlUpRewardType()
    {
        return _primaryLvlUpRewardType;
    }

    public int GetPrimaryLvlUpRewardCount()
    {
        return _primaryLvlUpRewardCount;
    }

    public int GetPrimaryLvlUpRewardExtraData()
    {
        return _primaryLvlUpRewardExtraData;
    }

    public string GetPrimaryLvlUpRewardData()
    {
        return _primaryLvlUpRewardData;
    }

    public int GetSecondaryLvlUpRewardType()
    {
        return _secondaryLvlUpRewardType;
    }

    public int GetSecondaryLvlUpRewardCount()
    {
        return _secondaryLvlUpRewardCount;
    }

    public int GetSecondaryLvlUpRewardExtraData()
    {
        return _secondaryLvlUpRewardExtraData;
    }

    public string GetSecondaryLvlUpRewardData()
    {
        return _secondaryLvlUpRewardData;
    }
}