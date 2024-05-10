using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.Mathematical;
using BSL.v41.Titan.Mathematical.Data;
using BSL.v41.Tools.LaserCsv;
using BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

namespace BSL.v41.General.NetIsland.LaserBattle.Significant;

public class LogicSkillServer
{
    private readonly LogicBattleModeServer _logicBattleModeServer;
    private readonly LogicSkillData _logicSkillData;
    private readonly int _maxCharge;
    private int _activeTime;
    private int _charge;
    private int _cooldownValue;
    private LogicVector2 _logicVector2 = new();
    private int _tick;

    public LogicSkillServer(LogicBattleModeServer logicBattleModeServer, int classId, int instanceId)
    {
        _logicBattleModeServer = logicBattleModeServer;
        _logicSkillData = ((LogicSkillData?)LogicDataTables.GetDataById(GlobalId.CreateGlobalId(classId, instanceId)))!;
        _tick = 0;
        _maxCharge = LogicMath.Max(1000, 1000 * _logicSkillData.GetMaxCharge());
        _charge = _maxCharge;
    }

    public void Tick()
    {
        if (_charge < _maxCharge && !(_tick < _activeTime) && _logicSkillData.GetMaxCharge() != 0)
        {
            _charge += 1000 / (_logicSkillData.GetRechargeTime() / 50);
            _charge = LogicMath.Min(_maxCharge, _charge);
        }

        if (_tick < _activeTime) _tick++;
    }

    public bool BlockedForCastType(bool v2)
    {
        if (v2) return _tick < _activeTime;
        if (_tick == 0) return _logicSkillData.GetExecuteFirstAttackImmediately();
        return _tick % (_logicSkillData.GetMsBetweenAttacks() / 50) == 0;
    }

    public bool Activate(bool wasReturn, LogicVector2 logicVector2)
    {
        _logicVector2 = logicVector2;
        
        if (_charge <= 1000 && _logicSkillData.GetMaxCharge() != 0) return false;
        if (_cooldownValue >= _logicBattleModeServer.GetTicksGone()) return false;
        if (wasReturn) return true;
        
        if (_logicSkillData.GetBehaviorType() != "Charge") _activeTime = _logicSkillData.GetActiveTime() / 50 - 2;
        else _activeTime = _logicSkillData.GetCastingRange() / 2 - 2;
        
        _tick = -1;
        _charge -= 1000;
        _cooldownValue = _logicBattleModeServer.GetTicksGone() + _logicSkillData.GetMsBetweenAttacks() / 10 -
                         10 / 2;

        return true;
    }

    public void Encode(BitStream bitStream, bool interruptedSkill)
    {
        var v4 = (int)((ulong)(1374389535L * _activeTime) >> 32);

        bitStream.WritePositiveVIntMax255OftenZero((int)((v4 >> 4) + ((uint)v4 >> 31)));
        bitStream.WriteBoolean(BlockedForCastType(true));
        bitStream.WritePositiveVIntMax255OftenZero(LogicMath.Clamp(_cooldownValue - _logicBattleModeServer.GetTicksGone(), 0, 100));

        if (_logicSkillData.GetMaxCharge() >= 1) bitStream.WritePositiveIntMax4095(_charge);
        if (_logicSkillData.GetSkillCanChange()) bitStream.WritePositiveIntMax255(_logicSkillData.GetInstanceId());
    }

    public LogicVector2 GetSkillCastPos()
    {
        return _logicVector2;
    }

    public LogicSkillData GetData()
    {
        return _logicSkillData;
    }

    public int GetMaxCharge()
    {
        return _maxCharge;
    }
}