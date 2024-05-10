namespace BSL.v41.General.NetIsland.LaserBattle.Significant;

public class LogicPoisonServer(int a1, int a2, int a3)
{
    public int PoisonType { get; } = a1;
    public int PoisonTileX { get; } = a2;
    public int PoisonTileY { get; } = a3;

    public void Tick(LogicBattleModeServer logicBattleModeServer)
    {
        // todo.
    }

    public int GetTickCount(int a1)
    {
        return a1 == 4 ? 2 : 4;
    }

    public bool AllowStacking(int a1)
    {
        return a1 == 4;
    }
}