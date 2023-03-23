using PixelWorlds.Runtime.Data;
using UnityEngine;
using UnityEngine.Tilemaps;
using static PixelWorlds.Runtime.Data.WorldData;

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
            
            tilemaps = _tilemaps;
            WorldData.Init(TerrainConfig.settings.worldSize);

            GenerateTerrain(TerrainConfig.settings.worldSize);
        }

        private void GenerateTerrain(Vector2Int worldSize)
        {
            for (var x = 0; x < worldSize.x; x++)
            {
                for (var y = 0; y < worldSize.y; y++)
                {
                    var tileToPlace = TerrainConfig.GenerateTile(x, y);
                    if (tileToPlace != null) 
                        PlaceTile(tileToPlace, x, y);
                }
            }
        }

        private void PlaceTile(TileClass tile, int x, int y)
        {   //Constraints
            if (tile == null) return;
            if (x < 0 || x >= TerrainConfig.settings.worldSize.x) return;
            if (y < 0 || y >= TerrainConfig.settings.worldSize.y) return;
            if (GetTile(x, y, (int)tile.tileLayer) != null) return;
            
            //Add tile to world and array then play tile sound if possible
            SetTile(tile, x, y, (int)tile.tileLayer);
            //tile.placeSound?.Play();
        }
        
        private void RemoveTile(int x, int y, int z)
        {   //Constraints
            if (x < 0 || x >= TerrainConfig.settings.worldSize.x) return;
            if (y < 0 || y >= TerrainConfig.settings.worldSize.y) return;
            if (GetTile(x, y, z) == null) return;
            
            //Remove tile from world and array
            SetTile(null, x, y, z);
        }
    }
}
