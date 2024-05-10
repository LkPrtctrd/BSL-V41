using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicTileData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private string _baseBulletHole1 = null!;
    private string _baseBulletHole2 = null!;
    private string _baseCrack1 = null!;
    private string _baseCrack2 = null!;
    private string _baseExplosionEffect = null!;
    private string _baseExportName = null!;
    private string _baseHitEffect = null!;
    private string _baseWindEffect = null!;
    private bool _blocksMovement;
    private bool _blocksProjectiles;
    private int _collisionMargin;
    private int _customAngleStep;
    private string _customMesh = null!;
    private string _customScw = null!;
    private int _dynamicCode;
    private bool _hasHitAnim;
    private bool _hasWindAnim;
    private bool _hidesHero;
    private bool _isDestructible;
    private bool _isDestructibleNormalWeapon;
    private int _lifetime;
    private int _respawnSeconds;
    private int _shadowScaleX;
    private int _shadowScaleY;
    private int _shadowSkew;
    private int _shadowX;
    private int _shadowY;
    private int _sortOffset;
    private string _tileCode = null!;

    // LogicTileData.

    public override void CreateReferences()
    {
        _tileCode = GetValue("TileCode", 0);
        _dynamicCode = GetIntegerValue("DynamicCode", 0);
        _blocksMovement = GetBooleanValue("BlocksMovement", 0);
        _blocksProjectiles = GetBooleanValue("BlocksProjectiles", 0);
        _isDestructible = GetBooleanValue("IsDestructible", 0);
        _isDestructibleNormalWeapon = GetBooleanValue("IsDestructibleNormalWeapon", 0);
        _hidesHero = GetBooleanValue("HidesHero", 0);
        _respawnSeconds = GetIntegerValue("RespawnSeconds", 0);
        _collisionMargin = GetIntegerValue("CollisionMargin", 0);
        _baseExportName = GetValue("BaseExportName", 0);
        _baseExplosionEffect = GetValue("BaseExplosionEffect", 0);
        _baseHitEffect = GetValue("BaseHitEffect", 0);
        _baseWindEffect = GetValue("BaseWindEffect", 0);
        _baseBulletHole1 = GetValue("BaseBulletHole1", 0);
        _baseBulletHole2 = GetValue("BaseBulletHole2", 0);
        _baseCrack1 = GetValue("BaseCrack1", 0);
        _baseCrack2 = GetValue("BaseCrack2", 0);
        _sortOffset = GetIntegerValue("SortOffset", 0);
        _hasHitAnim = GetBooleanValue("HasHitAnim", 0);
        _hasWindAnim = GetBooleanValue("HasWindAnim", 0);
        _shadowScaleX = GetIntegerValue("ShadowScaleX", 0);
        _shadowScaleY = GetIntegerValue("ShadowScaleY", 0);
        _shadowX = GetIntegerValue("ShadowX", 0);
        _shadowY = GetIntegerValue("ShadowY", 0);
        _shadowSkew = GetIntegerValue("ShadowSkew", 0);
        _lifetime = GetIntegerValue("Lifetime", 0);
        _customScw = GetValue("CustomSCW", 0);
        _customMesh = GetValue("CustomMesh", 0);
        _customAngleStep = GetIntegerValue("CustomAngleStep", 0);
    }

    public char GetTileCode()
    {
        return _tileCode[0];
    }

    public int GetDynamicCode()
    {
        return _dynamicCode;
    }

    public bool GetBlocksMovement()
    {
        return _blocksMovement;
    }

    public bool GetBlocksProjectiles()
    {
        return _blocksProjectiles;
    }

    public bool GetIsDestructible()
    {
        return _isDestructible;
    }

    public bool GetIsDestructibleNormalWeapon()
    {
        return _isDestructibleNormalWeapon;
    }

    public bool GetHidesHero()
    {
        return _hidesHero;
    }

    public int GetRespawnSeconds()
    {
        return _respawnSeconds;
    }

    public int GetCollisionMargin()
    {
        return _collisionMargin;
    }

    public string GetBaseExportName()
    {
        return _baseExportName;
    }

    public string GetBaseExplosionEffect()
    {
        return _baseExplosionEffect;
    }

    public string GetBaseHitEffect()
    {
        return _baseHitEffect;
    }

    public string GetBaseWindEffect()
    {
        return _baseWindEffect;
    }

    public string GetBaseBulletHole1()
    {
        return _baseBulletHole1;
    }

    public string GetBaseBulletHole2()
    {
        return _baseBulletHole2;
    }

    public string GetBaseCrack1()
    {
        return _baseCrack1;
    }

    public string GetBaseCrack2()
    {
        return _baseCrack2;
    }

    public int GetSortOffset()
    {
        return _sortOffset;
    }

    public bool GetHasHitAnim()
    {
        return _hasHitAnim;
    }

    public bool GetHasWindAnim()
    {
        return _hasWindAnim;
    }

    public int GetShadowScaleX()
    {
        return _shadowScaleX;
    }

    public int GetShadowScaleY()
    {
        return _shadowScaleY;
    }

    public int GetShadowX()
    {
        return _shadowX;
    }

    public int GetShadowY()
    {
        return _shadowY;
    }

    public int GetShadowSkew()
    {
        return _shadowSkew;
    }

    public int GetLifetime()
    {
        return _lifetime;
    }

    public string GetCustomScw()
    {
        return _customScw;
    }

    public string GetCustomMesh()
    {
        return _customMesh;
    }

    public int GetCustomAngleStep()
    {
        return _customAngleStep;
    }
}