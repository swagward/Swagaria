using PixelWorlds.Runtime.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PixelWorlds.Runtime.World
{
    [RequireComponent(typeof(Grid))]
    public class TerrainGenerator : MonoBehaviour
    {
        private Tilemap[] _tilemaps;

        private void Awake() => Init();
        private void Init()
        {
            TerrainConfig.Init();
            
            _tilemaps = GetComponentsInChildren<Tilemap>();
            foreach (var tilemap in _tilemaps)
                tilemap.ClearAllTiles();
            
            WorldData.Tilemaps = _tilemaps;
            WorldData.Init(TerrainConfig.Settings.worldSize);

            GenerateTerrain(TerrainConfig.Settings.worldSize);
        }

        private void GenerateTerrain(Vector2Int worldSize)
        {
            for (var x = 0; x < worldSize.x; x++)
            {
                for (var y = 0; y < worldSize.y; y++)
                {
                    var tileToPlace = TerrainConfig.GenerateTile(x, y);
                    if (tileToPlace is not null) 
                        PlaceTile(tileToPlace, x, y);
                }
            }
        }

        private void PlaceTile(TileClass tile, int x, int y)
        {   //Constraints
            if (tile is null) return;
            if (x < 0 || x >= TerrainConfig.Settings.worldSize.x) return;
            if (y < 0 || y >= TerrainConfig.Settings.worldSize.y) return;
            if (WorldData.GetTile(x, y, (int)tile.tileLayer) is not null) return;
            
            //Add tile to world and array then play tile sound if possible
            WorldData.SetTile(tile, x, y);
            //tile.placeSound?.Play();
        }
        
        private void RemoveTile(int x, int y, int z)
        {   //Constraints
            if (x < 0 || x >= TerrainConfig.Settings.worldSize.x) return;
            if (y < 0 || y >= TerrainConfig.Settings.worldSize.y) return;
            if (WorldData.GetTile(x, y, z) is null) return;
            
            //Remove tile from world and array
            WorldData.SetTile(null, x, y, z);
        }
    }
}
