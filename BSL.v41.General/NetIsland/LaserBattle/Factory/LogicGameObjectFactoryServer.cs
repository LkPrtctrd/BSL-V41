using BSL.v41.Titan.Mathematical.Data;
using BSL.v41.Tools.LaserCsv;
using BSL.v41.Tools.LaserCsv.Data;

namespace BSL.v41.General.NetIsland.LaserBattle.Factory;

public static class LogicGameObjectFactoryServer
{
    public static int CreateGameObjectByClassId(int classId)
    {
        return CreateGameObjectByData(LogicDataTables.GetDataById(GlobalId.CreateGlobalId(classId, 0)));
    }

    public static int CreateGameObjectByData(LogicData logicData)
    {
        return logicData.GetDataType();
    }
}