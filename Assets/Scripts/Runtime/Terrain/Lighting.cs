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
        
        [Header("Testing")]
        public bool smoothLighting;
        public bool update = false;
        public float groundAbsorption;
        public float wallAbsorption;
        public float airAbsorption;
        //public int stopX, stopY;
        public int editRadius;

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
            
            // if(update)
            //     UpdateLighting(2);
        }



        public void UpdateLighting(int iterations, int rootX, int rootY)
        {
            //yield return new WaitForEndOfFrame();
            for (var i = 0; i < iterations; i++)
            {
                rootX -= editRadius;
                rootY -= editRadius;

                var stopX = rootX + editRadius;
                var stopY = rootY + editRadius;
                
                var lightLevel = sunlightBrightness;
                for (var x = rootX; x < stopX; x++)
                {
                    for (var y = stopY - 2; y >= rootY; y--)
                    {
                        //check if this block is a torch OR exposes background
                        if (_terrain.IsIlluminate(x, y) ||
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

                            if (WorldData.GetTile(x, y, 1) is not null) lightLevel -= groundAbsorption;
                            else if (WorldData.GetTile(x, y, 3) is not null) lightLevel -= wallAbsorption;
                            else if (!Mathf.Approximately(lightLevel, sunlightBrightness)) lightLevel -= airAbsorption;
                            // else lightLevel -= 1;
                        }

                        _lightValues[x, y] = lightLevel;
                    }
                }
                
                //reverse calculation to remove artefacts
                for (var x = stopX - 2; x >= rootX; x--)
                {
                    for (var y = rootY; y < stopY; y++)
                    {
                        //check if this block is a torch OR exposes background
                        if (_terrain.IsIlluminate(x, y) ||
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

                            if (WorldData.GetTile(x, y, 1) is not null) lightLevel -= groundAbsorption; //ground
                            else if (WorldData.GetTile(x, y, 3) is not null) lightLevel -= wallAbsorption; //wall
                            else if (!Mathf.Approximately(lightLevel, sunlightBrightness)) lightLevel -= airAbsorption; //air
                            // else lightLevel -= 1;
                        }

                        _lightValues[x, y] = lightLevel;
                    }
                }
                
                //add data from array to the lightmap
                for (var x = 0; x < TerrainConfig.Settings.worldSize.x; x++)
                    for (var y = 0; y < TerrainConfig.Settings.worldSize.y; y++)
                        lightMap.SetPixel(x, y, new Color(0,0,0, _lightValues[x, y] / 15));
            }
            
            //apply and set texture
            lightMap.Apply();
            lightShader.SetTexture("_LightMap", lightMap);
        }
    }
}