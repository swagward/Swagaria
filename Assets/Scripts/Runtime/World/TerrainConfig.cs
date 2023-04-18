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
            Settings = WorldCreation.SettingsToUse ? WorldCreation.SettingsToUse : Resources.Load<TerrainSettings>("DefaultSettings");

            Debug.Log(Settings.name);
            Settings.seed = GetSeed();
            
            Settings.InitPlayer();
        }

        private static int GetSeed() 
        {
            return UnityRandom.Range(-Settings.seedRange, Settings.seedRange);
        }

        public static TileClass GenerateTile(int x, int y)
        {
            //Stop world from generating too high
            var height = GetHeight(x);
            if(y > height)
                return null;
                
            //Spawn player
            // if (x == Settings.worldSize.x / 2)
            //     Settings.player.Spawn(x, (int)height);
            
            //Primary cave gen
            var noiseValue = 0f;
            var caveFreq = Settings.caveFrequency;
            for (var i = 0; i < Settings.caveOctaves; i++)
            {
                if(!(y < height - Settings.dirtSpawnHeight)) continue;
                noiseValue += Mathf.PerlinNoise((x + Settings.seed) * caveFreq, (y + Settings.seed) * caveFreq);
                caveFreq *= 1.5f;
            }

            //Surface cave gen
            var surfaceCaves = 0f;
            var surfaceFreq = Settings.caveFrequency / 1.25f;
            if (y > height - Settings.dirtSpawnHeight)
                surfaceCaves = Mathf.PerlinNoise(1 - (x + Settings.seed) * surfaceFreq, 1 - (y + Settings.seed) * surfaceFreq);
            
            //Get an average so it doesnt go too high, then determine between
            //the two cave frequencies what is a tile and what isn't
            noiseValue /= Settings.caveOctaves;
            if (noiseValue > Settings.caveVisibility || surfaceCaves > Settings.surfaceValue) return null;
            
            foreach(var ore in Settings.ores)
            {
                if (ore.OreMask[x, y])
                    return ore.oreTile;
            }
            
            //Basic rules regarding Grass, Dirt and Stone
            if(y < height - Settings.dirtSpawnHeight - UnityRandom.Range(2 ,5)) return TileAtlas.Stone;
            if(y < height - 1) return TileAtlas.Dirt;
            if (y <= height) return TileAtlas.Grass;
            return null;
        }

        public static float GetHeight(float x)
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
