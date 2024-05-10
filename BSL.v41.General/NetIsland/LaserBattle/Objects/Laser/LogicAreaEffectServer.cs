using BSL.v41.Supercell.Titan.CommonUtils.Utils;
using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.Mathematical.Data;
using BSL.v41.Tools.LaserCsv;
using BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

namespace BSL.v41.General.NetIsland.LaserBattle.Objects.Laser;

public class LogicAreaEffectServer : LogicGameObjectServer
{
    private readonly List<LogicCharacterServer> _alreadyDamagedList;
    private readonly LogicAreaEffectData _logicAreaEffectData;
    private readonly LogicBattleModeServer _logicBattleModeServer;
    private readonly LogicAreaEffectData _logicParentAreaEffectData = null!;
    private int _lifeTime;
    private LogicCharacterServer _logicCharacterServerSource = null!;
    private int _preyDamage;
    private bool _staticSourcePosition;
    private int _tick;

    public LogicAreaEffectServer(LogicBattleModeServer logicBattleModeServer, int classId, int instanceId, int index) :
        base(logicBattleModeServer, classId, instanceId, 0, index)
    {
        _logicBattleModeServer = logicBattleModeServer;
        _logicAreaEffectData =
            ((LogicAreaEffectData?)LogicDataTables.GetDataById(GlobalId.CreateGlobalId(classId, instanceId)))!;
        _alreadyDamagedList = new List<LogicCharacterServer>();
        _staticSourcePosition = false;

        if (_logicAreaEffectData.GetParentAreaEffectForSkin() != "")
            _logicParentAreaEffectData =
                LogicDataTables.GetAreaEffectByName(_logicAreaEffectData.GetParentAreaEffectForSkin());
    }

    public override void Tick()
    {
        _tick++;
        {
            SetLifeTimeConsumedPercent((int)(_tick * 2.5));
        }

        if (_staticSourcePosition) SetPosition(_logicCharacterServerSource.GetPosition());
    }

    public void SetPreyOnTheWeak(int prayDamage)
    {
        _preyDamage = prayDamage;
    }

    public void SetSource(LogicCharacterServer logicCharacterServer, bool staticSourcePosition = false)
    {
        _logicCharacterServerSource = logicCharacterServer;
        _staticSourcePosition = staticSourcePosition;
    }

    public void SetLifeTimeConsumedPercent(int v1)
    {
        _lifeTime = v1;
    }

    public override int GetRadius()
    {
        return _logicAreaEffectData.GetRadius();
    }

    public LogicAreaEffectData GetAreaEffectData(bool isGetterParent)
    {
        return isGetterParent ? _logicParentAreaEffectData : _logicAreaEffectData;
    }

    public override bool ShouldDestruct()
    {
        return _tick >= _logicAreaEffectData.GetTimeMs() / 50;
    }

    public override void Encode(BitStream bitStream, bool isOwnObject, int visionIndex, int visionTeam)
    {
        base.Encode(bitStream, isOwnObject, visionIndex, visionTeam);

        bitStream.WritePositiveIntMax127(_lifeTime);
        if (bitStream.WriteBoolean(_staticSourcePosition))
            bitStream.WritePositiveIntMax134217727((int)_logicCharacterServerSource.GetPlayer()?.OwnObjectId!);
        bitStream.WritePositiveVIntMax65535OftenZero(0);
    }

    public override int GetType()
    {
        return ObjectTypeHelperTable.AreaEffect.GetObjectType();
    }
}