using System;
using UnityEngine;
using PixelWorlds.Runtime.Data;
using UnityRandom = UnityEngine.Random;

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
            return UnityRandom.Range(-Settings.seedRange, Settings.seedRange);
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
                // if(!(y < height - Convert.ToInt16(Settings.dirtSpawnHeight / UnityRandom.Range(2, 4)))) continue;
                noiseValue += Mathf.PerlinNoise((x + Settings.seed) * freq, (y + Settings.seed) * freq);
                freq *= 1.5f;
            }

            var surfaceCaves = 0f;
            if (y > height - Settings.dirtSpawnHeight)
                surfaceCaves = Mathf.PerlinNoise(x * freq, y * freq);

            noiseValue /= Settings.caveOctaves;
            if (noiseValue > Settings.caveVisibility || surfaceCaves > Settings.surfaceValue) return null;
            
            //smth with ores eventually
            
            if(y < height - Settings.dirtSpawnHeight - UnityRandom.Range(2 ,5)) return TileAtlas.Stone;
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
