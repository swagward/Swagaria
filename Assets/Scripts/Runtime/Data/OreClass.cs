using UnityEngine;
using PixelWorlds.Runtime.World;
using PixelWorlds.Runtime.Data;

namespace PixelWorlds.Runtime.Data
{
    [System.Serializable]
    public class OreClass
    {
        [SerializeField] private string name;
        public TileClass oreTile;
        public bool[,] oreMask;
        [SerializeField, Range(0, 1)] private float spawnRarity;
        [SerializeField] private float spawnFrequency;
        [SerializeField] private int minSpawnHeight, maxSpawnHeight;

        /// <summary>
        /// Generates x/y coordinates for each assigned ore during generation
        /// </summary>
        public void GenerateOres()
        {
            for (int x = 0; x < TerrainConfig.Settings.worldSize.x; x++)
            {
                for (int y = 0; y < TerrainConfig.Settings.worldSize.y; y++)
                {
                    var v = Mathf.PerlinNoise((x + TerrainConfig.Settings.seed * oreTile.name[0]) * spawnRarity, 
                        (y + TerrainConfig.Settings.seed * oreTile.name[0]) * spawnRarity);
                    oreMask[x, y] = v <= spawnFrequency && y <= maxSpawnHeight && y >= minSpawnHeight;
                }
            }
        }
    }
}
