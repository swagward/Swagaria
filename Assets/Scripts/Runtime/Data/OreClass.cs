using UnityEngine;
using PixelWorlds.Runtime.World;

namespace PixelWorlds.Runtime.Data
{
    [System.Serializable]
    public class OreClass
    {
        [Header("Ore Data")]
        public TileClass oreTile;
        public bool[,] OreMask;
        [SerializeField, Range(0, 1)] private float spawnFrequency;
        [SerializeField] private float spawnRarity;
        [SerializeField] private int minSpawnHeight, maxSpawnHeight;

        /// <summary>
        /// Generates x/y coordinates for each assigned ore during generation
        /// </summary>
        public void GenerateOres()
        {
            for (var x = 0; x < TerrainConfig.Settings.worldSize.x; x++)
            {
                for (var y = 0; y < TerrainConfig.Settings.worldSize.y; y++)
                {
                    var v = Mathf.PerlinNoise((x + TerrainConfig.Settings.seed * oreTile.name[0]) * spawnFrequency, 
                        (y + TerrainConfig.Settings.seed * oreTile.name[0]) * spawnFrequency);
                    OreMask[x, y] = v <= spawnRarity && y <= maxSpawnHeight && y >= minSpawnHeight;
                }
            }
        }                    
    }
}
