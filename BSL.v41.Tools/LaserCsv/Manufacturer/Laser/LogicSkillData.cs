using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicSkillData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private int _activeTime;
    private bool _alwaysCastAtMaxRange;
    private string _areaEffectObject = null!;
    private string _areaEffectObject2 = null!;
    private string _attackEffect = null!;
    private int _attackPattern;
    private string _behaviorType = null!;
    private bool _breakInvisibilityOnAttack;
    private string _buttonExportName = null!;
    private string _buttonSwf = null!;
    private bool _canAutoShoot;
    private bool _canMoveAtSameTime;
    private int _castingRange;
    private int _castingTime;
    private int _chargedShotCount;
    private string _chargeMoveSound = null!;
    private int _chargePushback;
    private int _chargeSpeed;
    private int _chargeType;
    private int _cooldown;
    private int _damage;
    private int _damageModifier;
    private string _endEffect = null!;
    private bool _executeFirstAttackImmediately;
    private bool _faceMovement;
    private int _forceValidTile;
    private string _iconSwf = null!;
    private string _largeIconExportName = null!;
    private string _largeIconSwf = null!;
    private string _loopEffect = null!;
    private string _loopEffect2 = null!;
    private int _maxCastingRange;
    private int _maxCharge;
    private int _maxSpawns;
    private int _msBetweenAttacks;
    private bool _multiShot;
    private int _numBulletsInOneAttack;
    private int _numSpawns;
    private string _projectile = null!;
    private int _rangeInputScale;
    private int _rangeVisual;
    private int _rechargeTime;
    private string _secondaryProjectile = null!;
    private int _seeInvisibilityDistance;
    private bool _showTimerBar;
    private bool _skillCanChange;
    private string _spawnedItem = null!;
    private int _spread;
    private string _summonedCharacter = null!;
    private bool _targeted;
    private bool _twoGuns;
    private string _useEffect = null!;

    // LogicSkillData.

    public override void CreateReferences()
    {
        _behaviorType = GetValue("BehaviorType", 0);
        _canMoveAtSameTime = GetBooleanValue("CanMoveAtSameTime", 0);
        _targeted = GetBooleanValue("Targeted", 0);
        _canAutoShoot = GetBooleanValue("CanAutoShoot", 0);
        _faceMovement = GetBooleanValue("FaceMovement", 0);
        _cooldown = GetIntegerValue("Cooldown", 0);
        _activeTime = GetIntegerValue("ActiveTime", 0);
        _castingTime = GetIntegerValue("CastingTime", 0);
        _castingRange = GetIntegerValue("CastingRange", 0);
        _rangeVisual = GetIntegerValue("RangeVisual", 0);
        _rangeInputScale = GetIntegerValue("RangeInputScale", 0);
        _maxCastingRange = GetIntegerValue("MaxCastingRange", 0);
        _forceValidTile = GetIntegerValue("ForceValidTile", 0);
        _rechargeTime = GetIntegerValue("RechargeTime", 0);
        _maxCharge = GetIntegerValue("MaxCharge", 0);
        _damage = GetIntegerValue("Damage", 0);
        _msBetweenAttacks = GetIntegerValue("MsBetweenAttacks", 0);
        _spread = GetIntegerValue("Spread", 0);
        _attackPattern = GetIntegerValue("AttackPattern", 0);
        _numBulletsInOneAttack = GetIntegerValue("NumBulletsInOneAttack", 0);
        _twoGuns = GetBooleanValue("TwoGuns", 0);
        _executeFirstAttackImmediately = GetBooleanValue("ExecuteFirstAttackImmediately", 0);
        _chargePushback = GetIntegerValue("ChargePushback", 0);
        _chargeSpeed = GetIntegerValue("ChargeSpeed", 0);
        _chargeType = GetIntegerValue("ChargeType", 0);
        _numSpawns = GetIntegerValue("NumSpawns", 0);
        _maxSpawns = GetIntegerValue("MaxSpawns", 0);
        _breakInvisibilityOnAttack = GetBooleanValue("BreakInvisibilityOnAttack", 0);
        _seeInvisibilityDistance = GetIntegerValue("SeeInvisibilityDistance", 0);
        _alwaysCastAtMaxRange = GetBooleanValue("AlwaysCastAtMaxRange", 0);
        _projectile = GetValue("Projectile", 0);
        _summonedCharacter = GetValue("SummonedCharacter", 0);
        _areaEffectObject = GetValue("AreaEffectObject", 0);
        _areaEffectObject2 = GetValue("AreaEffectObject2", 0);
        _spawnedItem = GetValue("SpawnedItem", 0);
        _iconSwf = GetValue("IconSWF", 0);
        _largeIconSwf = GetValue("LargeIconSWF", 0);
        _largeIconExportName = GetValue("LargeIconExportName", 0);
        _buttonSwf = GetValue("ButtonSWF", 0);
        _buttonExportName = GetValue("ButtonExportName", 0);
        _attackEffect = GetValue("AttackEffect", 0);
        _useEffect = GetValue("UseEffect", 0);
        _endEffect = GetValue("EndEffect", 0);
        _loopEffect = GetValue("LoopEffect", 0);
        _loopEffect2 = GetValue("LoopEffect2", 0);
        _chargeMoveSound = GetValue("ChargeMoveSound", 0);
        _multiShot = GetBooleanValue("MultiShot", 0);
        _skillCanChange = GetBooleanValue("SkillCanChange", 0);
        _showTimerBar = GetBooleanValue("ShowTimerBar", 0);
        _secondaryProjectile = GetValue("SecondaryProjectile", 0);
        _chargedShotCount = GetIntegerValue("ChargedShotCount", 0);
        _damageModifier = GetIntegerValue("DamageModifier", 0);
    }

    public string GetBehaviorType()
    {
        return _behaviorType;
    }

    public bool GetCanMoveAtSameTime()
    {
        return _canMoveAtSameTime;
    }

    public bool GetTargeted()
    {
        return _targeted;
    }

    public bool GetCanAutoShoot()
    {
        return _canAutoShoot;
    }

    public bool GetFaceMovement()
    {
        return _faceMovement;
    }

    public int GetCooldown()
    {
        return _cooldown;
    }

    public int GetActiveTime()
    {
        return _activeTime;
    }

    public int GetCastingTime()
    {
        return _castingTime;
    }

    public int GetCastingRange()
    {
        return _castingRange;
    }

    public int GetRangeVisual()
    {
        return _rangeVisual;
    }

    public int GetRangeInputScale()
    {
        return _rangeInputScale;
    }

    public int GetMaxCastingRange()
    {
        return _maxCastingRange;
    }

    public int GetForceValidTile()
    {
        return _forceValidTile;
    }

    public int GetRechargeTime()
    {
        return _rechargeTime;
    }

    public int GetMaxCharge()
    {
        return _maxCharge;
    }

    public int GetDamage()
    {
        return _damage;
    }

    public int GetMsBetweenAttacks()
    {
        return _msBetweenAttacks;
    }

    public int GetSpread()
    {
        return _spread;
    }

    public int GetAttackPattern()
    {
        return _attackPattern;
    }

    public int GetNumBulletsInOneAttack()
    {
        return _numBulletsInOneAttack;
    }

    public bool GetTwoGuns()
    {
        return _twoGuns;
    }

    public bool GetExecuteFirstAttackImmediately()
    {
        return _executeFirstAttackImmediately;
    }

    public int GetChargePushback()
    {
        return _chargePushback;
    }

    public int GetChargeSpeed()
    {
        return _chargeSpeed;
    }

    public int GetChargeType()
    {
        return _chargeType;
    }

    public int GetNumSpawns()
    {
        return _numSpawns;
    }

    public int GetMaxSpawns()
    {
        return _maxSpawns;
    }

    public bool GetBreakInvisibilityOnAttack()
    {
        return _breakInvisibilityOnAttack;
    }

    public int GetSeeInvisibilityDistance()
    {
        return _seeInvisibilityDistance;
    }

    public bool GetAlwaysCastAtMaxRange()
    {
        return _alwaysCastAtMaxRange;
    }

    public string GetProjectile()
    {
        return _projectile;
    }

    public string GetSummonedCharacter()
    {
        return _summonedCharacter;
    }

    public string GetAreaEffectObject()
    {
        return _areaEffectObject;
    }

    public string GetAreaEffectObject2()
    {
        return _areaEffectObject2;
    }

    public string GetSpawnedItem()
    {
        return _spawnedItem;
    }

    public string GetIconSwf()
    {
        return _iconSwf;
    }

    public string GetLargeIconSwf()
    {
        return _largeIconSwf;
    }

    public string GetLargeIconExportName()
    {
        return _largeIconExportName;
    }

    public string GetButtonSwf()
    {
        return _buttonSwf;
    }

    public string GetButtonExportName()
    {
        return _buttonExportName;
    }

    public string GetAttackEffect()
    {
        return _attackEffect;
    }

    public string GetUseEffect()
    {
        return _useEffect;
    }

    public string GetEndEffect()
    {
        return _endEffect;
    }

    public string GetLoopEffect()
    {
        return _loopEffect;
    }

    public string GetLoopEffect2()
    {
        return _loopEffect2;
    }

    public string GetChargeMoveSound()
    {
        return _chargeMoveSound;
    }

    public bool GetMultiShot()
    {
        return _multiShot;
    }

    public bool GetSkillCanChange()
    {
        return _skillCanChange;
    }

    public bool GetShowTimerBar()
    {
        return _showTimerBar;
    }

    public string GetSecondaryProjectile()
    {
        return _secondaryProjectile;
    }

    public int GetChargedShotCount()
    {
        return _chargedShotCount;
    }

    public int GetDamageModifier()
    {
        return _damageModifier;
    }
}