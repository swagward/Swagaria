using UnityEngine;

namespace PixelWorlds.Runtime.Data
{
    [CreateAssetMenu]
    public class LiquidTileClass : TileClass
    {
        [SerializeField] private LiquidType flowSpeed;
    }

    public enum LiquidType
    {
        Water = 300,
        Honey = 150,
        Lava = 75
    }
}