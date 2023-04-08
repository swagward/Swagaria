using UnityEngine;

namespace PixelWorlds.Runtime.Data
{
    [CreateAssetMenu(fileName = "newLiquid", menuName = "Pixel Worlds/Items/Tile/Liquid")]
    public class LiquidTileClass : TileClass
    {
        public float flowSpeed;
        // public bool destroyTiles;
    }
}