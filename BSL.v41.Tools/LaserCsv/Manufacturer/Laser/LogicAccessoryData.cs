using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicAccessoryData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private int _activationDelay;
    private int _activeTicks;
    private int _aimGuideType;
    private bool _allowStunActivation;
    private int _animationIndex;
    private string _areaEffect = null!;
    private int _chargeCount;
    private bool _consumesAmmo;
    private int _cooldown;
    private string _customObject = null!;
    private int _customValue1;
    private int _customValue2;
    private int _customValue3;
    private int _customValue4;
    private int _customValue5;
    private int _customValue6;
    private bool _destroyPet;
    private bool _interruptable;
    private bool _interruptsAction;
    private string _loopingEffect = null!;
    private string _loopingEffectPet = null!;
    private string _missingTargetText = null!;
    private string _petAreaEffect = null!;
    private int _range;
    private bool _requireEnemyInRange;
    private int _requirePetDistance;
    private string _requiresSpecificActionText = null!;
    private bool _setAttackAngle;
    private int _shieldPercent;
    private int _shieldTicks;
    private bool _showCountdown;
    private bool _skipTypeCondition;
    private int _speedBoost;
    private int _speedBoostTicks;
    private bool _stopMovement;
    private bool _stopPetForDelay;
    private int _subType;
    private string _targetAlreadyActiveText = null!;
    private bool _targetFriends;
    private bool _targetIndirect;
    private string _targetTooFarText = null!;
    private string _type = null!;
    private int _usableDuringCharge;
    private string _useEffect = null!;

    // LogicAccessoryData.

    public override void CreateReferences()
    {
        _type = GetValue("Type", 0);
        _subType = GetIntegerValue("SubType", 0);
        _chargeCount = GetIntegerValue("ChargeCount", 0);
        _cooldown = GetIntegerValue("Cooldown", 0);
        _useEffect = GetValue("UseEffect", 0);
        _loopingEffect = GetValue("LoopingEffect", 0);
        _loopingEffectPet = GetValue("LoopingEffectPet", 0);
        _activationDelay = GetIntegerValue("ActivationDelay", 0);
        _activeTicks = GetIntegerValue("ActiveTicks", 0);
        _showCountdown = GetBooleanValue("ShowCountdown", 0);
        _stopMovement = GetBooleanValue("StopMovement", 0);
        _stopPetForDelay = GetBooleanValue("StopPetForDelay", 0);
        _animationIndex = GetIntegerValue("AnimationIndex", 0);
        _setAttackAngle = GetBooleanValue("SetAttackAngle", 0);
        _aimGuideType = GetIntegerValue("AimGuideType", 0);
        _consumesAmmo = GetBooleanValue("ConsumesAmmo", 0);
        _areaEffect = GetValue("AreaEffect", 0);
        _petAreaEffect = GetValue("PetAreaEffect", 0);
        _interruptsAction = GetBooleanValue("InterruptsAction", 0);
        _allowStunActivation = GetBooleanValue("AllowStunActivation", 0);
        _interruptable = GetBooleanValue("Interruptable", 0);
        _requirePetDistance = GetIntegerValue("RequirePetDistance", 0);
        _destroyPet = GetBooleanValue("DestroyPet", 0);
        _range = GetIntegerValue("Range", 0);
        _requireEnemyInRange = GetBooleanValue("RequireEnemyInRange", 0);
        _targetFriends = GetBooleanValue("TargetFriends", 0);
        _targetIndirect = GetBooleanValue("TargetIndirect", 0);
        _shieldPercent = GetIntegerValue("ShieldPercent", 0);
        _shieldTicks = GetIntegerValue("ShieldTicks", 0);
        _speedBoost = GetIntegerValue("SpeedBoost", 0);
        _speedBoostTicks = GetIntegerValue("SpeedBoostTicks", 0);
        _skipTypeCondition = GetBooleanValue("SkipTypeCondition", 0);
        _usableDuringCharge = GetIntegerValue("UsableDuringCharge", 0);
        _customObject = GetValue("CustomObject", 0);
        _customValue1 = GetIntegerValue("CustomValue1", 0);
        _customValue2 = GetIntegerValue("CustomValue2", 0);
        _customValue3 = GetIntegerValue("CustomValue3", 0);
        _customValue4 = GetIntegerValue("CustomValue4", 0);
        _customValue5 = GetIntegerValue("CustomValue5", 0);
        _customValue6 = GetIntegerValue("CustomValue6", 0);
        _missingTargetText = GetValue("MissingTargetText", 0);
        _targetTooFarText = GetValue("TargetTooFarText", 0);
        _targetAlreadyActiveText = GetValue("TargetAlreadyActiveText", 0);
        _requiresSpecificActionText = GetValue("RequiresSpecificActionText", 0);
    }

    public int GetActivationDelay()
    {
        return _activationDelay;
    }

    public int GetActiveTicks()
    {
        return _activeTicks;
    }

    public int GetAimGuideType()
    {
        return _aimGuideType;
    }

    public bool GetAllowStunActivation()
    {
        return _allowStunActivation;
    }

    public int GetAnimationIndex()
    {
        return _animationIndex;
    }

    public string GetAreaEffect()
    {
        return _areaEffect;
    }

    public int GetChargeCount()
    {
        return _chargeCount;
    }

    public bool GetConsumesAmmo()
    {
        return _consumesAmmo;
    }

    public int GetCooldown()
    {
        return _cooldown;
    }

    public string GetCustomObject()
    {
        return _customObject;
    }

    public int GetCustomValue1()
    {
        return _customValue1;
    }

    public int GetCustomValue2()
    {
        return _customValue2;
    }

    public int GetCustomValue3()
    {
        return _customValue3;
    }

    public int GetCustomValue4()
    {
        return _customValue4;
    }

    public int GetCustomValue5()
    {
        return _customValue5;
    }

    public int GetCustomValue6()
    {
        return _customValue6;
    }

    public bool GetDestroyPet()
    {
        return _destroyPet;
    }

    public bool GetInterruptable()
    {
        return _interruptable;
    }

    public bool GetInterruptsAction()
    {
        return _interruptsAction;
    }

    public string GetLoopingEffect()
    {
        return _loopingEffect;
    }

    public string GetLoopingEffectPet()
    {
        return _loopingEffectPet;
    }

    public string GetMissingTargetText()
    {
        return _missingTargetText;
    }

    public string GetPetAreaEffect()
    {
        return _petAreaEffect;
    }

    public int GetRange()
    {
        return _range;
    }

    public bool GetRequireEnemyInRange()
    {
        return _requireEnemyInRange;
    }

    public int GetRequirePetDistance()
    {
        return _requirePetDistance;
    }

    public string GetRequiresSpecificActionText()
    {
        return _requiresSpecificActionText;
    }

    public bool GetSetAttackAngle()
    {
        return _setAttackAngle;
    }

    public int GetShieldPercent()
    {
        return _shieldPercent;
    }

    public int GetShieldTicks()
    {
        return _shieldTicks;
    }

    public bool GetShowCountdown()
    {
        return _showCountdown;
    }

    public bool GetSkipTypeCondition()
    {
        return _skipTypeCondition;
    }

    public int GetSpeedBoost()
    {
        return _speedBoost;
    }

    public int GetSpeedBoostTicks()
    {
        return _speedBoostTicks;
    }

    public bool GetStopMovement()
    {
        return _stopMovement;
    }

    public bool GetStopPetForDelay()
    {
        return _stopPetForDelay;
    }

    public int GetSubType()
    {
        return _subType;
    }

    public string GetTargetAlreadyActiveText()
    {
        return _targetAlreadyActiveText;
    }

    public bool GetTargetFriends()
    {
        return _targetFriends;
    }

    public bool GetTargetIndirect()
    {
        return _targetIndirect;
    }

    public string GetTargetTooFarText()
    {
        return _targetTooFarText;
    }

    public string GetAType()
    {
        return _type;
    }

    public int GetUsableDuringCharge()
    {
        return _usableDuringCharge;
    }

    public string GetUseEffect()
    {
        return _useEffect;
    }
}