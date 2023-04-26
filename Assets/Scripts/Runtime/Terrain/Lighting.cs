using System.Collections;
using TerrariaClone.Runtime.Data;
using UnityEngine;

namespace TerrariaClone.Runtime.Terrain
{
    public class Lighting : MonoBehaviour
    {
        private TerrainGenerator _terrain;
        private float[,] _lightValues;
        public float sunlightBrightness = 15;

        public Texture2D lightMap;
        public Transform lightOverlay;
        public Material lightShader;
        
        public bool smoothLighting;
        public bool update = false;

        public void Init()
        {
            _terrain = GetComponent<TerrainGenerator>();

            _lightValues = new float[TerrainConfig.Settings.worldSize.x, TerrainConfig.Settings.worldSize.y];
            lightMap = new Texture2D(TerrainConfig.Settings.worldSize.x, TerrainConfig.Settings.worldSize.y);
            
            lightOverlay.localScale =
                new Vector3(TerrainConfig.Settings.worldSize.x, TerrainConfig.Settings.worldSize.y, 1);
            lightOverlay.position = new Vector3(TerrainConfig.Settings.worldSize.x / 2,
                TerrainConfig.Settings.worldSize.y / 2, -10);
            
            lightShader.SetTexture("_LightMap", lightMap);

        }

        private void Update()
        {
            lightMap.filterMode = smoothLighting ? FilterMode.Bilinear : FilterMode.Point;
            
            if(update)
                IUpdate(2);
        }

        public void IUpdate(int iterations)
        {
            StopCoroutine(UpdateLighting(iterations));
            StartCoroutine(UpdateLighting(iterations));
        }

        private IEnumerator UpdateLighting(int iterations)
        {
            yield return new WaitForEndOfFrame();
            for (var i = 0; i < iterations; i++)
            {
                for (var x = 0; x < TerrainConfig.Settings.worldSize.x; x++)
                {
                    var lightLevel = sunlightBrightness;
                    for (var y = TerrainConfig.Settings.worldSize.y - 1; y >= 0; y--)
                    {
                        //check if this block is a torch OR exposes background
                        if (/*_terrain.IsIlluminate(x, y) ||*/
                            (WorldData.GetTile(x, y, 1) is null && WorldData.GetTile(x, y, 3) is null))
                            lightLevel = sunlightBrightness;
                        else
                        {
                            //else find the brightest neighbour
                            var nx1 = Mathf.Clamp(x - 1, 0, TerrainConfig.Settings.worldSize.x - 1);
                            var nx2 = Mathf.Clamp(x - 1, 0, TerrainConfig.Settings.worldSize.x - 1);
                            var ny1 = Mathf.Clamp(y - 1, 0, TerrainConfig.Settings.worldSize.y - 1);
                            var ny2 = Mathf.Clamp(y - 1, 0, TerrainConfig.Settings.worldSize.y - 1);

                            lightLevel = Mathf.Max(_lightValues[x, y], 
                                _lightValues[nx1, y], _lightValues[nx2, y], 
                                _lightValues[x, ny1], _lightValues[x, ny2]);

                            if (WorldData.GetTile(x, y, 1) is not null) lightLevel -= 2;
                            else if (WorldData.GetTile(x, y, 3) is not null) lightLevel -= 2;
                            else if (!Mathf.Approximately(lightLevel, sunlightBrightness)) lightLevel -= 2;
                            else lightLevel -= 2;
                        }

                        _lightValues[x, y] = lightLevel;
                    }
                }
                
                //reverse calculation to remove artefacts
                for (var x = TerrainConfig.Settings.worldSize.x - 1; x >= 0; x--)
                {
                    var lightLevel = sunlightBrightness;
                    for (var y = 0; y < TerrainConfig.Settings.worldSize.y; y++)
                    {
                        //check if this block is a torch OR exposes background
                        if (/*_terrain.IsIlluminate(x, y) ||*/
                            (WorldData.GetTile(x, y, 1) is null && WorldData.GetTile(x, y, 3) is null))
                            lightLevel = sunlightBrightness;
                        else
                        {
                            //else find the brightest neighbour
                            var nx1 = Mathf.Clamp(x + 1, 0, TerrainConfig.Settings.worldSize.x - 1);
                            var nx2 = Mathf.Clamp(x + 1, 0, TerrainConfig.Settings.worldSize.x - 1);
                            var ny1 = Mathf.Clamp(y + 1, 0, TerrainConfig.Settings.worldSize.y - 1);
                            var ny2 = Mathf.Clamp(y + 1, 0, TerrainConfig.Settings.worldSize.y - 1);

                            lightLevel = Mathf.Max(_lightValues[x, y], 
                                _lightValues[nx1, y], _lightValues[nx2, y], 
                                _lightValues[x, ny1], _lightValues[x, ny2]);

                            if (WorldData.GetTile(x, y, 1) is not null) lightLevel -= 2;
                            else if (WorldData.GetTile(x, y, 3) is not null) lightLevel -= 2;
                            else if (!Mathf.Approximately(lightLevel, sunlightBrightness)) lightLevel -= 2;
                            else lightLevel -= 2f;
                        }

                        _lightValues[x, y] = lightLevel;
                    }
                }
            }
            
            //add data from array to the lightmap
            for (var x = 0; x < TerrainConfig.Settings.worldSize.x; x++)
            {
                for (var y = 0; y < TerrainConfig.Settings.worldSize.y; y++)
                {
                    lightMap.SetPixel(x, y, new Color(0,0,0, _lightValues[x, y] / 15));
                }
            }
            
            //apply and set texture
            lightMap.Apply();
            lightShader.SetTexture("_LightMap", lightMap);
        }
    }
}