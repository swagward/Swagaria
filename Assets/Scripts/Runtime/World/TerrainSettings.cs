using UnityEngine;

namespace PixelWorlds.Runtime.World
{
    [CreateAssetMenu(fileName = "newSettings", menuName = "Pixel Worlds/Settings")]
    public class TerrainSettings : ScriptableObject
    {
        [Header("Generation")] 
        public Vector2Int worldSize;
        public int seed;
        public int seedRange;

        [Header("Cave Generation")] 
        public float caveFrequency;
        public float caveOctaves;
        public float surfaceValue;

        [Header("Terrain Generation")] 
        public float terrainFrequency;
        public float terrainOctaves;
        public int dirtSpawnHeight;
        public int heightMultiplier;
        public int heightAddition;

        [Header("Vegetation")] 
        public int flowerChance;
        public int treeChance;
        public int minTreeHeight;
        public int maxTreeHeight;
        
        //[Header("Ores")]
        //public OreClass[] ores;
    }
}