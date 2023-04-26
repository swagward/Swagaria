using TerrariaClone.Runtime.Data;
using UnityEngine;
using UnityEngine.Tilemaps;
using static TerrariaClone.Runtime.Data.WorldData;
using static TerrariaClone.Runtime.Terrain.TerrainConfig;

namespace TerrariaClone.Runtime.Terrain
{
    [RequireComponent(typeof(Grid))]
    public class TerrainGenerator : MonoBehaviour
    {
        private Tilemap[] _tilemaps;
        private Lighting _lighting;

        private void Awake() => Init();
        private void Init()
        {
            TerrainConfig.Init();
            
            _tilemaps = GetComponentsInChildren<Tilemap>();
            foreach (var tilemap in _tilemaps)
                tilemap.ClearAllTiles();
            
            Tilemaps = _tilemaps;
            WorldData.Init(Settings.worldSize);

            //_lighting = GetComponent<Lighting>();
            //_lighting.Init();
            
            foreach (var ore in Settings.ores)
            {
                ore.OreMask = new bool[Settings.worldSize.x, Settings.worldSize.y];
                ore.GenerateOres();
            }

            GenerateTerrain();
            //_lighting.IUpdate(4);
        }

        private void GenerateTerrain()
        {
            for (var x = 0; x < Settings.worldSize.x; x++)
            {
                var height = GetHeight(x);
                for (var y = 0; y < height; y++)
                {
                    var tileToPlace = GenerateTile(x, y);
                    PlaceTile(tileToPlace, x, y, false);

                    if (tileToPlace is null or OreTileClass or LiquidTileClass)
                    {
                        if (y < height - Settings.dirtSpawnHeight - Random.Range(2, 5))  
                                PlaceTile(TileAtlas.StoneWall, x, y, false);
                        else if (y < height - 2)
                                PlaceTile(TileAtlas.DirtWall, x, y, false);
                    }

                    // All nature stuff managed here
                    if (GetTile(x, y, 1) == TileAtlas.Grass)
                    {
                        if (GetTile(x, y + 1, 0) is null)
                        {
                            if (Random.Range(0 ,100) > Settings.treeChance)
                                PlaceTree(x, y + 1);
                            else if (Random.Range(0, 100) > Settings.flowerChance)
                                PlaceTile(TileAtlas.Flower, x, y + 1, false);
                        }

                        if (GetTile(x, y - 1, 0) is null && GetTile(x, y, 0) is null)
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
        {   
            //REWRITE THIS LOGIC EVENTUALLY
            
            //Restraints to keep trees in the world and keep a distance between trees (not always perfect but no need to fix)
            if (x < 1 || x > Settings.worldSize.x - 1) return;
            if (GetTile(x + 1, y, 0) is not null || GetTile(x - 1, y, 0) is not null) return;
            if (GetTile(x + 2, y, 0) is not null || GetTile(x - 2, y, 0) is not null) return;
            if (GetTile(x + 1, y + 1, 0) is not null || GetTile(x - 1, y + 1, 0) is not null) return;
            
            var height = Random.Range(Settings.minTreeHeight, Settings.maxTreeHeight);
            for (var i = 0; i < height; i++)
            {
                PlaceTile(TileAtlas.OakTree, x, y + i, false);

                //Generate branches without messing up surrounding tree structures
                if (i >= 1 && i < height - 1)
                {
                    var branchChance = Random.Range(0, 10);
                
                    switch (branchChance)
                    {
                        case < 2:
                            if(GetTile(x + 1, y, 1) is null || GetTile(x + 1, y + i, 0) is null)
                                PlaceTile(TileAtlas.OakBranch, x + 1, y + i, false);
                            break;
                        case > 8:
                            if(GetTile(x - 1, y, 1) is null || GetTile(x - 1, y + i, 0) is null)
                                PlaceTile(TileAtlas.OakBranch, x - 1, y + i, false);
                            break;
                    }
                }
            }
        }

        private void PlaceVine(int x, int y)
        {
            var length = Random.Range(1, 7);
            var i = 0;

            PlaceTile(TileAtlas.Grass, x, y + 1, true);
            while (i < length && GetTile(x, y - i, 1) is null)
            {
                PlaceTile(TileAtlas.Vine, x, y - i, false);
                i++;
            }
        }
        
        public void PlaceTile(TileClass tile, int x, int y, bool overrideTile, bool updateLighting = false)
        {   //Constraints
            if (tile is null) return;
            if (x < 0 || x >= Settings.worldSize.x) return;
            if (y < 0 || y >= Settings.worldSize.y) return;
            if (GetTile(x, y, (int)tile.tileLayer) is not null && overrideTile is false) return;
            
            //Add tile to world and array
            SetTile(tile, x, y, (int)tile.tileLayer);

            if (tile is LiquidTileClass @liquidTile)
            {
                var newLiquidTile = new LiquidTile(x, y, this, @liquidTile);
                StartCoroutine(newLiquidTile.GenerateLiquids());
            }

            //if (updateLighting) _lighting.IUpdate(3);
        }

        public void RemoveTile(int x, int y, int z, bool updateLighting = false)
        {   //Constraints
            if (x < 0 || x >= Settings.worldSize.x) return;
            if (y < 0 || y >= Settings.worldSize.y) return;
            if (GetTile(x, y, z) is null) return;
            
            //Place wall backgrounds when possible
            // var wallTile = GetTile(x, y, 1).wallVariant;
            // if (wallTile is not null) 
            //     PlaceTile(wallTile, x, y, false);
            
            //Remove tile from world and array
            SetTile(null, x, y, z);

            //if (updateLighting) _lighting.IUpdate(3);
        }

        public bool CanPlaceHere(int x, int y)
        {
            //check connected tiles nearby
            if (GetTile(x + 1, y, 1) is not null) return true;
            if (GetTile(x - 1, y, 1) is not null) return true;
            if (GetTile(x, y + 1, 1) is not null) return true;
            if (GetTile(x, y - 1, 1) is not null) return true;
            
            //check if theres a wall background behind
            if (GetTile(x, y, 3) is not null) return true;

            //if theres water or tree then say no lol
            if (GetTile(x, y, 0) == TileAtlas.OakTree || 
                GetTile(x, y, 0) == TileAtlas.OakBranch) return false;
            
            //default
            return false;
        }

        public bool IsIlluminate(int x, int y) 
            => GetTile(x, y, 1) is TorchTile;
    }
}
