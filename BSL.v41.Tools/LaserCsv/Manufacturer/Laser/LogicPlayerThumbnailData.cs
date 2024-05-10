using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicPlayerThumbnailData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private string _iconSwf = null!;
    private int _requiredExpLevel;
    private string _requiredHero = null!;
    private int _requiredSeasonPoints;
    private int _requiredTotalTrophies;
    private int _sortOrder;

    // LogicPlayerThumbnailData.

    public override void CreateReferences()
    {
        _requiredExpLevel = GetIntegerValue("RequiredExpLevel", 0);
        _requiredTotalTrophies = GetIntegerValue("RequiredTotalTrophies", 0);
        _requiredSeasonPoints = GetIntegerValue("RequiredSeasonPoints", 0);
        _requiredHero = GetValue("RequiredHero", 0);
        _iconSwf = GetValue("IconSWF", 0);
        _sortOrder = GetIntegerValue("SortOrder", 0);
    }

    public int GetRequiredExpLevel()
    {
        return _requiredExpLevel;
    }

    public int GetRequiredTotalTrophies()
    {
        return _requiredTotalTrophies;
    }

    public int GetRequiredSeasonPoints()
    {
        return _requiredSeasonPoints;
    }

    public string GetRequiredHero()
    {
        return _requiredHero;
    }

    public string GetIconSwf()
    {
        return _iconSwf;
    }

    public int GetSortOrder()
    {
        return _sortOrder;
    }
}