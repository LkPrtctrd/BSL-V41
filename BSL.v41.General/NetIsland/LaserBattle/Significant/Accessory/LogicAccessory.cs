using BSL.v41.General.NetIsland.LaserBattle.Objects;
using BSL.v41.Titan.DataStream;
using BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

namespace BSL.v41.General.NetIsland.LaserBattle.Significant.Accessory;

public class LogicAccessory(LogicGameObjectServer logicCharacterServer, LogicAccessoryData logicAccessoryData)
{
    private const bool V2AvailableCheck = true;

    private readonly bool _exclusiveAccessory = logicAccessoryData.GetAType().Equals("next_attack_change");

    private int _cooldownTick;
    private int _deltaTick;
    private bool _exclusiveAccessoryDoubleTrigger = logicAccessoryData.GetAType().Equals("next_attack_change");
    private bool _firstUse;

    private int _nowCount = logicAccessoryData.GetChargeCount();
    private bool _nowUse;
    private bool _pastUse;

    public void UpdateAccessory()
    {
        if (!_exclusiveAccessory)
        {
            _nowUse = _cooldownTick != 0;
        }
        else
        {
            if (_exclusiveAccessoryDoubleTrigger && _firstUse && !_pastUse)
            {
                _cooldownTick = logicAccessoryData.GetCooldown();
                _deltaTick = _cooldownTick - 1;
                _pastUse = true;
            }
            else
            {
                _nowUse = !_exclusiveAccessoryDoubleTrigger;
                _cooldownTick = _nowUse ? 1 : _pastUse ? logicAccessoryData.GetCooldown() : 0;
                if (_nowUse) _deltaTick = _nowUse ? 0 : _pastUse ? logicAccessoryData.GetCooldown() : 0;
            }
        }

        if (!_exclusiveAccessory)
        {
            if (_nowCount > 0)
            {
                if (_cooldownTick > 0) _deltaTick--;
                if (_deltaTick >= 1) return;

                _cooldownTick = 0;
                _deltaTick = 0;
            }
            else
            {
                _nowCount = 0;
                _cooldownTick = logicAccessoryData.GetCooldown();
                _deltaTick = 0;
            }
        }
        else
        {
            if (_nowCount <= 0) return;
            if (_cooldownTick <= 0 || _deltaTick <= 0 || !_pastUse) return;

            _deltaTick--;
            if (_deltaTick != 0) return;

            _cooldownTick = 0;
            _deltaTick = 0;

            _firstUse = false;
            _pastUse = false;
            _nowUse = false;
        }
    }

    public void TriggerAccessory(bool exclusiveAccessoryDoubleTrigger = false)
    {
        _exclusiveAccessoryDoubleTrigger = exclusiveAccessoryDoubleTrigger;
        {
            if (_exclusiveAccessoryDoubleTrigger) return;
        }

        if (_nowCount < 1) return;
        if (_deltaTick > 0) return;
        if (V2AvailableCheck)
            if (CheckCurrentAccessoryAvailability(true))
                return;

        _firstUse = true;
        _cooldownTick = logicAccessoryData.GetCooldown();
        _deltaTick = _cooldownTick;

        _nowCount--;
        _nowUse = true;

        if (_deltaTick > 0)
        {
            // todo: accessory logic.
        }
    }

    public bool CheckCurrentAccessoryAvailability(bool v2Check)
    {
        if (v2Check) return _cooldownTick != 0 || _deltaTick != 0;
        return _exclusiveAccessory && _nowUse;
    }

    public void Encode(BitStream bitStream, bool isOwn)
    {
        bitStream.WritePositiveIntMax7(_nowCount);
        {
            if (isOwn)
            {
                bitStream.WritePositiveVIntMax255OftenZero(_deltaTick);
                if (bitStream.WritePositiveVIntMax255OftenZero(_cooldownTick) != 1) return;

                bitStream.WritePositiveIntMax16383(0);
                bitStream.WritePositiveIntMax511(0);
            }
            else
            {
                if (!bitStream.WriteBoolean(_nowUse && _exclusiveAccessory)) return;

                bitStream.WritePositiveIntMax16383(0);
                bitStream.WritePositiveIntMax511(0);
            }
        }
    }
}