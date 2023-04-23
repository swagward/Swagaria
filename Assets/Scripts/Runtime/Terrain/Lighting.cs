using System.Collections;
using TerrariaClone.Runtime.Data;
using UnityEngine;
using static TerrariaClone.Runtime.Terrain.TerrainConfig;

namespace TerrariaClone.Runtime.Terrain
{
    public class Lighting : MonoBehaviour
    {
        public int iterationCount;
        public float sunlightBrightness = 15f;
        public Texture2D lightMap;
        public Transform lightMapOverlay;
        public Material lightShader;
        
        private TerrainGenerator _terrain;
        private float[,] _lightValues;
        
        public void Init()
        {
            _terrain = GetComponent<TerrainGenerator>();

            _lightValues = new float[TerrainConfig.Settings.worldSize.x, TerrainConfig.Settings.worldSize.y];
            lightMap = new Texture2D(TerrainConfig.Settings.worldSize.x, TerrainConfig.Settings.worldSize.y);
            
            lightMapOverlay.localScale =
                new Vector3(TerrainConfig.Settings.worldSize.x, TerrainConfig.Settings.worldSize.y, 1);
            lightMapOverlay.position = new Vector2(TerrainConfig.Settings.worldSize.x / 2,
                TerrainConfig.Settings.worldSize.y / 2);
            
            lightShader.SetTexture("_LightMap", lightMap);
            lightMap.filterMode = FilterMode.Point;
        }

        private void Update()
        {
            
        }

        private IEnumerator UpdateLighting()
        {
            yield return new WaitForEndOfFrame();
            for (var i = 0; i < iterationCount; i++)
            {
                for (var x = 0; x < Settings.worldSize.x; x++)
                {
                    var lightLevel = sunlightBrightness;
                    for (var y = Settings.worldSize.y - 1; y >= 0; y--)
                    {
                        if (_terrain.IsIlluminate(x, y) || //check if current tile is a torch tile
                            (WorldData.GetTile(x, y, 1) is null && WorldData.GetTile(x, y, 3) is null)) //check if ground tile and background tile are null
                            lightLevel = sunlightBrightness;
                        else
                        {
                            //find the brightest neighbour
                            var nx1 = Mathf.Clamp(x - 1, 0, Settings.worldSize.x - 1);
                            var nx2 = Mathf.Clamp(x + 1, 0, Settings.worldSize.x - 1);
                            var ny1 = Mathf.Clamp(y + 1, 0, Settings.worldSize.y - 1);
                            var ny2 = Mathf.Clamp(y - 1, 0, Settings.worldSize.y - 1);
                            
                            lightLevel = Mathf.Max(_lightValues[nx1, y], _lightValues[nx2, y], 
                                                   _lightValues[x, ny1], _lightValues[y, ny2]);

                            if (WorldData.GetTile(x, y, 1) is null)
                                lightLevel -= .75f;
                            else _lightValues[x, y] = lightLevel;
                        }

                        _lightValues[x, y] = lightLevel;
                    }
                }
            }
        }
    }
}