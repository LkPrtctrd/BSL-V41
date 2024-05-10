using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicSkinConfData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private string _areaEffect = null!;
    private string _areaEffectStarPower = null!;
    private bool _attackLocksAttackAngle;
    private string _attackSoundVo = null!;
    private string _autoAttackProjectile = null!;
    private string _boneEffect1 = null!;
    private string _boneEffect2 = null!;
    private string _boneEffect3 = null!;
    private string _boneEffect4 = null!;
    private string _boneEffectUse = null!;
    private string _bossAutoAttackAnim = null!;
    private string _bossAutoAttackRecoilAnim = null!;
    private string _bossAutoAttackRecoilAnim2 = null!;
    private string _character = null!;
    private string _chargedShotEffect = null!;
    private string _deathSoundVo = null!;
    private bool _disableHeadRotation;
    private string _gadgetActiveAnim = null!;
    private string _gadgetModel = null!;
    private string _gadgetRecoilAnim = null!;
    private string _happyAnim = null!;
    private string _happyEffect = null!;
    private string _happyEnterAnim = null!;
    private string _happyFace = null!;
    private string _happyLoopAnim = null!;
    private string _happyLoopFace = null!;
    private string _heroScreenAnim = null!;
    private string _heroScreenFace = null!;
    private string _heroScreenIdleAnim = null!;
    private string _heroScreenIdleFace = null!;
    private string _heroScreenLoopAnim = null!;
    private string _heroScreenLoopFace = null!;
    private string _idleAnim = null!;
    private string _idleFace = null!;
    private string _incendiaryStarPowerAreaEffect = null!;
    private string _inLeadCelebrationSoundVo = null!;
    private string _introAnim = null!;
    private string _introCameraFile = null!;
    private string _introFace = null!;
    private string _killCelebrationSoundVo = null!;
    private string _mainAttackEffect = null!;
    private string _mainAttackProjectile = null!;
    private string _mainAttackUseEffect = null!;
    private string _meleeHitEffect = null!;
    private bool _mirrorIntro;
    private string _model = null!;
    private string _moveEffect = null!;
    private bool _petInSameSprite;
    private string _portraitCameraFile = null!;
    private string _primarySkillAnim = null!;
    private string _primarySkillRecoilAnim = null!;
    private string _primarySkillRecoilAnim2 = null!;
    private string _profileAnim = null!;
    private string _profileFace = null!;
    private string _projectileForShockyStarPower = null!;
    private string _pushbackAnim = null!;
    private string _reloadingAnim = null!;
    private string _sadAnim = null!;
    private string _sadEffect = null!;
    private string _sadEnterAnim = null!;
    private string _sadFace = null!;
    private string _sadLoopAnim = null!;
    private string _sadLoopFace = null!;
    private string _secondarySkillAnim = null!;
    private string _secondarySkillRecoilAnim = null!;
    private string _secondarySkillRecoilAnim2 = null!;
    private string _signatureAnim = null!;
    private string _signatureFace = null!;
    private string _spawnedItem = null!;
    private string _spawnEffect = null!;
    private string _startSoundVo = null!;
    private string _stillEffect = null!;
    private string _takeDamageSoundVo = null!;
    private string _ultiAttackEffect = null!;
    private string _ultiEndEffect = null!;
    private bool _ultiLocksAttackAngle;
    private string _ultiLoopEffect = null!;
    private string _ultiLoopEffect2 = null!;
    private string _ultiProjectile = null!;
    private string _ultiUseEffect = null!;
    private bool _useBlueTextureInMenus;
    private string _useUltiSoundVo = null!;
    private string _walkAnim = null!;
    private string _walkFace = null!;

    // LogicSkinConfData.

    public override void CreateReferences()
    {
        _character = GetValue("Character", 0);
        _model = GetValue("Model", 0);
        _gadgetModel = GetValue("GadgetModel", 0);
        _portraitCameraFile = GetValue("PortraitCameraFile", 0);
        _introCameraFile = GetValue("IntroCameraFile", 0);
        _mirrorIntro = GetBooleanValue("MirrorIntro", 0);
        _idleAnim = GetValue("IdleAnim", 0);
        _walkAnim = GetValue("WalkAnim", 0);
        _primarySkillAnim = GetValue("PrimarySkillAnim", 0);
        _secondarySkillAnim = GetValue("SecondarySkillAnim", 0);
        _primarySkillRecoilAnim = GetValue("PrimarySkillRecoilAnim", 0);
        _primarySkillRecoilAnim2 = GetValue("PrimarySkillRecoilAnim2", 0);
        _secondarySkillRecoilAnim = GetValue("SecondarySkillRecoilAnim", 0);
        _secondarySkillRecoilAnim2 = GetValue("SecondarySkillRecoilAnim2", 0);
        _reloadingAnim = GetValue("ReloadingAnim", 0);
        _pushbackAnim = GetValue("PushbackAnim", 0);
        _happyAnim = GetValue("HappyAnim", 0);
        _happyLoopAnim = GetValue("HappyLoopAnim", 0);
        _sadAnim = GetValue("SadAnim", 0);
        _sadLoopAnim = GetValue("SadLoopAnim", 0);
        _heroScreenIdleAnim = GetValue("HeroScreenIdleAnim", 0);
        _heroScreenAnim = GetValue("HeroScreenAnim", 0);
        _heroScreenLoopAnim = GetValue("HeroScreenLoopAnim", 0);
        _signatureAnim = GetValue("SignatureAnim", 0);
        _happyEnterAnim = GetValue("HappyEnterAnim", 0);
        _sadEnterAnim = GetValue("SadEnterAnim", 0);
        _profileAnim = GetValue("ProfileAnim", 0);
        _introAnim = GetValue("IntroAnim", 0);
        _bossAutoAttackAnim = GetValue("BossAutoAttackAnim", 0);
        _bossAutoAttackRecoilAnim = GetValue("BossAutoAttackRecoilAnim", 0);
        _bossAutoAttackRecoilAnim2 = GetValue("BossAutoAttackRecoilAnim2", 0);
        _gadgetActiveAnim = GetValue("GadgetActiveAnim", 0);
        _gadgetRecoilAnim = GetValue("GadgetRecoilAnim", 0);
        _idleFace = GetValue("IdleFace", 0);
        _walkFace = GetValue("WalkFace", 0);
        _happyFace = GetValue("HappyFace", 0);
        _happyLoopFace = GetValue("HappyLoopFace", 0);
        _sadFace = GetValue("SadFace", 0);
        _sadLoopFace = GetValue("SadLoopFace", 0);
        _heroScreenIdleFace = GetValue("HeroScreenIdleFace", 0);
        _heroScreenFace = GetValue("HeroScreenFace", 0);
        _heroScreenLoopFace = GetValue("HeroScreenLoopFace", 0);
        _signatureFace = GetValue("SignatureFace", 0);
        _profileFace = GetValue("ProfileFace", 0);
        _introFace = GetValue("IntroFace", 0);
        _happyEffect = GetValue("HappyEffect", 0);
        _sadEffect = GetValue("SadEffect", 0);
        _petInSameSprite = GetBooleanValue("PetInSameSprite", 0);
        _attackLocksAttackAngle = GetBooleanValue("AttackLocksAttackAngle", 0);
        _ultiLocksAttackAngle = GetBooleanValue("UltiLocksAttackAngle", 0);
        _mainAttackProjectile = GetValue("MainAttackProjectile", 0);
        _ultiProjectile = GetValue("UltiProjectile", 0);
        _mainAttackEffect = GetValue("MainAttackEffect", 0);
        _ultiAttackEffect = GetValue("UltiAttackEffect", 0);
        _useBlueTextureInMenus = GetBooleanValue("UseBlueTextureInMenus", 0);
        _mainAttackUseEffect = GetValue("MainAttackUseEffect", 0);
        _ultiUseEffect = GetValue("UltiUseEffect", 0);
        _ultiEndEffect = GetValue("UltiEndEffect", 0);
        _meleeHitEffect = GetValue("MeleeHitEffect", 0);
        _spawnEffect = GetValue("SpawnEffect", 0);
        _ultiLoopEffect = GetValue("UltiLoopEffect", 0);
        _ultiLoopEffect2 = GetValue("UltiLoopEffect2", 0);
        _areaEffect = GetValue("AreaEffect", 0);
        _areaEffectStarPower = GetValue("AreaEffectStarPower", 0);
        _spawnedItem = GetValue("SpawnedItem", 0);
        _killCelebrationSoundVo = GetValue("KillCelebrationSoundVO", 0);
        _inLeadCelebrationSoundVo = GetValue("InLeadCelebrationSoundVO", 0);
        _startSoundVo = GetValue("StartSoundVO", 0);
        _useUltiSoundVo = GetValue("UseUltiSoundVO", 0);
        _takeDamageSoundVo = GetValue("TakeDamageSoundVO", 0);
        _deathSoundVo = GetValue("DeathSoundVO", 0);
        _attackSoundVo = GetValue("AttackSoundVO", 0);
        _boneEffect1 = GetValue("BoneEffect1", 0);
        _boneEffect2 = GetValue("BoneEffect2", 0);
        _boneEffect3 = GetValue("BoneEffect3", 0);
        _boneEffect4 = GetValue("BoneEffect4", 0);
        _boneEffectUse = GetValue("BoneEffectUse", 0);
        _autoAttackProjectile = GetValue("AutoAttackProjectile", 0);
        _projectileForShockyStarPower = GetValue("ProjectileForShockyStarPower", 0);
        _incendiaryStarPowerAreaEffect = GetValue("IncendiaryStarPowerAreaEffect", 0);
        _moveEffect = GetValue("MoveEffect", 0);
        _stillEffect = GetValue("StillEffect", 0);
        _chargedShotEffect = GetValue("ChargedShotEffect", 0);
        _disableHeadRotation = GetBooleanValue("DisableHeadRotation", 0);
    }

    public string GetCharacter()
    {
        return _character;
    }

    public string GetModel()
    {
        return _model;
    }

    public string GetGadgetModel()
    {
        return _gadgetModel;
    }

    public string GetPortraitCameraFile()
    {
        return _portraitCameraFile;
    }

    public string GetIntroCameraFile()
    {
        return _introCameraFile;
    }

    public bool GetMirrorIntro()
    {
        return _mirrorIntro;
    }

    public string GetIdleAnim()
    {
        return _idleAnim;
    }

    public string GetWalkAnim()
    {
        return _walkAnim;
    }

    public string GetPrimarySkillAnim()
    {
        return _primarySkillAnim;
    }

    public string GetSecondarySkillAnim()
    {
        return _secondarySkillAnim;
    }

    public string GetPrimarySkillRecoilAnim()
    {
        return _primarySkillRecoilAnim;
    }

    public string GetPrimarySkillRecoilAnim2()
    {
        return _primarySkillRecoilAnim2;
    }

    public string GetSecondarySkillRecoilAnim()
    {
        return _secondarySkillRecoilAnim;
    }

    public string GetSecondarySkillRecoilAnim2()
    {
        return _secondarySkillRecoilAnim2;
    }

    public string GetReloadingAnim()
    {
        return _reloadingAnim;
    }

    public string GetPushbackAnim()
    {
        return _pushbackAnim;
    }

    public string GetHappyAnim()
    {
        return _happyAnim;
    }

    public string GetHappyLoopAnim()
    {
        return _happyLoopAnim;
    }

    public string GetSadAnim()
    {
        return _sadAnim;
    }

    public string GetSadLoopAnim()
    {
        return _sadLoopAnim;
    }

    public string GetHeroScreenIdleAnim()
    {
        return _heroScreenIdleAnim;
    }

    public string GetHeroScreenAnim()
    {
        return _heroScreenAnim;
    }

    public string GetHeroScreenLoopAnim()
    {
        return _heroScreenLoopAnim;
    }

    public string GetSignatureAnim()
    {
        return _signatureAnim;
    }

    public string GetHappyEnterAnim()
    {
        return _happyEnterAnim;
    }

    public string GetSadEnterAnim()
    {
        return _sadEnterAnim;
    }

    public string GetProfileAnim()
    {
        return _profileAnim;
    }

    public string GetIntroAnim()
    {
        return _introAnim;
    }

    public string GetBossAutoAttackAnim()
    {
        return _bossAutoAttackAnim;
    }

    public string GetBossAutoAttackRecoilAnim()
    {
        return _bossAutoAttackRecoilAnim;
    }

    public string GetBossAutoAttackRecoilAnim2()
    {
        return _bossAutoAttackRecoilAnim2;
    }

    public string GetGadgetActiveAnim()
    {
        return _gadgetActiveAnim;
    }

    public string GetGadgetRecoilAnim()
    {
        return _gadgetRecoilAnim;
    }

    public string GetIdleFace()
    {
        return _idleFace;
    }

    public string GetWalkFace()
    {
        return _walkFace;
    }

    public string GetHappyFace()
    {
        return _happyFace;
    }

    public string GetHappyLoopFace()
    {
        return _happyLoopFace;
    }

    public string GetSadFace()
    {
        return _sadFace;
    }

    public string GetSadLoopFace()
    {
        return _sadLoopFace;
    }

    public string GetHeroScreenIdleFace()
    {
        return _heroScreenIdleFace;
    }

    public string GetHeroScreenFace()
    {
        return _heroScreenFace;
    }

    public string GetHeroScreenLoopFace()
    {
        return _heroScreenLoopFace;
    }

    public string GetSignatureFace()
    {
        return _signatureFace;
    }

    public string GetProfileFace()
    {
        return _profileFace;
    }

    public string GetIntroFace()
    {
        return _introFace;
    }

    public string GetHappyEffect()
    {
        return _happyEffect;
    }

    public string GetSadEffect()
    {
        return _sadEffect;
    }

    public bool GetPetInSameSprite()
    {
        return _petInSameSprite;
    }

    public bool GetAttackLocksAttackAngle()
    {
        return _attackLocksAttackAngle;
    }

    public bool GetUltiLocksAttackAngle()
    {
        return _ultiLocksAttackAngle;
    }

    public string GetMainAttackProjectile()
    {
        return _mainAttackProjectile;
    }

    public string GetUltiProjectile()
    {
        return _ultiProjectile;
    }

    public string GetMainAttackEffect()
    {
        return _mainAttackEffect;
    }

    public string GetUltiAttackEffect()
    {
        return _ultiAttackEffect;
    }

    public bool GetUseBlueTextureInMenus()
    {
        return _useBlueTextureInMenus;
    }

    public string GetMainAttackUseEffect()
    {
        return _mainAttackUseEffect;
    }

    public string GetUltiUseEffect()
    {
        return _ultiUseEffect;
    }

    public string GetUltiEndEffect()
    {
        return _ultiEndEffect;
    }

    public string GetMeleeHitEffect()
    {
        return _meleeHitEffect;
    }

    public string GetSpawnEffect()
    {
        return _spawnEffect;
    }

    public string GetUltiLoopEffect()
    {
        return _ultiLoopEffect;
    }

    public string GetUltiLoopEffect2()
    {
        return _ultiLoopEffect2;
    }

    public string GetAreaEffect()
    {
        return _areaEffect;
    }

    public string GetAreaEffectStarPower()
    {
        return _areaEffectStarPower;
    }

    public string GetSpawnedItem()
    {
        return _spawnedItem;
    }

    public string GetKillCelebrationSoundVo()
    {
        return _killCelebrationSoundVo;
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

    public string GetAutoAttackProjectile()
    {
        return _autoAttackProjectile;
    }

    public string GetProjectileForShockyStarPower()
    {
        return _projectileForShockyStarPower;
    }

    public string GetIncendiaryStarPowerAreaEffect()
    {
        return _incendiaryStarPowerAreaEffect;
    }

    public string GetMoveEffect()
    {
        return _moveEffect;
    }

    public string GetStillEffect()
    {
        return _stillEffect;
    }

    public string GetChargedShotEffect()
    {
        return _chargedShotEffect;
    }

    public bool GetDisableHeadRotation()
    {
        return _disableHeadRotation;
    }
}