using BSL.v41.General.NetIsland.LaserBattle.Render.Tile;

namespace BSL.v41.General.NetIsland.LaserBattle.Render;

public class RenderSystem
{
    protected internal int Height { get; set; }
    protected internal int Width { get; set; }
    protected internal LogicTileMap LogicTileMap { get; set; } = null!;

    public int GetTilemapWidth()
    {
        return Width;
    }

    public bool GetWaterTile(int x, int y)
    {
        return LogicTileMap.GetTile(x, y).TileData.GetTileCode() == 'W';
    }

    public int GetTilemapHeight()
    {
        return Height;
    }
}