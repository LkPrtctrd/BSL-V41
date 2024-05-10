using BSL.v41.Supercell.Titan.CommonUtils.Utils;
using BSL.v41.Titan.Graphic;
using BSL.v41.Titan.Mathematical.Data;
using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Manufacturer;
using BSL.v41.Tools.LaserCsv.Manufacturer.Laser;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv;

public static class LogicDataTables
{
    private static string _globalPath = null!;
    private static Dictionary<int, LogicDataTable>? _dataTables;

    public static void CreateReferences(string gPath)
    {
        _globalPath = gPath;
        _dataTables = new Dictionary<int, LogicDataTable>();

        InitDataTable(CsvHelperTable.Accessories.GetFileName(), CsvHelperTable.Accessories.GetId());
        InitDataTable(CsvHelperTable.Resources.GetFileName(), CsvHelperTable.Resources.GetId());
        InitDataTable(CsvHelperTable.Projectiles.GetFileName(), CsvHelperTable.Projectiles.GetId());
        InitDataTable(CsvHelperTable.AllianceBadges.GetFileName(), CsvHelperTable.AllianceBadges.GetId());
        InitDataTable(CsvHelperTable.AllianceRoles.GetFileName(), CsvHelperTable.AllianceRoles.GetId());
        InitDataTable(CsvHelperTable.Locations.GetFileName(), CsvHelperTable.Locations.GetId());
        InitDataTable(CsvHelperTable.LocationThemes.GetFileName(), CsvHelperTable.LocationThemes.GetId());
        InitDataTable(CsvHelperTable.Characters.GetFileName(), CsvHelperTable.Characters.GetId());
        InitDataTable(CsvHelperTable.AreaEffects.GetFileName(), CsvHelperTable.AreaEffects.GetId());
        InitDataTable(CsvHelperTable.Items.GetFileName(), CsvHelperTable.Items.GetId());
        InitDataTable(CsvHelperTable.Skills.GetFileName(), CsvHelperTable.Skills.GetId());
        InitDataTable(CsvHelperTable.Cards.GetFileName(), CsvHelperTable.Cards.GetId());
        InitDataTable(CsvHelperTable.Tiles.GetFileName(), CsvHelperTable.Tiles.GetId());
        InitDataTable(CsvHelperTable.GameModeVariation.GetFileName(), CsvHelperTable.GameModeVariation.GetId());
        InitDataTable(CsvHelperTable.Messages.GetFileName(), CsvHelperTable.Messages.GetId());
        InitDataTable(CsvHelperTable.Milestones.GetFileName(), CsvHelperTable.Milestones.GetId());
        InitDataTable(CsvHelperTable.NameColors.GetFileName(), CsvHelperTable.NameColors.GetId());
        InitDataTable(CsvHelperTable.Regions.GetFileName(), CsvHelperTable.Regions.GetId());
        InitDataTable(CsvHelperTable.PlayerThumbnails.GetFileName(), CsvHelperTable.PlayerThumbnails.GetId());
        InitDataTable(CsvHelperTable.Skins.GetFileName(), CsvHelperTable.Skins.GetId());
        InitDataTable(CsvHelperTable.Globals.GetFileName(), CsvHelperTable.Globals.GetId());
        InitDataTable(CsvHelperTable.Themes.GetFileName(), CsvHelperTable.Themes.GetId());
        InitDataTable(CsvHelperTable.SkinConfs.GetFileName(), CsvHelperTable.SkinConfs.GetId());

        ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Start,
            $"Logic-element started! Information: element name: LogicDataTables; table buffer size: {_dataTables.Count}.");
    }

    private static void InitDataTable(string path, int tableIndex)
    {
        if (!File.Exists(_globalPath + path)) return;
        {
            var lines = File.ReadAllLines(_globalPath + path);
            {
                if (lines.Length > 1)
                    _dataTables!.Add(tableIndex, new LogicDataTable(new CsvNode(lines, path).GetTable(), tableIndex));
            }
        }
    }

    public static int GetTableCount()
    {
        return 61;
    }

    public static LogicData GetDataById(int globalId)
    {
        return GlobalId.GetClassId(globalId) is >= 0 and <= 60 && _dataTables![GlobalId.GetClassId(globalId)] != null!
            ? _dataTables[GlobalId.GetClassId(globalId)].GetItemById(globalId)
            : null!;
    }

    public static LogicDataTable GetDataFromTable(int tableIndex)
    {
        return tableIndex >= 0 && tableIndex <= GetTableCount() - 1 && _dataTables![tableIndex] != null!
            ? _dataTables[tableIndex]
            : null!;
    }

    public static LogicData[] GetAllDataFromCsvById(int id)
    {
        var data = Array.Empty<LogicData>();
        if (GetDataFromTable(id) == null!) return null!;

        for (var i = 0; i < GetDataFromTable(id).GetItemCount(); i++)
        {
            Array.Resize(ref data, data.Length + 1);
            {
                data[^1] = GetDataById(GlobalId.CreateGlobalId(id, i));
            }
        }

        return data;
    }

    public static LogicResourceData GetResourceByName(string name)
    {
        return (LogicResourceData)_dataTables![CsvHelperTable.Resources.GetId()].GetDataByName(name);
    }

    public static LogicAccessoryData GetAccessoryByName(string name)
    {
        return (LogicAccessoryData)_dataTables![CsvHelperTable.Accessories.GetId()].GetDataByName(name);
    }

    public static LogicCharacterData GetCharacterByName(string name)
    {
        return (LogicCharacterData)_dataTables![CsvHelperTable.Characters.GetId()].GetDataByName(name);
    }

    public static LogicCardData GetCardByName(string name)
    {
        return (LogicCardData)_dataTables![CsvHelperTable.Cards.GetId()].GetDataByName(name);
    }

    public static LogicProjectileData GetProjectileByName(string name)
    {
        return (LogicProjectileData)_dataTables![CsvHelperTable.Projectiles.GetId()].GetDataByName(name);
    }

    public static LogicLocationData GetLocationByName(string name)
    {
        return (LogicLocationData)_dataTables![CsvHelperTable.Locations.GetId()].GetDataByName(name);
    }

    public static LogicAllianceRoleData GetAllianceRoleByName(string name)
    {
        return (LogicAllianceRoleData)_dataTables![CsvHelperTable.AllianceRoles.GetId()].GetDataByName(name);
    }

    public static LogicLocationThemeData GetLocationThemeByName(string name)
    {
        return (LogicLocationThemeData)_dataTables![CsvHelperTable.LocationThemes.GetId()].GetDataByName(name);
    }

    public static LogicAreaEffectData GetAreaEffectByName(string name)
    {
        return (LogicAreaEffectData)_dataTables![CsvHelperTable.AreaEffects.GetId()].GetDataByName(name);
    }

    public static LogicItemData GetItemByName(string name)
    {
        return (LogicItemData)_dataTables![CsvHelperTable.Items.GetId()].GetDataByName(name);
    }

    public static LogicSkillData GetSkillByName(string name)
    {
        return (LogicSkillData)_dataTables![CsvHelperTable.Skills.GetId()].GetDataByName(name);
    }

    public static LogicSkinData GetSkinByName(string name)
    {
        return (LogicSkinData)_dataTables![CsvHelperTable.Skins.GetId()].GetDataByName(name);
    }

    public static LogicThemeData GetThemeByName(string name)
    {
        return (LogicThemeData)_dataTables![CsvHelperTable.Themes.GetId()].GetDataByName(name);
    }

    public static LogicSkinConfData GetSkinConfByName(string name)
    {
        return (LogicSkinConfData)_dataTables![CsvHelperTable.SkinConfs.GetId()].GetDataByName(name);
    }

    public static LogicGameModeVariationData GetGameModeVariationByName(string name)
    {
        return (LogicGameModeVariationData)_dataTables![CsvHelperTable.GameModeVariation.GetId()].GetDataByName(name);
    }
}