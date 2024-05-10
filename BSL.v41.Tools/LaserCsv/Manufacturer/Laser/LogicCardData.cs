using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicCardData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private bool _dontUpgradeStat;
    private int _dynamicRarityStartSeason;
    private bool _hideDamageStat;
    private string _iconSwf = null!;
    private bool _lockedForChronos;
    private int _metaType;
    private string _powerIcon1ExportName = null!;
    private string _powerIcon2ExportName = null!;
    private string _powerNumber2Tid = null!;
    private string _powerNumber3Tid = null!;
    private string _powerNumberTid = null!;
    private string _rarity = null!;
    private string _requiresCard = null!;
    private string _skill = null!;
    private int _sortOrder;
    private string _target = null!;
    private string _type = null!;
    private int _value;
    private int _value2;
    private int _value3;

    // LogicCardData.

    public override void CreateReferences()
    {
        _iconSwf = GetValue("IconSWF", 0);
        _target = GetValue("Target", 0);
        _lockedForChronos = GetBooleanValue("LockedForChronos", 0);
        _dynamicRarityStartSeason = GetIntegerValue("DynamicRarityStartSeason", 0);
        _metaType = GetIntegerValue("MetaType", 0);
        _requiresCard = GetValue("RequiresCard", 0);
        _type = GetValue("Type", 0);
        _skill = GetValue("Skill", 0);
        _value = GetIntegerValue("Value", 0);
        _value2 = GetIntegerValue("Value2", 0);
        _value3 = GetIntegerValue("Value3", 0);
        _rarity = GetValue("Rarity", 0);
        _powerNumberTid = GetValue("PowerNumberTID", 0);
        _powerNumber2Tid = GetValue("PowerNumber2TID", 0);
        _powerNumber3Tid = GetValue("PowerNumber3TID", 0);
        _powerIcon1ExportName = GetValue("PowerIcon1ExportName", 0);
        _powerIcon2ExportName = GetValue("PowerIcon2ExportName", 0);
        _sortOrder = GetIntegerValue("SortOrder", 0);
        _dontUpgradeStat = GetBooleanValue("DontUpgradeStat", 0);
        _hideDamageStat = GetBooleanValue("HideDamageStat", 0);
    }

    public string GetIconSwf()
    {
        return _iconSwf;
    }

    public string GetTarget()
    {
        return _target;
    }

    public bool GetLockedForChronos()
    {
        return _lockedForChronos;
    }

    public int GetDynamicRarityStartSeason()
    {
        return _dynamicRarityStartSeason;
    }

    public int GetMetaType()
    {
        return _metaType;
    }

    public string GetRequiresCard()
    {
        return _requiresCard;
    }

    public new string GetType()
    {
        return _type;
    }

    public string GetSkill()
    {
        return _skill;
    }

    public int GetValue()
    {
        return _value;
    }

    public int GetValue2()
    {
        return _value2;
    }

    public int GetValue3()
    {
        return _value3;
    }

    public string GetRarity()
    {
        return _rarity;
    }

    public string GetPowerNumberTid()
    {
        return _powerNumberTid;
    }

    public string GetPowerNumber2Tid()
    {
        return _powerNumber2Tid;
    }

    public string GetPowerNumber3Tid()
    {
        return _powerNumber3Tid;
    }

    public string GetPowerIcon1ExportName()
    {
        return _powerIcon1ExportName;
    }

    public string GetPowerIcon2ExportName()
    {
        return _powerIcon2ExportName;
    }

    public int GetSortOrder()
    {
        return _sortOrder;
    }

    public bool GetDontUpgradeStat()
    {
        return _dontUpgradeStat;
    }

    public bool GetHideDamageStat()
    {
        return _hideDamageStat;
    }
}