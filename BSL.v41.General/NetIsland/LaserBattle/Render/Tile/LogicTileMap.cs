using BSL.v41.General.NetIsland.LaserBattle.Render.Map;
using BSL.v41.General.NetIsland.LaserBattle.Render.Tile.Laser;

namespace BSL.v41.General.NetIsland.LaserBattle.Render.Tile;

public class LogicTileMap(RenderSystem renderSystem)
{
    private Dictionary<int, LogicTile> _dynamicTiles = null!;
    private Dictionary<int, int> _dynamicTiles2 = null!;
    private LogicTile[,] _tiles = null!;

    public void LoadMap()
    {
        _tiles = new LogicTile[renderSystem.GetTilemapHeight(), renderSystem.GetTilemapWidth()];
        _dynamicTiles = new Dictionary<int, LogicTile>();
        _dynamicTiles2 = new Dictionary<int, int>();
    }

    public void GenerateTileMap(string data)
    {
        var chars = data.ToCharArray();
        var counter = 0;

        for (var i = 0; i < renderSystem.GetTilemapHeight(); i++)
        for (var j = 0; j < renderSystem.GetTilemapWidth(); j++)
        {
            _tiles[i, j] = new LogicTile(chars[counter], TileToLogic(j), TileToLogic(i));
            {
                counter++;
            }
        }
    }

    public int GetOriginalWallCount()
    {
        return _dynamicTiles.Count;
    }

    public LogicTile GetTile(int x, int y, bool isTile = false, bool isDynamicTile = false,
        LogicBattleModeServer logicBattleModeServer = null!, int dynamicCounter = -1)
    {
        if (!isTile)
        {
            x = LogicToTile(x);
            y = LogicToTile(y);
        }

        if (isDynamicTile)
            return dynamicCounter > -1
                ? _dynamicTiles[_dynamicTiles2[dynamicCounter]]
                : _dynamicTiles[LogicMapLoader.GetTileIndex(logicBattleModeServer, x, y, true)];

        if (x >= 0 && x < renderSystem.GetTilemapWidth() && y >= 0 && y < renderSystem.GetTilemapHeight())
            return _tiles[y, x];

        return null!;
    }

    public void AddDynamicTile(LogicBattleModeServer logicBattleModeServer, int tileX, int tileY, bool isTile = true,
        int dynamicCode = 1)
    {
        var x = 0;
        var y = 0;

        if (isTile)
        {
            x = TileToLogic(tileX);
            y = TileToLogic(tileY);
        }

        var v1 = LogicMapLoader.GetTileIndex(logicBattleModeServer, tileX, tileY, isTile);

        _dynamicTiles.TryAdd(v1, new LogicTile(dynamicCode.ToString()[0], x, y));
        _dynamicTiles2.TryAdd(_dynamicTiles2.Count, v1);
    }

    public void RemoveDynamicTile(LogicBattleModeServer logicBattleModeServer, int tileX, int tileY, bool isTile = true)
    {
        var v1 = LogicMapLoader.GetTileIndex(logicBattleModeServer, tileX, tileY, isTile);
        var v2 = _dynamicTiles2.FirstOrDefault(x => x.Value == v1).Key;

        _dynamicTiles.Remove(v1);
        _dynamicTiles2.Remove(v2);
    }

    public static int LogicToTile(int logicValue)
    {
        return logicValue / 300;
    }

    public static int TileToLogic(int tile)
    {
        return 300 * tile + 150;
    }

    public static int LogicToPathFinderTile(int logicValue)
    {
        return logicValue / 100;
    }

    public static int PathFinderTileToLogic(int tile, bool a2)
    {
        var result = 100 * tile;

        if (a2)
            result += 50;

        return result;
    }
}