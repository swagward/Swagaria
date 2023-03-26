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
                    if (tileToPlace != null) 
                        PlaceTile(tileToPlace, x, y);

                    // All nature stuff managed here
                    if (GetTile(x, y, 1) == TileAtlas.Grass)
                    {
                        if (GetTile(x, y + 1, 1) is null)
                        {
                            if (Random.Range(0, 100) > TerrainConfig.Settings.treeChance)
                                PlaceTree(x, y + 1);
                            else if (Random.Range(0 ,100) > TerrainConfig.Settings.flowerChance)
                                PlaceTile(TileAtlas.Stone, x, y + 1);
                        }

                        if (GetTile(x, y - 1, 1) is null)
                        {
                            var i = 0;
                            while (i < 5)
                            {
                                if (GetTile(x, y - i, 1) is null)
                                {
                                   PlaceVine(x, y - i);
                                   break;
                                }

                                i++;
                            }
                        }
                    }
                }
            }
        }

        private void PlaceTree(int x, int y)
        {   //Restraints
            if (x < 3 || x > TerrainConfig.Settings.worldSize.x - 3) return;
            if (GetTile(x + 1, y, 1) is not null || GetTile(x - 1, y, 1) is not null) return;
            if (GetTile(x + 2, y, 1) is not null || GetTile(x - 2, y, 1) is not null) return;
            if (GetTile(x + 1, y - 1, 0) is not null || GetTile(x - 1, y - 1, 0) is not null) return;
            
            var height = Random.Range(TerrainConfig.Settings.minTreeHeight, TerrainConfig.Settings.maxTreeHeight);
            for (var i = 0; i < height; i++)
            {
                PlaceTile(TileAtlas.Stone, x, y + i);

                if (i >= 1)
                {
                    var branchChance = Random.Range(0, 10);
                
                    switch (branchChance)
                    {
                        case < 2:
                            PlaceTile(TileAtlas.Dirt, x + 1, y + i - 1);
                            break;
                        case > 8:
                            PlaceTile(TileAtlas.Dirt, x - 1, y + i - 1);
                            break;
                    }
                }
            }
        }

        private void PlaceVine(int x, int y)
        {
            var length = Random.Range(1, 7);
            var i = 0;
            while (i < length && GetTile(x, y - i, 1) is null)
            {
                PlaceTile(TileAtlas.Vine, x, y - i);
                i++;
            }
        }
        
        private void PlaceTile(TileClass tile, int x, int y)
        {   //Constraints
            if (tile == null) return;
            if (x < 0 || x >= TerrainConfig.Settings.worldSize.x) return;
            if (y < 0 || y >= TerrainConfig.Settings.worldSize.y) return;
            if (GetTile(x, y, (int)tile.tileLayer) != null) return;
            
            //Add tile to world and array then play tile sound if possible
            SetTile(tile, x, y, (int)tile.tileLayer);
            //tile.placeSound?.Play();
        }
        
        private void RemoveTile(int x, int y, int z)
        {   //Constraints
            if (x < 0 || x >= TerrainConfig.Settings.worldSize.x) return;
            if (y < 0 || y >= TerrainConfig.Settings.worldSize.y) return;
            if (GetTile(x, y, z) == null) return;
            
            //Remove tile from world and array
            SetTile(null, x, y, z);
        }
    }
}
