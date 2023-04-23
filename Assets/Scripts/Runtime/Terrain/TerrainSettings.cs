using TerrariaClone.Runtime.Data;
using TerrariaClone.Runtime.Player;
using UnityEngine;

namespace TerrariaClone.Runtime.Terrain
{
    [CreateAssetMenu(fileName = "newSettings", menuName = "Pixel Worlds/Settings")]
    public class TerrainSettings : ScriptableObject
    { 
        public PlayerController Player { get; private set; }

        public void InitPlayer()
            => Player = FindObjectOfType<PlayerController>(); 
        
        [Header("Generation")] 
        public Vector2Int worldSize;
        public int seed;
        public int seedRange;

        [Header("Cave Generation")] 
        public float caveFrequency;
        public float caveOctaves;
        public float surfaceValue;
        public float caveVisibility;

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
        
        [Header("Ores")]
        public OreClass[] ores;
    }
}