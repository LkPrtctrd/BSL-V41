using BSL.v41.General.NetIsland.LaserBattle.Render.Map;
using BSL.v41.Tools.LaserCsv.Manufacturer.Laser;

namespace BSL.v41.General.NetIsland.LaserBattle.Render.Tile.Laser;

public class LogicTile(char code, int x, int y)
{
    public readonly int LogicX = x, LogicY = y;
    public readonly char TileCode = code;
    public readonly LogicTileData TileData = LogicMapLoader.TileCodeToTileData(code);
    public readonly int TileX = x / 300, TileY = y / 300;

    private bool _destructed;

    public void DestroyTile()
    {
        _destructed = true;
    }

    public bool IsDestroyed()
    {
        return _destructed;
    }

    public bool IsDestroyableWithWeaponType()
    {
        return TileData.GetIsDestructibleNormalWeapon();
    }
}