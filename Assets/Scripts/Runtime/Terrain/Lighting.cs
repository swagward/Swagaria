using System.Collections;
using UnityEngine;
using static TerrariaClone.Runtime.Terrain.TerrainConfig;
using static TerrariaClone.Runtime.Data.WorldData;

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
        private static readonly int LightMap = Shader.PropertyToID("_LightMap");

        /// <summary>
        /// Needs to be highly optimised because it runs through every tile twice while checking neighbour tiles
        /// 400x200 world would have (400 x 200 x 2 x 4) updates per frame, which is 640,000
        /// </summary>
        public void Init()
        {
            _terrain = GetComponent<TerrainGenerator>();

            _lightValues = new float[Settings.worldSize.x, Settings.worldSize.y];
            lightMap = new Texture2D(Settings.worldSize.x, Settings.worldSize.y);
            
            lightMapOverlay.localScale =
                new Vector3(Settings.worldSize.x, Settings.worldSize.y, 1);
            lightMapOverlay.position = new Vector3(Settings.worldSize.x / 2,
                Settings.worldSize.y / 2, -10);
            
            lightShader.SetTexture(LightMap, lightMap);
            lightMap.filterMode = FilterMode.Point;
        }

        public IEnumerator UpdateLighting()
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
                            (GetTile(x, y, 1) is null && GetTile(x, y, 3) is null)) //check if ground tile and background tile are null
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

                            if (GetTile(x, y, 1) is null)
                                lightLevel -= .75f;
                            else _lightValues[x, y] = lightLevel;
                        }

                        _lightValues[x, y] = lightLevel;
                    }
                }
         
                //reverse loops as to remove any glitched artefacts
                for (var x = Settings.worldSize.y - 1; x >= 0; x--)
                {
                    var lightLevel = sunlightBrightness;
                    for (var y = 0; y < Settings.worldSize.y; y++)
                    {
                        if (_terrain.IsIlluminate(x, y) || //same logic as before
                            (GetTile(x, y, 1) is null && GetTile(x, y, 3) is null))
                            lightLevel = sunlightBrightness;
                        else
                        {
                            var nx1 = Mathf.Clamp(x - 1, 0, Settings.worldSize.x - 1);
                            var nx2 = Mathf.Clamp(x + 1, 0, Settings.worldSize.x - 1);                            
                            var ny1 = Mathf.Clamp(y - 1, 0, Settings.worldSize.y - 1);
                            var ny2 = Mathf.Clamp(y + 1, 0, Settings.worldSize.y - 1);

                            lightLevel = Mathf.Max(_lightValues[nx1, y], _lightValues[nx2, y], 
                                                   _lightValues[x, ny1], _lightValues[x, ny2]);

                            if (GetTile(x, y, 1) is null)
                                lightLevel -= .75f;
                            else _lightValues[x, y] = lightLevel;
                        }

                        _lightValues[x, y] = lightLevel;
                    }
                }
            }
            
            for (var x = 0; x < Settings.worldSize.x; x++)
            {
                for (var y = 0; y < Settings.worldSize.y; y++)
                {
                    lightMap.SetPixel(x, y, new Color(0, 0, 0, _lightValues[x, y]));
                }
            }
            
            lightMap.Apply();
            lightShader.SetTexture(LightMap, lightMap);
        }
    }
}