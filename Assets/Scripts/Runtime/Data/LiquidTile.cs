using System.Collections;
using PixelWorlds.Runtime.World;

namespace PixelWorlds.Runtime.Data
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

        // public IEnumerator GenerateLiquids()
        // {
        //     // while (true)
        //     // {
        //     //     yield return new WaitForSeconds(_liquidData.flowSpeed);
        //     //     
        //     //     //on ground
        //     //     if(_y > 0 && WorldData.GetTile(_x, _y - 1, 1) is null)
        //     //         _terrain.PlaceTile(_liquidData, _x, _y - 1, false);
        //     //         
        //     //     //to left
        //     //     if(_x > 0 && _y > 0
        //     //               && WorldData.GetTile())
        //     //         
        //     //     //to right
        //     // }
        // }
    }
}