using BSL.v41.Titan.Mathematical.Data;
using BSL.v41.Titan.Mathematical.Massive;
using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Manufacturer.Laser;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer;

public class LogicDataTable
{
    private readonly CsvTable _table;
    private readonly int _tableIndex;
    private LogicArrayList<LogicData> _items = null!;

    public LogicDataTable(CsvTable table, int index)
    {
        _tableIndex = index;
        _table = table;

        LoadTable();
    }

    public void LoadTable()
    {
        _items = new LogicArrayList<LogicData>();
        {
            for (var i = 0; i < _table.GetRowCount(); i++)
            {
                var data = CreateItem(_table.GetRowAt(i));

                if (data == null!) break;

                _items.Add(data);
            }
        }

        CreateReferences();
    }

    public LogicData CreateItem(CsvRow row)
    {
        LogicData data = null!;

        return _tableIndex switch
        {
            3 => new LogicGlobalData(row, this),
            5 => new LogicResourceData(row, this),
            6 => new LogicProjectileData(row, this),
            8 => new LogicAllianceBadgeData(row, this),
            14 => new LogicRegionData(row, this),
            15 => new LogicLocationData(row, this),
            16 => new LogicCharacterData(row, this),
            17 => new LogicAreaEffectData(row, this),
            18 => new LogicItemData(row, this),
            20 => new LogicSkillData(row, this),
            23 => new LogicCardData(row, this),
            25 => new LogicAllianceRoleData(row, this),
            27 => new LogicTileData(row, this),
            28 => new LogicPlayerThumbnailData(row, this),
            29 => new LogicSkinData(row, this),
            33 => new LogicGameModeVariationData(row, this),
            39 => new LogicMilestoneData(row, this),
            40 => new LogicMessageData(row, this),
            41 => new LogicThemeData(row, this),
            43 => new LogicNameColorData(row, this),
            44 => new LogicSkinConfData(row, this),
            56 => new LogicLocationThemeData(row, this),
            50 => new LogicAccessoryData(row, this),
            _ => data
        };
    }

    public void CreateReferences()
    {
        for (var i = 0; i < _items.Count; i++) _items[i].AutoLoadData();
        for (var i = 0; i < _items.Count; i++) _items[i].CreateReferences();
    }

    public LogicData GetItemAt(int index)
    {
        return _items[index];
    }

    public LogicData GetDataByName(string name)
    {
        if (string.IsNullOrEmpty(name)) return null!;
        {
            for (var i = 0; i < _items.Count; i++)
                if (_items[i].GetName().Equals(name))
                    return _items[i];
        }

        return null!;
    }

    public LogicData GetItemById(int globalId)
    {
        return GlobalId.GetInstanceId(globalId) < 0 || GlobalId.GetInstanceId(globalId) >= _items.Count
            ? null!
            : _items[GlobalId.GetInstanceId(globalId)];
    }

    public int GetItemCount()
    {
        return _items.Count;
    }

    public int GetTableIndex()
    {
        return _tableIndex;
    }

    public string GetTableName()
    {
        return _table.GetFileName();
    }
}