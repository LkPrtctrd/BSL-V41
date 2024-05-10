using BSL.v41.General.NetIsland.LaserBattle.Manager;
using BSL.v41.General.NetIsland.LaserBattle.Objects.Laser;
using BSL.v41.Logic.Environment.LaserMessage.Sepo.Player;
using BSL.v41.Supercell.Titan.CommonUtils.Utils;
using BSL.v41.Titan.DataStream;
using BSL.v41.Titan.Mathematical;
using BSL.v41.Titan.Mathematical.Data;

namespace BSL.v41.General.NetIsland.LaserBattle.Objects;

public class LogicGameObjectServer
{
    private readonly LogicBattleModeServer _logicBattleModeServer;

    public readonly Dictionary<int, bool> ShowedInInvisibleForTeams = new()
    {
        { 0, false },
        { 1, false },
        { 2, false },
        { 3, false },
        { 4, false },
        { 5, false },
        { 6, false },
        { 7, false },
        { 8, false },
        { 9, false },
        { 10, false }
    };

    private int _dataId;
    private int _fadeCounter;
    private int _index;
    private LogicGameObjectManagerServer _logicGameObjectManagerServer = null!;
    private int _objectGlobalId;
    private LogicVector2 _position;
    private int _z;

    public LogicGameObjectServer(LogicBattleModeServer logicBattleModeServer, int classId, int instanceId, int z,
        int index)
    {
        _logicBattleModeServer = logicBattleModeServer;
        _dataId = GlobalId.CreateGlobalId(classId, instanceId);
        _position = new LogicVector2();
        _z = z;
        _index = index;
        _fadeCounter = 10;
    }

    public LogicGameObjectServer(LogicBattleModeServer logicBattleModeServer, int classId, int instanceId, int z,
        int index, int objectGlobalId)
    {
        _logicBattleModeServer = logicBattleModeServer;
        _dataId = GlobalId.CreateGlobalId(classId, instanceId);
        _position = new LogicVector2();
        _z = z;
        _index = index;
        _objectGlobalId = objectGlobalId;
        _fadeCounter = 10;
    }

    public virtual void SetObjectGlobalId(int id)
    {
        _objectGlobalId = id;
    }

    public virtual void Encode(BitStream bitStream, bool isOwnObject, int visionIndex, int visionTeam)
    {
        var v1 = LogicMath.Clamp(_position.GetX(), 0, 65535);
        var v2 = LogicMath.Clamp(_position.GetY(), 0, 65535);
        var v3 = LogicMath.Clamp(GetZ(), 0, 65535);

        bitStream.WritePositiveVIntMax65535(v1);
        bitStream.WritePositiveVIntMax65535(v2);
        bitStream.WritePositiveVIntMax65535(v3);
        bitStream.WritePositiveVIntMax255(_index);
        if (GetFadeCounterClient() < 1 &&
            GetType() == ObjectTypeHelperTable.Character.GetObjectType() &&
            ((LogicCharacterServer)this).GetCharacterData().IsHero())
        {
            if (GetPlayer()!.GetTeamIndex() == visionTeam)
                bitStream.WritePositiveIntMax15(10 - 2);
            else if (!ShowedInInvisibleForTeams[visionTeam])
                bitStream.WritePositiveIntMax15(GetFadeCounterClient());
            else
                bitStream.WritePositiveIntMax15(10 - 2 - 2);
        }
        else if (GetType() != ObjectTypeHelperTable.Projectile.GetObjectType())
        {
            bitStream.WritePositiveIntMax15(GetFadeCounterClient());
        }
    }

    public int GetData(int s = -1)
    {
        if (s > -1) _dataId = s;
        return _dataId;
    }

    public virtual bool ShouldDestruct()
    {
        return false;
    }

    public LogicVector2 GetPosition()
    {
        return _position;
    }

    public void SetPosition(int x, int y, int z)
    {
        _position.Set(x, y);
        _z = z;
    }

    public void SetPosition(LogicVector2 logicVector2)
    {
        _position.Set(logicVector2.GetX(), logicVector2.GetY());
    }

    public int GetX()
    {
        return _position.GetX();
    }

    public int GetY()
    {
        return _position.GetY();
    }

    public int GetTileX()
    {
        return _position.GetX() / 300;
    }

    public int GetTileY()
    {
        return _position.GetY() / 300;
    }

    public int GetZ()
    {
        return _z;
    }

    public int GetObjectGlobalId()
    {
        return _objectGlobalId;
    }

    public int GetIndex()
    {
        return _index;
    }

    public LogicPlayer? GetPlayer()
    {
        return _logicBattleModeServer.GetPlayer(_objectGlobalId, false);
    }

    public LogicBattleModeServer GetLogicBattleModeServer()
    {
        return _logicBattleModeServer;
    }

    public LogicGameObjectManagerServer GetLogicGameObjectManager()
    {
        return _logicGameObjectManagerServer;
    }

    public void ChangeFadeCounterServer(int newValue)
    {
        _fadeCounter = newValue;
    }

    public int GetFadeCounterClient()
    {
        return _fadeCounter;
    }

    public void ResetGameObject()
    {
        _dataId = 0;
        _position = null!;
        _z = 0;
        _index = 0;
        _objectGlobalId = 0;
        _fadeCounter = 0;
    }

    public void Destruct()
    {
        _position.Destruct();
    }

    public void AttachLogicGameObjectManager(LogicGameObjectManagerServer logicGameObjectManagerServer, int globalId)
    {
        _logicGameObjectManagerServer = logicGameObjectManagerServer;
        _objectGlobalId = globalId;
    }

    public virtual void Tick()
    {
        ;
    }

    public virtual bool IsAlive()
    {
        return true;
    }

    public virtual int GetRadius()
    {
        return 100;
    }

    public virtual int GetSize()
    {
        return 100;
    }

    public new virtual int GetType()
    {
        return -1;
    }
}