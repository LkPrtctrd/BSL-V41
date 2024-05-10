using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicCharacterData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private string _areaEffect = null!;
    private int _attackingWeaponScale;
    private string _attackSoundVo = null!;
    private int _attackStartEffectOffset;
    private int _autoAttackBulletsPerShot;
    private int _autoAttackDamage;
    private string _autoAttackMode = null!;
    private string _autoAttackProjectile = null!;
    private int _autoAttackProjectileSpread;
    private int _autoAttackRange;
    private int _autoAttackSpeedMs;
    private string _autoAttackStartEffect = null!;
    private int _blueAdd;
    private string _blueExportName = null!;
    private int _blueMul;
    private string _boneEffect1 = null!;
    private string _boneEffect2 = null!;
    private string _boneEffect3 = null!;
    private string _boneEffect4 = null!;
    private string _boneEffectUse = null!;
    private bool _canWalkOverWater;
    private int _chargeUltiAutomatically;
    private int _collisionRadius;
    private int _commonSetUpgradeBonus;
    private int _damagerPercentFromAliens;
    private string _deathAreaEffect = null!;
    private string _deathEffect = null!;
    private string _deathSoundVo = null!;
    private string _defaultSkin = null!;
    private int _defenseRating;
    private int _differentFootstepOffset;
    private bool _disabled;
    private string _dryFireEffect = null!;
    private int _endScreenScale;
    private int _extraMinions;
    private string _fileName = null!;
    private int _fitToBoxScale;
    private int _flyingHeight;
    private string _footstepClip = null!;
    private int _footstepIntervalMs;
    private bool _forceAttackAnimationToEnd;
    private int _gatchaScreenScale;
    private int _greenAdd;
    private int _greenMul;
    private string _healthBar = null!;
    private int _healthBarOffsetY;
    private int _heroScreenScale;
    private int _heroScreenXOffset;
    private int _heroScreenZOffset;
    private int _hitpoints;
    private int _homeScreenScale;
    private string _homeworld = null!;
    private string _iconSwf = null!;
    private string _inLeadCelebrationSoundVo = null!;
    private string _itemName = null!;
    private string _killCelebrationSoundVo = null!;
    private bool _lockedForChronos;
    private string _loopedEffect = null!;
    private string _loopedEffect2 = null!;
    private bool _meleeAutoAttackSplashDamage;
    private string _meleeHitEffect = null!;
    private string _moveEffect = null!;
    private int _offenseRating;
    private string _outOfAmmoEffect = null!;
    private string _pet = null!;
    private int _petAutoSpawnDelay;
    private int _projectileStartZ;
    private int _rareSetUpgradeBonus;
    private int _recoilAmount;
    private int _redAdd;
    private string _redExportName = null!;
    private int _redMul;
    private int _regeneratePerSecond;
    private string _reloadEffect = null!;
    private int _scale;
    private bool _secondaryPet;
    private string _shadowExportName = null!;
    private int _shadowScaleX;
    private int _shadowScaleY;
    private int _shadowSkew;
    private int _shadowX;
    private int _shadowY;
    private bool _shouldEncodePetStatus;
    private string _spawnEffect = null!;
    private int _speed;
    private string _startSoundVo = null!;
    private int _stopMovementAfterMs;
    private int _superRareSetUpgradeBonus;
    private string _takeDamageEffect = null!;
    private string _takeDamageSoundVo = null!;
    private int _twoWeaponAttackEffectOffset;
    private string _type = null!;
    private int _ultiChargeMul;
    private int _ultiChargeUltiMul;
    private string _ultimateSkill = null!;
    private bool _useColorMod;
    private bool _useThrowingLeftWeaponBoneScaling;
    private bool _useThrowingRightWeaponBoneScaling;
    private string _useUltiSoundVo = null!;
    private int _utilityRating;
    private string _videoLink = null!;
    private int _waitMs;
    private string _weaponSkill = null!;

    // LogicCharacterData.

    public override void CreateReferences()
    {
        _lockedForChronos = GetBooleanValue("LockedForChronos", 0);
        _disabled = GetBooleanValue("Disabled", 0);
        _itemName = GetValue("ItemName", 0);
        _weaponSkill = GetValue("WeaponSkill", 0);
        _ultimateSkill = GetValue("UltimateSkill", 0);
        _pet = GetValue("Pet", 0);
        _speed = GetIntegerValue("Speed", 0);
        _hitpoints = GetIntegerValue("Hitpoints", 0);
        _meleeAutoAttackSplashDamage = GetBooleanValue("MeleeAutoAttackSplashDamage", 0);
        _autoAttackSpeedMs = GetIntegerValue("AutoAttackSpeedMs", 0);
        _autoAttackDamage = GetIntegerValue("AutoAttackDamage", 0);
        _autoAttackBulletsPerShot = GetIntegerValue("AutoAttackBulletsPerShot", 0);
        _autoAttackMode = GetValue("AutoAttackMode", 0);
        _autoAttackProjectileSpread = GetIntegerValue("AutoAttackProjectileSpread", 0);
        _autoAttackProjectile = GetValue("AutoAttackProjectile", 0);
        _autoAttackRange = GetIntegerValue("AutoAttackRange", 0);
        _regeneratePerSecond = GetIntegerValue("RegeneratePerSecond", 0);
        _ultiChargeMul = GetIntegerValue("UltiChargeMul", 0);
        _ultiChargeUltiMul = GetIntegerValue("UltiChargeUltiMul", 0);
        _type = GetValue("Type", 0);
        _damagerPercentFromAliens = GetIntegerValue("DamagerPercentFromAliens", 0);
        _defaultSkin = GetValue("DefaultSkin", 0);
        _fileName = GetValue("FileName", 0);
        _blueExportName = GetValue("BlueExportName", 0);
        _redExportName = GetValue("RedExportName", 0);
        _shadowExportName = GetValue("ShadowExportName", 0);
        _areaEffect = GetValue("AreaEffect", 0);
        _deathAreaEffect = GetValue("DeathAreaEffect", 0);
        _takeDamageEffect = GetValue("TakeDamageEffect", 0);
        _deathEffect = GetValue("DeathEffect", 0);
        _moveEffect = GetValue("MoveEffect", 0);
        _reloadEffect = GetValue("ReloadEffect", 0);
        _outOfAmmoEffect = GetValue("OutOfAmmoEffect", 0);
        _dryFireEffect = GetValue("DryFireEffect", 0);
        _spawnEffect = GetValue("SpawnEffect", 0);
        _meleeHitEffect = GetValue("MeleeHitEffect", 0);
        _autoAttackStartEffect = GetValue("AutoAttackStartEffect", 0);
        _boneEffect1 = GetValue("BoneEffect1", 0);
        _boneEffect2 = GetValue("BoneEffect2", 0);
        _boneEffect3 = GetValue("BoneEffect3", 0);
        _boneEffect4 = GetValue("BoneEffect4", 0);
        _boneEffectUse = GetValue("BoneEffectUse", 0);
        _loopedEffect = GetValue("LoopedEffect", 0);
        _loopedEffect2 = GetValue("LoopedEffect2", 0);
        _killCelebrationSoundVo = GetValue("KillCelebrationSoundVO", 0);
        _inLeadCelebrationSoundVo = GetValue("InLeadCelebrationSoundVO", 0);
        _startSoundVo = GetValue("StartSoundVO", 0);
        _useUltiSoundVo = GetValue("UseUltiSoundVO", 0);
        _takeDamageSoundVo = GetValue("TakeDamageSoundVO", 0);
        _deathSoundVo = GetValue("DeathSoundVO", 0);
        _attackSoundVo = GetValue("AttackSoundVO", 0);
        _attackStartEffectOffset = GetIntegerValue("AttackStartEffectOffset", 0);
        _twoWeaponAttackEffectOffset = GetIntegerValue("TwoWeaponAttackEffectOffset", 0);
        _shadowScaleX = GetIntegerValue("ShadowScaleX", 0);
        _shadowScaleY = GetIntegerValue("ShadowScaleY", 0);
        _shadowX = GetIntegerValue("ShadowX", 0);
        _shadowY = GetIntegerValue("ShadowY", 0);
        _shadowSkew = GetIntegerValue("ShadowSkew", 0);
        _scale = GetIntegerValue("Scale", 0);
        _heroScreenScale = GetIntegerValue("HeroScreenScale", 0);
        _fitToBoxScale = GetIntegerValue("FitToBoxScale", 0);
        _endScreenScale = GetIntegerValue("EndScreenScale", 0);
        _gatchaScreenScale = GetIntegerValue("GatchaScreenScale", 0);
        _homeScreenScale = GetIntegerValue("HomeScreenScale", 0);
        _heroScreenXOffset = GetIntegerValue("HeroScreenXOffset", 0);
        _heroScreenZOffset = GetIntegerValue("HeroScreenZOffset", 0);
        _collisionRadius = GetIntegerValue("CollisionRadius", 0);
        _healthBar = GetValue("HealthBar", 0);
        _healthBarOffsetY = GetIntegerValue("HealthBarOffsetY", 0);
        _flyingHeight = GetIntegerValue("FlyingHeight", 0);
        _projectileStartZ = GetIntegerValue("ProjectileStartZ", 0);
        _stopMovementAfterMs = GetIntegerValue("StopMovementAfterMS", 0);
        _waitMs = GetIntegerValue("WaitMS", 0);
        _forceAttackAnimationToEnd = GetBooleanValue("ForceAttackAnimationToEnd", 0);
        _iconSwf = GetValue("IconSWF", 0);
        _recoilAmount = GetIntegerValue("RecoilAmount", 0);
        _homeworld = GetValue("Homeworld", 0);
        _footstepClip = GetValue("FootstepClip", 0);
        _differentFootstepOffset = GetIntegerValue("DifferentFootstepOffset", 0);
        _footstepIntervalMs = GetIntegerValue("FootstepIntervalMS", 0);
        _attackingWeaponScale = GetIntegerValue("AttackingWeaponScale", 0);
        _useThrowingLeftWeaponBoneScaling = GetBooleanValue("UseThrowingLeftWeaponBoneScaling", 0);
        _useThrowingRightWeaponBoneScaling = GetBooleanValue("UseThrowingRightWeaponBoneScaling", 0);
        _commonSetUpgradeBonus = GetIntegerValue("CommonSetUpgradeBonus", 0);
        _rareSetUpgradeBonus = GetIntegerValue("RareSetUpgradeBonus", 0);
        _superRareSetUpgradeBonus = GetIntegerValue("SuperRareSetUpgradeBonus", 0);
        _canWalkOverWater = GetBooleanValue("CanWalkOverWater", 0);
        _useColorMod = GetBooleanValue("UseColorMod", 0);
        _redAdd = GetIntegerValue("RedAdd", 0);
        _greenAdd = GetIntegerValue("GreenAdd", 0);
        _blueAdd = GetIntegerValue("BlueAdd", 0);
        _redMul = GetIntegerValue("RedMul", 0);
        _greenMul = GetIntegerValue("GreenMul", 0);
        _blueMul = GetIntegerValue("BlueMul", 0);
        _chargeUltiAutomatically = GetIntegerValue("ChargeUltiAutomatically", 0);
        _videoLink = GetValue("VideoLink", 0);
        _shouldEncodePetStatus = GetBooleanValue("ShouldEncodePetStatus", 0);
        _secondaryPet = GetBooleanValue("SecondaryPet", 0);
        _extraMinions = GetIntegerValue("ExtraMinions", 0);
        _petAutoSpawnDelay = GetIntegerValue("PetAutoSpawnDelay", 0);
        _offenseRating = GetIntegerValue("OffenseRating", 0);
        _defenseRating = GetIntegerValue("DefenseRating", 0);
        _utilityRating = GetIntegerValue("UtilityRating", 0);
    }

    public bool GetLockedForChronos()
    {
        return _lockedForChronos;
    }

    public bool GetDisabled()
    {
        return _disabled;
    }

    public string GetItemName()
    {
        return _itemName;
    }

    public string GetWeaponSkill()
    {
        return _weaponSkill;
    }

    public string GetUltimateSkill()
    {
        return _ultimateSkill;
    }

    public string GetPet()
    {
        return _pet;
    }

    public int GetSpeed()
    {
        return _speed;
    }

    public int GetHitpoints()
    {
        return _hitpoints;
    }

    public bool GetMeleeAutoAttackSplashDamage()
    {
        return _meleeAutoAttackSplashDamage;
    }

    public int GetAutoAttackSpeedMs()
    {
        return _autoAttackSpeedMs;
    }

    public int GetAutoAttackDamage()
    {
        return _autoAttackDamage;
    }

    public int GetAutoAttackBulletsPerShot()
    {
        return _autoAttackBulletsPerShot;
    }

    public string GetAutoAttackMode()
    {
        return _autoAttackMode;
    }

    public int GetAutoAttackProjectileSpread()
    {
        return _autoAttackProjectileSpread;
    }

    public string GetAutoAttackProjectile()
    {
        return _autoAttackProjectile;
    }

    public int GetAutoAttackRange()
    {
        return _autoAttackRange;
    }

    public int GetRegeneratePerSecond()
    {
        return _regeneratePerSecond;
    }

    public int GetUltiChargeMul()
    {
        return _ultiChargeMul;
    }

    public int GetUltiChargeUltiMul()
    {
        return _ultiChargeUltiMul;
    }

    public new string GetType()
    {
        return _type;
    }

    public int GetDamagerPercentFromAliens()
    {
        return _damagerPercentFromAliens;
    }

    public string GetDefaultSkin()
    {
        return _defaultSkin;
    }

    public string GetFileName()
    {
        return _fileName;
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

    public string GetAreaEffect()
    {
        return _areaEffect;
    }

    public string GetDeathAreaEffect()
    {
        return _deathAreaEffect;
    }

    public string GetTakeDamageEffect()
    {
        return _takeDamageEffect;
    }

    public string GetDeathEffect()
    {
        return _deathEffect;
    }

    public string GetMoveEffect()
    {
        return _moveEffect;
    }

    public string GetReloadEffect()
    {
        return _reloadEffect;
    }

    public string GetOutOfAmmoEffect()
    {
        return _outOfAmmoEffect;
    }

    public string GetDryFireEffect()
    {
        return _dryFireEffect;
    }

    public string GetSpawnEffect()
    {
        return _spawnEffect;
    }

    public string GetMeleeHitEffect()
    {
        return _meleeHitEffect;
    }

    public string GetAutoAttackStartEffect()
    {
        return _autoAttackStartEffect;
    }

    public string GetBoneEffect1()
    {
        return _boneEffect1;
    }

    public string GetBoneEffect2()
    {
        return _boneEffect2;
    }

    public string GetBoneEffect3()
    {
        return _boneEffect3;
    }

    public string GetBoneEffect4()
    {
        return _boneEffect4;
    }

    public string GetBoneEffectUse()
    {
        return _boneEffectUse;
    }

    public string GetLoopedEffect()
    {
        return _loopedEffect;
    }

    public string GetLoopedEffect2()
    {
        return _loopedEffect2;
    }

    public string GetKillCelebrationSoundVo()
    {
        return _killCelebrationSoundVo;
    }

    public string GetInLeadCelebrationSoundVo()
    {
        return _inLeadCelebrationSoundVo;
    }

    public string GetStartSoundVo()
    {
        return _startSoundVo;
    }

    public string GetUseUltiSoundVo()
    {
        return _useUltiSoundVo;
    }

    public string GetTakeDamageSoundVo()
    {
        return _takeDamageSoundVo;
    }

    public string GetDeathSoundVo()
    {
        return _deathSoundVo;
    }

    public string GetAttackSoundVo()
    {
        return _attackSoundVo;
    }

    public int GetAttackStartEffectOffset()
    {
        return _attackStartEffectOffset;
    }

    public int GetTwoWeaponAttackEffectOffset()
    {
        return _twoWeaponAttackEffectOffset;
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

    public int GetScale()
    {
        return _scale;
    }

    public int GetHeroScreenScale()
    {
        return _heroScreenScale;
    }

    public int GetFitToBoxScale()
    {
        return _fitToBoxScale;
    }

    public int GetEndScreenScale()
    {
        return _endScreenScale;
    }

    public int GetGatchaScreenScale()
    {
        return _gatchaScreenScale;
    }

    public int GetHomeScreenScale()
    {
        return _homeScreenScale;
    }

    public int GetHeroScreenXOffset()
    {
        return _heroScreenXOffset;
    }

    public int GetHeroScreenZOffset()
    {
        return _heroScreenZOffset;
    }

    public int GetCollisionRadius()
    {
        return _collisionRadius;
    }

    public string GetHealthBar()
    {
        return _healthBar;
    }

    public int GetHealthBarOffsetY()
    {
        return _healthBarOffsetY;
    }

    public int GetFlyingHeight()
    {
        return _flyingHeight;
    }

    public int GetProjectileStartZ()
    {
        return _projectileStartZ;
    }

    public int GetStopMovementAfterMs()
    {
        return _stopMovementAfterMs;
    }

    public int GetWaitMs()
    {
        return _waitMs;
    }

    public bool GetForceAttackAnimationToEnd()
    {
        return _forceAttackAnimationToEnd;
    }

    public string GetIconSwf()
    {
        return _iconSwf;
    }

    public int GetRecoilAmount()
    {
        return _recoilAmount;
    }

    public string GetHomeworld()
    {
        return _homeworld;
    }

    public string GetFootstepClip()
    {
        return _footstepClip;
    }

    public int GetDifferentFootstepOffset()
    {
        return _differentFootstepOffset;
    }

    public int GetFootstepIntervalMs()
    {
        return _footstepIntervalMs;
    }

    public int GetAttackingWeaponScale()
    {
        return _attackingWeaponScale;
    }

    public bool GetUseThrowingLeftWeaponBoneScaling()
    {
        return _useThrowingLeftWeaponBoneScaling;
    }

    public bool GetUseThrowingRightWeaponBoneScaling()
    {
        return _useThrowingRightWeaponBoneScaling;
    }

    public int GetCommonSetUpgradeBonus()
    {
        return _commonSetUpgradeBonus;
    }

    public int GetRareSetUpgradeBonus()
    {
        return _rareSetUpgradeBonus;
    }

    public int GetSuperRareSetUpgradeBonus()
    {
        return _superRareSetUpgradeBonus;
    }

    public bool GetCanWalkOverWater()
    {
        return _canWalkOverWater;
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

    public int GetChargeUltiAutomatically()
    {
        return _chargeUltiAutomatically;
    }

    public string GetVideoLink()
    {
        return _videoLink;
    }

    public bool GetShouldEncodePetStatus()
    {
        return _shouldEncodePetStatus;
    }

    public bool GetSecondaryPet()
    {
        return _secondaryPet;
    }

    public int GetExtraMinions()
    {
        return _extraMinions;
    }

    public int GetPetAutoSpawnDelay()
    {
        return _petAutoSpawnDelay;
    }

    public int GetOffenseRating()
    {
        return _offenseRating;
    }

    public int GetDefenseRating()
    {
        return _defenseRating;
    }

    public int GetUtilityRating()
    {
        return _utilityRating;
    }

    public bool IsDecoy()
    {
        return GetType() == "Minion_Mirage";
    }

    public bool IsBoss()
    {
        return GetType() == "Npc_Boss" || GetType() == "Npc_Boss_TownCrush";
    }

    public bool IsTrain()
    {
        return GetType() == "Train";
    }

    public bool IsBase()
    {
        return GetType() == "Pvp_Base";
    }

    public bool IsLootBox()
    {
        return GetType() == "LootBox";
    }

    public bool IsTrainingDummy()
    {
        return GetType() == "Minion_Building_charges_ulti";
    }

    public bool IsTownCrushBoss()
    {
        return GetType() == "Npc_Boss_TownCrush";
    }

    public bool IsCarryable()
    {
        return GetType() == "Carryable";
    }

    public bool IsHero()
    {
        return GetType() == "Hero";
    }

    public bool IsPet()
    {
        return GetPet() != null!;
    }

    public bool IsDuplicate()
    {
        return GetType() == "Minion_Duplicate";
    }

    public bool IsPayload()
    {
        return GetType() == "Payload";
    }

    public bool HasAutoAttack()
    {
        return GetAutoAttackDamage() > 0;
    }

    public bool HasVeryMuchHitPoints()
    {
        return GetHitpoints() > 5799;
    }
}