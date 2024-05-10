using BSL.v41.Tools.LaserCsv.Data;
using BSL.v41.Tools.LaserCsv.Portal;

namespace BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

public class LogicAreaEffectData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private bool _allowEffectInterrupt;
    private string _blueExportName = null!;
    private string _bulletExplosionBullet = null!;
    private int _bulletExplosionBulletDistance;
    private string _bulletExplosionItem = null!;
    private bool _canStopGrapple;
    private int _customValue;
    private int _damage;
    private bool _destroysEnvironment;
    private bool _dontShowToEnemy;
    private string _effect = null!;
    private string _exportNameObject = null!;
    private string _exportNameTop = null!;
    private string _fileName = null!;
    private int _freezeStrength;
    private int _freezeTicks;
    private bool _isPersonal;
    private string _layer = null!;
    private string _loopingEffect = null!;
    private string _neutralExportName = null!;
    private string _ownExportName = null!;
    private string _parentAreaEffectForSkin = null!;
    private int _pushbackDeadzone;
    private int _pushbackStrength;
    private int _pushbackStrengthSelf;
    private int _radius;
    private string _redExportName = null!;
    private bool _requireLineOfSight;
    private int _sameAreaEffectCanNotDamageMs;
    private int _scale;
    private bool _serverControlsFrame;
    private bool _shouldShowEvenIfOutsideScreen;
    private int _timeMs;
    private string _type = null!;

    // LogicAreaEffectData.

    public override void CreateReferences()
    {
        _parentAreaEffectForSkin = GetValue("ParentAreaEffectForSkin", 0);
        _fileName = GetValue("FileName", 0);
        _ownExportName = GetValue("OwnExportName", 0);
        _blueExportName = GetValue("BlueExportName", 0);
        _redExportName = GetValue("RedExportName", 0);
        _neutralExportName = GetValue("NeutralExportName", 0);
        _layer = GetValue("Layer", 0);
        _exportNameTop = GetValue("ExportNameTop", 0);
        _exportNameObject = GetValue("ExportNameObject", 0);
        _effect = GetValue("Effect", 0);
        _loopingEffect = GetValue("LoopingEffect", 0);
        _allowEffectInterrupt = GetBooleanValue("AllowEffectInterrupt", 0);
        _serverControlsFrame = GetBooleanValue("ServerControlsFrame", 0);
        _scale = GetIntegerValue("Scale", 0);
        _timeMs = GetIntegerValue("TimeMs", 0);
        _radius = GetIntegerValue("Radius", 0);
        _damage = GetIntegerValue("Damage", 0);
        _customValue = GetIntegerValue("CustomValue", 0);
        _type = GetValue("Type", 0);
        _isPersonal = GetBooleanValue("IsPersonal", 0);
        _bulletExplosionBullet = GetValue("BulletExplosionBullet", 0);
        _bulletExplosionBulletDistance = GetIntegerValue("BulletExplosionBulletDistance", 0);
        _bulletExplosionItem = GetValue("BulletExplosionItem", 0);
        _destroysEnvironment = GetBooleanValue("DestroysEnvironment", 0);
        _pushbackStrength = GetIntegerValue("PushbackStrength", 0);
        _pushbackStrengthSelf = GetIntegerValue("PushbackStrengthSelf", 0);
        _pushbackDeadzone = GetIntegerValue("PushbackDeadzone", 0);
        _canStopGrapple = GetBooleanValue("CanStopGrapple", 0);
        _freezeStrength = GetIntegerValue("FreezeStrength", 0);
        _freezeTicks = GetIntegerValue("FreezeTicks", 0);
        _shouldShowEvenIfOutsideScreen = GetBooleanValue("ShouldShowEvenIfOutsideScreen", 0);
        _sameAreaEffectCanNotDamageMs = GetIntegerValue("SameAreaEffectCanNotDamageMs", 0);
        _dontShowToEnemy = GetBooleanValue("DontShowToEnemy", 0);
        _requireLineOfSight = GetBooleanValue("RequireLineOfSight", 0);
    }

    public string GetParentAreaEffectForSkin()
    {
        return _parentAreaEffectForSkin;
    }

    public string GetFileName()
    {
        return _fileName;
    }

    public string GetOwnExportName()
    {
        return _ownExportName;
    }

    public string GetBlueExportName()
    {
        return _blueExportName;
    }

    public string GetRedExportName()
    {
        return _redExportName;
    }

    public string GetNeutralExportName()
    {
        return _neutralExportName;
    }

    public string GetLayer()
    {
        return _layer;
    }

    public string GetExportNameTop()
    {
        return _exportNameTop;
    }

    public string GetExportNameObject()
    {
        return _exportNameObject;
    }

    public string GetEffect()
    {
        return _effect;
    }

    public string GetLoopingEffect()
    {
        return _loopingEffect;
    }

    public bool GetAllowEffectInterrupt()
    {
        return _allowEffectInterrupt;
    }

    public bool GetServerControlsFrame()
    {
        return _serverControlsFrame;
    }

    public int GetScale()
    {
        return _scale;
    }

    public int GetTimeMs()
    {
        return _timeMs;
    }

    public int GetRadius()
    {
        return _radius;
    }

    public int GetDamage()
    {
        return _damage;
    }

    public int GetCustomValue()
    {
        return _customValue;
    }

    public new string GetType()
    {
        return _type;
    }

    public bool GetIsPersonal()
    {
        return _isPersonal;
    }

    public string GetBulletExplosionBullet()
    {
        return _bulletExplosionBullet;
    }

    public int GetBulletExplosionBulletDistance()
    {
        return _bulletExplosionBulletDistance;
    }

    public string GetBulletExplosionItem()
    {
        return _bulletExplosionItem;
    }

    public bool GetDestroysEnvironment()
    {
        return _destroysEnvironment;
    }

    public int GetPushbackStrength()
    {
        return _pushbackStrength;
    }

    public int GetPushbackStrengthSelf()
    {
        return _pushbackStrengthSelf;
    }

    public int GetPushbackDeadzone()
    {
        return _pushbackDeadzone;
    }

    public bool GetCanStopGrapple()
    {
        return _canStopGrapple;
    }

    public int GetFreezeStrength()
    {
        return _freezeStrength;
    }

    public int GetFreezeTicks()
    {
        return _freezeTicks;
    }

    public bool GetShouldShowEvenIfOutsideScreen()
    {
        return _shouldShowEvenIfOutsideScreen;
    }

    public int GetSameAreaEffectCanNotDamageMs()
    {
        return _sameAreaEffectCanNotDamageMs;
    }

    public bool GetDontShowToEnemy()
    {
        return _dontShowToEnemy;
    }

    public bool GetRequireLineOfSight()
    {
        return _requireLineOfSight;
    }
}