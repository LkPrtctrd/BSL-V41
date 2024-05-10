using BSL.v41.General.NetIsland.LaserBattle.Objects;
using BSL.v41.Titan.Mathematical;

namespace BSL.v41.General.NetIsland.LaserBattle.Significant.Fly;

public class FlyParticle(LogicVector2 targetVector2)
{
    private LogicGameObjectServer _logicGameObjectServer = null!;
    private int _speed = 1;
    private int _tick = -1;
    private int _travelTick = -2;

    public bool Update()
    {
        if (_travelTick is 0 or -2) return false;

        var z = LogicMath.Clamp(_logicGameObjectServer.GetZ(), 0, 5000);
        var distance = _logicGameObjectServer.GetPosition().GetDistance(targetVector2);

        if (_travelTick < 0)
        {
            _travelTick = distance * _speed / (_logicGameObjectServer.GetLogicBattleModeServer().GetTick() * 1000);
            _travelTick = LogicMath.Min(_travelTick, 30);
        }

        if (_logicGameObjectServer.GetLogicBattleModeServer().GetTicksGone() - _tick < distance * _speed /
            (_logicGameObjectServer.GetLogicBattleModeServer().GetTick() * 1000) / 2 / 2 / 2)
        {
            z += _logicGameObjectServer.GetLogicBattleModeServer().GetTicksGone() - _tick < distance * _speed /
                (_logicGameObjectServer.GetLogicBattleModeServer().GetTick() * 1000) / 2 / 2
                    ? _speed / 50
                    : _speed / 25;
        }
        else
        {
            if (z > 1)
            {
                z -= _speed / 60;
            }
            else
            {
                z = 0;
                _travelTick = 0;
            }
        }

        var v1A = targetVector2.Clone();
        {
            v1A.Substract(_logicGameObjectServer.GetPosition().Clone());
        }

        var deltaX =
            LogicMath.Cos(LogicMath.GetAngle(v1A.GetX(), v1A.GetY())) /
            (_logicGameObjectServer.GetLogicBattleModeServer().GetTicksGone() -
                _tick < distance * _speed /
                (_logicGameObjectServer.GetLogicBattleModeServer().GetTick() * 1000 - _speed) / 2 /
                2 / 2 / 2 / 2
                    ? _logicGameObjectServer.GetLogicBattleModeServer().GetTick() / 2
                    : _logicGameObjectServer.GetLogicBattleModeServer().GetTick());

        var deltaY = LogicMath.Sin(LogicMath.GetAngle(v1A.GetX(), v1A.GetY())) /
                     (_logicGameObjectServer.GetLogicBattleModeServer().GetTicksGone() -
                         _tick < distance * _speed /
                         (_logicGameObjectServer.GetLogicBattleModeServer().GetTick() * 1000 - _speed) / 2 / 2 / 2 / 2 /
                         2
                             ? _logicGameObjectServer.GetLogicBattleModeServer().GetTick() / 2
                             : _logicGameObjectServer.GetLogicBattleModeServer().GetTick());

        _logicGameObjectServer.SetPosition(_logicGameObjectServer.GetX() + deltaX + 1,
            _logicGameObjectServer.GetY() + deltaY, LogicMath.Clamp(z, 0, 5000));

        return true;
    }

    public void SetParent(LogicGameObjectServer logicGameObjectServer, int speed)
    {
        _logicGameObjectServer = logicGameObjectServer;
        _tick = logicGameObjectServer.GetLogicBattleModeServer().GetTicksGone();
        _travelTick = -1;
        _speed = speed;
    }
}