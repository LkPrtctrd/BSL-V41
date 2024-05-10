using BSL.v41.General.NetIsland.LaserBattle.Render.Tile.Metadata;

namespace BSL.v41.General.NetIsland.LaserBattle.Render.Help;

public static class TileHelper
{
    public static void DestroyAllBlocksInMap(LogicTileMetadata? logicTileMetadata)
    {
        for (var i = 0; i < logicTileMetadata!.RenderSystem.GetTilemapWidth(); i++)
        for (var j = 0; j < logicTileMetadata.RenderSystem.GetTilemapHeight(); j++)
        {
            var tile = logicTileMetadata.LogicTileMap.GetTile(i, j, true);
            {
                if (tile.TileData.GetRespawnSeconds() > 0 || tile.TileData.GetIsDestructible() ||
                    tile.TileData.GetIsDestructibleNormalWeapon()) tile.DestroyTile();
            }
        }
    }
}