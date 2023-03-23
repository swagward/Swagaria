using System;
using UnityEngine;
using PixelWorlds.Runtime.Data;
using PixelWorlds.Runtime.Player;
using UnityRandom = UnityEngine.Random;

namespace PixelWorlds.Runtime.World
{
    public static class TerrainConfig
    {
        public static TerrainSettings settings;

        public static void Init()
        {
            settings = Resources.Load<TerrainSettings>("DefaultSettings");
            settings.seed = GetSeed();
            
            settings.InitPlayer();
        }

        private static int GetSeed() 
        {
            return UnityRandom.Range(-settings.seedRange, settings.seedRange);
        }

        public static TileClass GenerateTile(int x, int y)
        {
            //Stop world from generating too high
            var height = GetHeight(x);
            if(y > height)
                return null;
                
            //Spawn player
            if (x == settings.worldSize.x / 2)
                settings.player.Spawn(x, (int)height);

            //Generate fewer caves on the surface
            var surfaceCaves = 0f;
            var surfaceFreq = settings.caveFrequency / 1.25f;
            if (y > height - settings.dirtSpawnHeight)
                surfaceCaves = Mathf.PerlinNoise((x + settings.seed) * surfaceFreq, (y + settings.seed) * surfaceFreq);
            
            //Primary cave gen
            var noiseValue = 0f;
            var freq = settings.caveFrequency;
            for (var i = 0; i < settings.caveOctaves; i++)
            {
                if(!(y < height - settings.dirtSpawnHeight)) continue;
                noiseValue += Mathf.PerlinNoise((x + settings.seed) * freq, (y + settings.seed) * freq);
                freq *= 1.5f;
            }
            
            //Get an average so it doesnt go too high, then determine between
            //the two cave frequencies what is a tile and what isn't
            noiseValue /= settings.caveOctaves;
            if (noiseValue > settings.caveVisibility || surfaceCaves > settings.surfaceValue) return null;
            
            //smth with ores eventually
            
            //Basic rules regarding Grass, Dirt and Stone
            if(y < height - settings.dirtSpawnHeight - UnityRandom.Range(2 ,5)) return TileAtlas.Stone;
            if(y < height - 1) return TileAtlas.Dirt;
            if (y <= height) return TileAtlas.Grass;
            return null;
        }

        private static float GetHeight(float x)
        {
            var freq = settings.terrainFrequency;
            var noiseHeight = 0f;
            for (var i = 0; i < settings.terrainOctaves; i++)
            {
                noiseHeight += Mathf.PerlinNoise((x + settings.seed) * freq, settings.seed * freq) * settings.heightMultiplier + settings.heightAddition;
                freq *= 1.5f;
            }
            noiseHeight /= settings.terrainOctaves;
            
            return noiseHeight;
        }
    }
}
