using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicItemData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private string _activateEffect = null!;
    private bool _canBePickedUp;
    private string _exportName = null!;
    private string _exportNameEnemy = null!;
    private string _fileName = null!;
    private string _groundGlowExportName = null!;
    private string _layer = null!;
    private string _loopingEffect = null!;
    private string _parentItemForSkin = null!;
    private string _sCw = null!;
    private string _sCwEnemy = null!;
    private string _shadowExportName = null!;
    private string _spawnEffect = null!;
    private string _triggerAreaEffect = null!;
    private int _triggerRangeSubTiles;
    private int _value;
    private int _value2;

    // LogicItemData.

    public override void CreateReferences()
    {
        _parentItemForSkin = GetValue("ParentItemForSkin", 0);
        _fileName = GetValue("FileName", 0);
        _exportName = GetValue("ExportName", 0);
        _exportNameEnemy = GetValue("ExportNameEnemy", 0);
        _shadowExportName = GetValue("ShadowExportName", 0);
        _groundGlowExportName = GetValue("GroundGlowExportName", 0);
        _loopingEffect = GetValue("LoopingEffect", 0);
        _value = GetIntegerValue("Value", 0);
        _value2 = GetIntegerValue("Value2", 0);
        _triggerRangeSubTiles = GetIntegerValue("TriggerRangeSubTiles", 0);
        _triggerAreaEffect = GetValue("TriggerAreaEffect", 0);
        _canBePickedUp = GetBooleanValue("CanBePickedUp", 0);
        _spawnEffect = GetValue("SpawnEffect", 0);
        _activateEffect = GetValue("ActivateEffect", 0);
        _sCw = GetValue("SCW", 0);
        _sCwEnemy = GetValue("SCWEnemy", 0);
        _layer = GetValue("Layer", 0);
    }

    public string GetParentItemForSkin()
    {
        return _parentItemForSkin;
    }

    public string GetFileName()
    {
        return _fileName;
    }

    public string GetExportName()
    {
        return _exportName;
    }

    public string GetExportNameEnemy()
    {
        return _exportNameEnemy;
    }

    public string GetShadowExportName()
    {
        return _shadowExportName;
    }

    public string GetGroundGlowExportName()
    {
        return _groundGlowExportName;
    }

    public string GetLoopingEffect()
    {
        return _loopingEffect;
    }

    public int GetValue()
    {
        return _value;
    }

    public int GetValue2()
    {
        return _value2;
    }

    public int GetTriggerRangeSubTiles()
    {
        return _triggerRangeSubTiles;
    }

    public string GetTriggerAreaEffect()
    {
        return _triggerAreaEffect;
    }

    public bool GetCanBePickedUp()
    {
        return _canBePickedUp;
    }

    public string GetSpawnEffect()
    {
        return _spawnEffect;
    }

    public string GetActivateEffect()
    {
        return _activateEffect;
    }

    public string GetScw()
    {
        return _sCw;
    }

    public string GetScwEnemy()
    {
        return _sCwEnemy;
    }

    public string GetLayer()
    {
        return _layer;
    }
}