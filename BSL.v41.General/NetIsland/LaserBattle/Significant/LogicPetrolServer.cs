using BSL.v41.General.NetIsland.LaserBattle.Objects;
using BSL.v41.General.NetIsland.LaserBattle.Objects.Laser;
using BSL.v41.General.NetIsland.LaserBattle.Render.Tile;
using BSL.v41.Tools.LaserCsv;

namespace BSL.v41.General.NetIsland.LaserBattle.Significant;

public class LogicPetrolServer(int a1)
{
    public int PetrolType { get; set; } = a1;

    public int PetrolTileX { get; private set; } = (int)0L;
    public int PetrolTileY { get; private set; } = (int)0L;

    public int OwnerPlayerIndex { get; set; } = -1;

    public void Tick(LogicBattleModeServer logicBattleModeServer)
    {
        // todo.
    }

    public void Ignite(LogicGameObjectServer logicGameObjectServer, int logicX, int logicY, int ownerPlayerIndex)
    {
        var areaEffectData = LogicDataTables.GetAreaEffectByName("PetrolDistributionArea");

        PetrolType = 1;

        PetrolTileX = LogicTileMap.LogicToTile(logicX);
        PetrolTileY = LogicTileMap.LogicToTile(logicY);

        OwnerPlayerIndex = ownerPlayerIndex;

        var areaEffectServer = new LogicAreaEffectServer(
            logicGameObjectServer.GetLogicBattleModeServer(),
            areaEffectData.GetClassId(), areaEffectData.GetInstanceId(), logicGameObjectServer.GetIndex());
        {
            areaEffectServer.SetPosition(logicX, logicY, 0);
        }

        logicGameObjectServer.GetLogicGameObjectManager().AddLogicGameObject(areaEffectServer);
    }
}