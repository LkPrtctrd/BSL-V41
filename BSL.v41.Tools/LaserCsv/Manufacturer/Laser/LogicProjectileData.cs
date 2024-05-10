using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicProjectileData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private bool _attractsPet;
    private bool _blockUltiCharge;
    private int _blueAdd;
    private string _blueExportName = null!;
    private string _blueGroundGlowExportName = null!;
    private int _blueMul;
    private string _blueScw = null!;
    private int _bouncePercent;
    private string _cancelEffect = null!;
    private bool _canGrowStronger;
    private bool _canHitAgainAfterBounce;
    private string _chainBullet = null!;
    private int _chainBullets;
    private int _chainSpread;
    private int _chainsToEnemies;
    private int _chainTravelDistance;
    private bool _constantFlyTime;
    private bool _damageOnlyWithOneProj;
    private int _damagePercentEnd;
    private int _damagePercentStart;
    private int _damagesConstantlyTickDelay;
    private bool _directionAlignedPushback;
    private int _distanceAddFromBounce;
    private int _earlyTicks;
    private int _executeChainSpecialCase;
    private string _fileName = null!;
    private int _freezeDurationMs;
    private int _freezeStrength;
    private bool _grapplesEnemy;
    private int _gravity;
    private int _greenAdd;
    private int _greenMul;
    private bool _groundBasis;
    private int _healOwnPercent;
    private bool _hideFaster;
    private int _hideTime;
    private string _hitEffectChar = null!;
    private string _hitEffectEnv = null!;
    private int _homeDistance;
    private bool _indirect;
    private bool _isBeam;
    private bool _isBoomerang;
    private bool _isBouncing;
    private bool _isFriendlyHomingMissile;
    private bool _isHomingMissile;
    private int _kickBack;
    private int _lifeStealPercent;
    private string _maxRangeReachedEffect = null!;
    private int _minDistanceForSpread;
    private string _parentProjectileForSkin = null!;
    private bool _passesEnvironment;
    private bool _piercesCharacters;
    private bool _piercesEnvironment;
    private bool _piercesEnvironmentLikeButter;
    private int _poisonDamagePercent;
    private int _poisonType;
    private string _preExplosionBlueExportName = null!;
    private string _preExplosionRedExportName = null!;
    private int _preExplosionTimeMs;
    private int _pushbackStrength;
    private int _radius;
    private int _randomStartFrame;
    private int _redAdd;
    private string _redExportName = null!;
    private string _redGroundGlowExportName = null!;
    private int _redMul;
    private string _redScw = null!;
    private string _rendering = null!;
    private int _scale;
    private int _scaleEnd;
    private int _scaleStart;
    private string _shadowExportName = null!;
    private bool _shotByHero;
    private string _spawnAreaEffectObject = null!;
    private string _spawnAreaEffectObject2 = null!;
    private string _spawnCharacter = null!;
    private string _spawnItem = null!;
    private int _speed;
    private int _steerIgnoreTicks;
    private int _steerLifeTime;
    private int _steerStrength;
    private int _stunLengthMs;
    private string _trailEffect = null!;
    private int _travelType;
    private int _triggerWithDelayMs;
    private bool _useColorMod;
    private bool _variablePushback;

    // LogicProjectileData.

    public override void CreateReferences()
    {
        _parentProjectileForSkin = GetValue("ParentProjectileForSkin", 0);
        _speed = GetIntegerValue("Speed", 0);
        _fileName = GetValue("FileName", 0);
        _blueScw = GetValue("BlueSCW", 0);
        _redScw = GetValue("RedSCW", 0);
        _blueExportName = GetValue("BlueExportName", 0);
        _redExportName = GetValue("RedExportName", 0);
        _shadowExportName = GetValue("ShadowExportName", 0);
        _blueGroundGlowExportName = GetValue("BlueGroundGlowExportName", 0);
        _redGroundGlowExportName = GetValue("RedGroundGlowExportName", 0);
        _preExplosionBlueExportName = GetValue("PreExplosionBlueExportName", 0);
        _preExplosionRedExportName = GetValue("PreExplosionRedExportName", 0);
        _preExplosionTimeMs = GetIntegerValue("PreExplosionTimeMs", 0);
        _hitEffectEnv = GetValue("HitEffectEnv", 0);
        _hitEffectChar = GetValue("HitEffectChar", 0);
        _maxRangeReachedEffect = GetValue("MaxRangeReachedEffect", 0);
        _cancelEffect = GetValue("CancelEffect", 0);
        _radius = GetIntegerValue("Radius", 0);
        _indirect = GetBooleanValue("Indirect", 0);
        _constantFlyTime = GetBooleanValue("ConstantFlyTime", 0);
        _triggerWithDelayMs = GetIntegerValue("TriggerWithDelayMs", 0);
        _bouncePercent = GetIntegerValue("BouncePercent", 0);
        _gravity = GetIntegerValue("Gravity", 0);
        _earlyTicks = GetIntegerValue("EarlyTicks", 0);
        _hideTime = GetIntegerValue("HideTime", 0);
        _scale = GetIntegerValue("Scale", 0);
        _randomStartFrame = GetIntegerValue("RandomStartFrame", 0);
        _spawnAreaEffectObject = GetValue("SpawnAreaEffectObject", 0);
        _spawnAreaEffectObject2 = GetValue("SpawnAreaEffectObject2", 0);
        _spawnCharacter = GetValue("SpawnCharacter", 0);
        _spawnItem = GetValue("SpawnItem", 0);
        _trailEffect = GetValue("TrailEffect", 0);
        _shotByHero = GetBooleanValue("ShotByHero", 0);
        _isBeam = GetBooleanValue("IsBeam", 0);
        _isBouncing = GetBooleanValue("IsBouncing", 0);
        _distanceAddFromBounce = GetIntegerValue("DistanceAddFromBounce", 0);
        _rendering = GetValue("Rendering", 0);
        _piercesCharacters = GetBooleanValue("PiercesCharacters", 0);
        _piercesEnvironment = GetBooleanValue("PiercesEnvironment", 0);
        _piercesEnvironmentLikeButter = GetBooleanValue("PiercesEnvironmentLikeButter", 0);
        _pushbackStrength = GetIntegerValue("PushbackStrength", 0);
        _variablePushback = GetBooleanValue("VariablePushback", 0);
        _directionAlignedPushback = GetBooleanValue("DirectionAlignedPushback", 0);
        _chainsToEnemies = GetIntegerValue("ChainsToEnemies", 0);
        _chainBullets = GetIntegerValue("ChainBullets", 0);
        _chainSpread = GetIntegerValue("ChainSpread", 0);
        _chainTravelDistance = GetIntegerValue("ChainTravelDistance", 0);
        _chainBullet = GetValue("ChainBullet", 0);
        _executeChainSpecialCase = GetIntegerValue("ExecuteChainSpecialCase", 0);
        _damagePercentStart = GetIntegerValue("DamagePercentStart", 0);
        _damagePercentEnd = GetIntegerValue("DamagePercentEnd", 0);
        _damagesConstantlyTickDelay = GetIntegerValue("DamagesConstantlyTickDelay", 0);
        _freezeStrength = GetIntegerValue("FreezeStrength", 0);
        _freezeDurationMs = GetIntegerValue("FreezeDurationMS", 0);
        _stunLengthMs = GetIntegerValue("StunLengthMS", 0);
        _scaleStart = GetIntegerValue("ScaleStart", 0);
        _scaleEnd = GetIntegerValue("ScaleEnd", 0);
        _attractsPet = GetBooleanValue("AttractsPet", 0);
        _lifeStealPercent = GetIntegerValue("LifeStealPercent", 0);
        _passesEnvironment = GetBooleanValue("PassesEnvironment", 0);
        _poisonDamagePercent = GetIntegerValue("PoisonDamagePercent", 0);
        _damageOnlyWithOneProj = GetBooleanValue("DamageOnlyWithOneProj", 0);
        _healOwnPercent = GetIntegerValue("HealOwnPercent", 0);
        _canGrowStronger = GetBooleanValue("CanGrowStronger", 0);
        _hideFaster = GetBooleanValue("HideFaster", 0);
        _grapplesEnemy = GetBooleanValue("GrapplesEnemy", 0);
        _kickBack = GetIntegerValue("KickBack", 0);
        _useColorMod = GetBooleanValue("UseColorMod", 0);
        _redAdd = GetIntegerValue("RedAdd", 0);
        _greenAdd = GetIntegerValue("GreenAdd", 0);
        _blueAdd = GetIntegerValue("BlueAdd", 0);
        _redMul = GetIntegerValue("RedMul", 0);
        _greenMul = GetIntegerValue("GreenMul", 0);
        _blueMul = GetIntegerValue("BlueMul", 0);
        _groundBasis = GetBooleanValue("GroundBasis", 0);
        _minDistanceForSpread = GetIntegerValue("MinDistanceForSpread", 0);
        _isFriendlyHomingMissile = GetBooleanValue("IsFriendlyHomingMissile", 0);
        _isBoomerang = GetBooleanValue("IsBoomerang", 0);
        _canHitAgainAfterBounce = GetBooleanValue("CanHitAgainAfterBounce", 0);
        _isHomingMissile = GetBooleanValue("IsHomingMissile", 0);
        _blockUltiCharge = GetBooleanValue("BlockUltiCharge", 0);
        _poisonType = GetIntegerValue("PoisonType", 0);
        _travelType = GetIntegerValue("TravelType", 0);
        _steerStrength = GetIntegerValue("SteerStrength", 0);
        _steerIgnoreTicks = GetIntegerValue("SteerIgnoreTicks", 0);
        _homeDistance = GetIntegerValue("HomeDistance", 0);
        _steerLifeTime = GetIntegerValue("SteerLifeTime", 0);
    }

    public string GetParentProjectileForSkin()
    {
        return _parentProjectileForSkin;
    }

    public int GetSpeed()
    {
        return _speed;
    }

    public string GetFileName()
    {
        return _fileName;
    }

    public string GetBlueScw()
    {
        return _blueScw;
    }

    public string GetRedScw()
    {
        return _redScw;
    }

    public string GetBlueExportName()
    {
        return _blueExportName;
    }

    public string GetRedExportName()
    {
        return _redExportName;
    }

    public string GetShadowExportName()
    {
        return _shadowExportName;
    }

    public string GetBlueGroundGlowExportName()
    {
        return _blueGroundGlowExportName;
    }

    public string GetRedGroundGlowExportName()
    {
        return _redGroundGlowExportName;
    }

    public string GetPreExplosionBlueExportName()
    {
        return _preExplosionBlueExportName;
    }

    public string GetPreExplosionRedExportName()
    {
        return _preExplosionRedExportName;
    }

    public int GetPreExplosionTimeMs()
    {
        return _preExplosionTimeMs;
    }

    public string GetHitEffectEnv()
    {
        return _hitEffectEnv;
    }

    public string GetHitEffectChar()
    {
        return _hitEffectChar;
    }

    public string GetMaxRangeReachedEffect()
    {
        return _maxRangeReachedEffect;
    }

    public string GetCancelEffect()
    {
        return _cancelEffect;
    }

    public int GetRadius()
    {
        return _radius;
    }

    public bool GetIndirect()
    {
        return _indirect;
    }

    public bool GetConstantFlyTime()
    {
        return _constantFlyTime;
    }

    public int GetTriggerWithDelayMs()
    {
        return _triggerWithDelayMs;
    }

    public int GetBouncePercent()
    {
        return _bouncePercent;
    }

    public int GetGravity()
    {
        return _gravity;
    }

    public int GetEarlyTicks()
    {
        return _earlyTicks;
    }

    public int GetHideTime()
    {
        return _hideTime;
    }

    public int GetScale()
    {
        return _scale;
    }

    public int GetRandomStartFrame()
    {
        return _randomStartFrame;
    }

    public string GetSpawnAreaEffectObject()
    {
        return _spawnAreaEffectObject;
    }

    public string GetSpawnAreaEffectObject2()
    {
        return _spawnAreaEffectObject2;
    }

    public string GetSpawnCharacter()
    {
        return _spawnCharacter;
    }

    public string GetSpawnItem()
    {
        return _spawnItem;
    }

    public string GetTrailEffect()
    {
        return _trailEffect;
    }

    public bool GetShotByHero()
    {
        return _shotByHero;
    }

    public bool GetIsBeam()
    {
        return _isBeam;
    }

    public bool GetIsBouncing()
    {
        return _isBouncing;
    }

    public int GetDistanceAddFromBounce()
    {
        return _distanceAddFromBounce;
    }

    public string GetRendering()
    {
        return _rendering;
    }

    public bool GetPiercesCharacters()
    {
        return _piercesCharacters;
    }

    public bool GetPiercesEnvironment()
    {
        return _piercesEnvironment;
    }

    public bool GetPiercesEnvironmentLikeButter()
    {
        return _piercesEnvironmentLikeButter;
    }

    public int GetPushbackStrength()
    {
        return _pushbackStrength;
    }

    public bool GetVariablePushback()
    {
        return _variablePushback;
    }

    public bool GetDirectionAlignedPushback()
    {
        return _directionAlignedPushback;
    }

    public int GetChainsToEnemies()
    {
        return _chainsToEnemies;
    }

    public int GetChainBullets()
    {
        return _chainBullets;
    }

    public int GetChainSpread()
    {
        return _chainSpread;
    }

    public int GetChainTravelDistance()
    {
        return _chainTravelDistance;
    }

    public string GetChainBullet()
    {
        return _chainBullet;
    }

    public int GetExecuteChainSpecialCase()
    {
        return _executeChainSpecialCase;
    }

    public int GetDamagePercentStart()
    {
        return _damagePercentStart;
    }

    public int GetDamagePercentEnd()
    {
        return _damagePercentEnd;
    }

    public int GetDamagesConstantlyTickDelay()
    {
        return _damagesConstantlyTickDelay;
    }

    public int GetFreezeStrength()
    {
        return _freezeStrength;
    }

    public int GetFreezeDurationMs()
    {
        return _freezeDurationMs;
    }

    public int GetStunLengthMs()
    {
        return _stunLengthMs;
    }

    public int GetScaleStart()
    {
        return _scaleStart;
    }

    public int GetScaleEnd()
    {
        return _scaleEnd;
    }

    public bool GetAttractsPet()
    {
        return _attractsPet;
    }

    public int GetLifeStealPercent()
    {
        return _lifeStealPercent;
    }

    public bool GetPassesEnvironment()
    {
        return _passesEnvironment;
    }

    public int GetPoisonDamagePercent()
    {
        return _poisonDamagePercent;
    }

    public bool GetDamageOnlyWithOneProj()
    {
        return _damageOnlyWithOneProj;
    }

    public int GetHealOwnPercent()
    {
        return _healOwnPercent;
    }

    public bool GetCanGrowStronger()
    {
        return _canGrowStronger;
    }

    public bool GetHideFaster()
    {
        return _hideFaster;
    }

    public bool GetGrapplesEnemy()
    {
        return _grapplesEnemy;
    }

    public int GetKickBack()
    {
        return _kickBack;
    }

    public bool GetUseColorMod()
    {
        return _useColorMod;
    }

    public int GetRedAdd()
    {
        return _redAdd;
    }

    public int GetGreenAdd()
    {
        return _greenAdd;
    }

    public int GetBlueAdd()
    {
        return _blueAdd;
    }

    public int GetRedMul()
    {
        return _redMul;
    }

    public int GetGreenMul()
    {
        return _greenMul;
    }

    public int GetBlueMul()
    {
        return _blueMul;
    }

    public bool GetGroundBasis()
    {
        return _groundBasis;
    }

    public int GetMinDistanceForSpread()
    {
        return _minDistanceForSpread;
    }

    public bool GetIsFriendlyHomingMissile()
    {
        return _isFriendlyHomingMissile;
    }

    public bool GetIsBoomerang()
    {
        return _isBoomerang;
    }

    public bool GetCanHitAgainAfterBounce()
    {
        return _canHitAgainAfterBounce;
    }

    public bool GetIsHomingMissile()
    {
        return _isHomingMissile;
    }

    public bool GetBlockUltiCharge()
    {
        return _blockUltiCharge;
    }

    public int GetPoisonType()
    {
        return _poisonType;
    }

    public int GetTravelType()
    {
        return _travelType;
    }

    public int GetSteerStrength()
    {
        return _steerStrength;
    }

    public int GetSteerIgnoreTicks()
    {
        return _steerIgnoreTicks;
    }

    public int GetHomeDistance()
    {
        return _homeDistance;
    }

    public int GetSteerLifeTime()
    {
        return _steerLifeTime;
    }
}