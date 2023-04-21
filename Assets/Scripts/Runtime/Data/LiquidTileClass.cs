using UnityEngine;

namespace PixelWorlds.Runtime.Data
{
    [CreateAssetMenu(fileName = "newLiquid", menuName = "Pixel Worlds/Items/Tiles/Liquid", order = 2)]
    public class LiquidTileClass : TileClass
    {
        [Header("Liquid Data")]
        public float flowSpeed;
        // public bool destroyTiles;
    }
}