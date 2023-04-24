using System.Collections;
using TerrariaClone.Runtime.Terrain;
using UnityEngine;
using static TerrariaClone.Runtime.Data.WorldData;
using static TerrariaClone.Runtime.Terrain.TerrainConfig;

namespace TerrariaClone.Runtime.Data
{
    public class LiquidTile
    {
        private readonly int _x, _y;
        private readonly TerrainGenerator _terrain;
        private readonly LiquidTileClass _liquidData;
        
        public LiquidTile(int x, int y, TerrainGenerator terrain, LiquidTileClass liquidData)
        {
            _x = x;
            _y = y;
            _terrain = terrain;
            _liquidData = liquidData;
        }

        public IEnumerator GenerateLiquids()
        {
            while (true)
            {
                var offset = Random.Range(_liquidData.flowSpeed, _liquidData.flowSpeed / 2);
                yield return new WaitForSeconds(_liquidData.flowSpeed + offset);
                
                //Check if tile is above 0 and the tile below is empty and on the ground tilemap.
                //If true place liquid tile below
                if(_y > 0 && GetTile(_x, _y - 1, 1) is null)
                    _terrain.PlaceTile(_liquidData, _x, _y - 1, false);
                    
                //Check if tile to the left is in the world space and empty
                //And make sure current tile is grounded
                //If true place liquid tile to the left
                if(_x > 0 && _y > 0 && GetTile(_x - 1, _y, 1) is null && GetTile(_x, _y - 1, 1) is not null)
                    _terrain.PlaceTile(_liquidData, _x - 1, _y, false);
                
                //Check if tile to the right is in world space and empty
                //Make sure current tile is grounded
                //Place tile to right
                if(_x < Settings.worldSize.x && _y > 0 && GetTile(_x + 1, _y, 1) is null && GetTile(_x, _y - 1, 1 ) is not null)
                    _terrain.PlaceTile(_liquidData, _x + 1, _y, false);
                
                if(GetTile(_x, _y, 1) is not null) _terrain.RemoveTile(_x, _y, 2, true);
            }
        }
    }
}