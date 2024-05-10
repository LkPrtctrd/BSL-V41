using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicLocationThemeData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private string _benchScw = null!;
    private int _blocking1AngleStep;
    private string _blocking1Mesh = null!;
    private string _blocking1Scw = null!;
    private int _blocking2AngleStep;
    private string _blocking2Mesh = null!;
    private string _blocking2Scw = null!;
    private int _blocking3AngleStep;
    private string _blocking3Mesh = null!;
    private string _blocking3Scw = null!;
    private int _blocking4AngleStep;
    private string _blocking4Mesh = null!;
    private string _blocking4Scw = null!;
    private int _destructableAngleStep;
    private int _destructableAngleStepCn;
    private string _destructableMesh = null!;
    private string _destructableMeshCn = null!;
    private string _destructableScw = null!;
    private string _destructableScwCn = null!;
    private string _fenceScw = null!;
    private string _forestScw = null!;
    private int _fragileAngleStep;
    private int _fragileAngleStepCn;
    private string _fragileMesh = null!;
    private string _fragileMeshCn = null!;
    private string _fragileScw = null!;
    private string _fragileScwCn = null!;
    private string _indestructibleMesh = null!;
    private string _indestructibleScw = null!;
    private string _laserBallSkinOverride = null!;
    private string _lootBoxSkinOverride = null!;
    private int _mapHeight;
    private string _mapPreviewBallExportName = null!;
    private int _mapPreviewBgColorBlue;
    private int _mapPreviewBgColorGreen;
    private int _mapPreviewBgColorRed;
    private string _mapPreviewCnOverrides = null!;
    private string _mapPreviewGemGrabSpawnHoleExportName = null!;
    private string _mapPreviewGoal1ExportName = null!;
    private string _mapPreviewGoal2ExportName = null!;
    private int _mapWidth;
    private string _maskedEnvironmentScw = null!;
    private string _mineGemSpawnScwOverride = null!;
    private string _respawningForestScw = null!;
    private int _respawningWallAngleStep;
    private string _respawningWallMesh = null!;
    private string _respawningWallScw = null!;
    private string _showdownBoostScwOverride = null!;
    private string _tileSetPrefix = null!;
    private string _waterTileScw = null!;

    // LogicLocationThemeData.

    public override void CreateReferences()
    {
        _tileSetPrefix = GetValue("TileSetPrefix", 0);
        _maskedEnvironmentScw = GetValue("MaskedEnvironmentScw", 0);
        _blocking1Scw = GetValue("Blocking1Scw", 0);
        _blocking1Mesh = GetValue("Blocking1Mesh", 0);
        _blocking1AngleStep = GetIntegerValue("Blocking1AngleStep", 0);
        _blocking2Scw = GetValue("Blocking2Scw", 0);
        _blocking2Mesh = GetValue("Blocking2Mesh", 0);
        _blocking2AngleStep = GetIntegerValue("Blocking2AngleStep", 0);
        _blocking3Scw = GetValue("Blocking3Scw", 0);
        _blocking3Mesh = GetValue("Blocking3Mesh", 0);
        _blocking3AngleStep = GetIntegerValue("Blocking3AngleStep", 0);
        _blocking4Scw = GetValue("Blocking4Scw", 0);
        _blocking4Mesh = GetValue("Blocking4Mesh", 0);
        _blocking4AngleStep = GetIntegerValue("Blocking4AngleStep", 0);
        _respawningWallScw = GetValue("RespawningWallScw", 0);
        _respawningWallMesh = GetValue("RespawningWallMesh", 0);
        _respawningWallAngleStep = GetIntegerValue("RespawningWallAngleStep", 0);
        _respawningForestScw = GetValue("RespawningForestScw", 0);
        _forestScw = GetValue("ForestScw", 0);
        _destructableScw = GetValue("DestructableScw", 0);
        _destructableMesh = GetValue("DestructableMesh", 0);
        _destructableAngleStep = GetIntegerValue("DestructableAngleStep", 0);
        _destructableScwCn = GetValue("DestructableScwCn", 0);
        _destructableMeshCn = GetValue("DestructableMeshCn", 0);
        _destructableAngleStepCn = GetIntegerValue("DestructableAngleStepCn", 0);
        _fragileScw = GetValue("FragileScw", 0);
        _fragileMesh = GetValue("FragileMesh", 0);
        _fragileAngleStep = GetIntegerValue("FragileAngleStep", 0);
        _fragileScwCn = GetValue("FragileScwCn", 0);
        _fragileMeshCn = GetValue("FragileMeshCn", 0);
        _fragileAngleStepCn = GetIntegerValue("FragileAngleStepCn", 0);
        _waterTileScw = GetValue("WaterTileScw", 0);
        _fenceScw = GetValue("FenceScw", 0);
        _indestructibleScw = GetValue("IndestructibleScw", 0);
        _indestructibleMesh = GetValue("IndestructibleMesh", 0);
        _benchScw = GetValue("BenchScw", 0);
        _laserBallSkinOverride = GetValue("LaserBallSkinOverride", 0);
        _mineGemSpawnScwOverride = GetValue("MineGemSpawnScwOverride", 0);
        _lootBoxSkinOverride = GetValue("LootBoxSkinOverride", 0);
        _showdownBoostScwOverride = GetValue("ShowdownBoostScwOverride", 0);
        _mapPreviewBgColorRed = GetIntegerValue("MapPreviewBGColorRed", 0);
        _mapPreviewBgColorGreen = GetIntegerValue("MapPreviewBGColorGreen", 0);
        _mapPreviewBgColorBlue = GetIntegerValue("MapPreviewBGColorBlue", 0);
        _mapPreviewGemGrabSpawnHoleExportName = GetValue("MapPreviewGemGrabSpawnHoleExportName", 0);
        _mapPreviewBallExportName = GetValue("MapPreviewBallExportName", 0);
        _mapPreviewGoal1ExportName = GetValue("MapPreviewGoal1ExportName", 0);
        _mapPreviewGoal2ExportName = GetValue("MapPreviewGoal2ExportName", 0);
        _mapPreviewCnOverrides = GetValue("MapPreviewCNOverrides", 0);
        _mapWidth = GetIntegerValue("MapWidth", 0);
        _mapHeight = GetIntegerValue("MapHeight", 0);
    }

    public string GetTileSetPrefix()
    {
        return _tileSetPrefix;
    }

    public string GetMaskedEnvironmentScw()
    {
        return _maskedEnvironmentScw;
    }

    public string GetBlocking1Scw()
    {
        return _blocking1Scw;
    }

    public string GetBlocking1Mesh()
    {
        return _blocking1Mesh;
    }

    public int GetBlocking1AngleStep()
    {
        return _blocking1AngleStep;
    }

    public string GetBlocking2Scw()
    {
        return _blocking2Scw;
    }

    public string GetBlocking2Mesh()
    {
        return _blocking2Mesh;
    }

    public int GetBlocking2AngleStep()
    {
        return _blocking2AngleStep;
    }

    public string GetBlocking3Scw()
    {
        return _blocking3Scw;
    }

    public string GetBlocking3Mesh()
    {
        return _blocking3Mesh;
    }

    public int GetBlocking3AngleStep()
    {
        return _blocking3AngleStep;
    }

    public string GetBlocking4Scw()
    {
        return _blocking4Scw;
    }

    public string GetBlocking4Mesh()
    {
        return _blocking4Mesh;
    }

    public int GetBlocking4AngleStep()
    {
        return _blocking4AngleStep;
    }

    public string GetRespawningWallScw()
    {
        return _respawningWallScw;
    }

    public string GetRespawningWallMesh()
    {
        return _respawningWallMesh;
    }

    public int GetRespawningWallAngleStep()
    {
        return _respawningWallAngleStep;
    }

    public string GetRespawningForestScw()
    {
        return _respawningForestScw;
    }

    public string GetForestScw()
    {
        return _forestScw;
    }

    public string GetDestructableScw()
    {
        return _destructableScw;
    }

    public string GetDestructableMesh()
    {
        return _destructableMesh;
    }

    public int GetDestructableAngleStep()
    {
        return _destructableAngleStep;
    }

    public string GetDestructableScwCn()
    {
        return _destructableScwCn;
    }

    public string GetDestructableMeshCn()
    {
        return _destructableMeshCn;
    }

    public int GetDestructableAngleStepCn()
    {
        return _destructableAngleStepCn;
    }

    public string GetFragileScw()
    {
        return _fragileScw;
    }

    public string GetFragileMesh()
    {
        return _fragileMesh;
    }

    public int GetFragileAngleStep()
    {
        return _fragileAngleStep;
    }

    public string GetFragileScwCn()
    {
        return _fragileScwCn;
    }

    public string GetFragileMeshCn()
    {
        return _fragileMeshCn;
    }

    public int GetFragileAngleStepCn()
    {
        return _fragileAngleStepCn;
    }

    public string GetWaterTileScw()
    {
        return _waterTileScw;
    }

    public string GetFenceScw()
    {
        return _fenceScw;
    }

    public string GetIndestructibleScw()
    {
        return _indestructibleScw;
    }

    public string GetIndestructibleMesh()
    {
        return _indestructibleMesh;
    }

    public string GetBenchScw()
    {
        return _benchScw;
    }

    public string GetLaserBallSkinOverride()
    {
        return _laserBallSkinOverride;
    }

    public string GetMineGemSpawnScwOverride()
    {
        return _mineGemSpawnScwOverride;
    }

    public string GetLootBoxSkinOverride()
    {
        return _lootBoxSkinOverride;
    }

    public string GetShowdownBoostScwOverride()
    {
        return _showdownBoostScwOverride;
    }

    public int GetMapPreviewBgColorRed()
    {
        return _mapPreviewBgColorRed;
    }

    public int GetMapPreviewBgColorGreen()
    {
        return _mapPreviewBgColorGreen;
    }

    public int GetMapPreviewBgColorBlue()
    {
        return _mapPreviewBgColorBlue;
    }

    public string GetMapPreviewGemGrabSpawnHoleExportName()
    {
        return _mapPreviewGemGrabSpawnHoleExportName;
    }

    public string GetMapPreviewBallExportName()
    {
        return _mapPreviewBallExportName;
    }

    public string GetMapPreviewGoal1ExportName()
    {
        return _mapPreviewGoal1ExportName;
    }

    public string GetMapPreviewGoal2ExportName()
    {
        return _mapPreviewGoal2ExportName;
    }

    public string GetMapPreviewCnOverrides()
    {
        return _mapPreviewCnOverrides;
    }

    public int GetMapWidth()
    {
        return _mapWidth;
    }

    public int GetMapHeight()
    {
        return _mapHeight;
    }
}