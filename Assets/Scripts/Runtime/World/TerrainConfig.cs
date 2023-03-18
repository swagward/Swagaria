using UnityEngine;
using PixelWorlds.Runtime.Data;

namespace PixelWorlds.Runtime.World
{
    public static class TerrainConfig
    {
        public static TerrainSettings Settings;

        public static void Init()
        {
            Settings = Resources.Load<TerrainSettings>("DefaultSettings");
            Settings.seed = GetSeed();
        }

        private static int GetSeed() 
        {
            return Random.Range(-Settings.seedRange, Settings.seedRange);
        }

        public static TileClass GenerateTile(int x, int y)
        {
            var height = GetHeight(x);
            if(y > height)
                return null;

            var noiseValue = 0f;
            var freq = Settings.caveFrequency;
            for (var i = 0; i < Settings.caveOctaves; i++)
            {
                noiseValue += Mathf.PerlinNoise((x + Settings.seed) * freq, (y + Settings.seed) * freq);
                freq *= 1.5f;
            }

            noiseValue /= Settings.caveOctaves;
            if (noiseValue > Settings.surfaceValue) return null;
            
            //smth with ores eventually
            
            if(y < height - Settings.dirtSpawnHeight - Random.Range(2 ,5)) return TileAtlas.Stone;
            if(y < height - 1) return TileAtlas.Dirt;
            if (y <= height) return TileAtlas.Grass;
            return null;
        }

        private static float GetHeight(float x)
        {
            var freq = Settings.terrainFrequency;
            var noiseHeight = 0f;
            for (var i = 0; i < Settings.terrainOctaves; i++)
            {
                noiseHeight += Mathf.PerlinNoise((x + Settings.seed) * freq, Settings.seed * freq) * Settings.heightMultiplier + Settings.heightAddition;
                freq *= 1.5f;
            }
            noiseHeight /= Settings.terrainOctaves;
            
            return noiseHeight;
        }
    }
}
