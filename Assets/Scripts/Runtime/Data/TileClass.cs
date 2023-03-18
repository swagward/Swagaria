using UnityEngine;
using UnityEngine.Tilemaps;

namespace PixelWorlds.Runtime.Data
{
    [CreateAssetMenu(fileName = "newItem", menuName = "Pixel Worlds/Items/Tile")]
    public class TileClass : ItemClass
    {
        public TileBase tile;
        public TileLayer tileLayer;
        //[SerializeField] private Color mapPixel;

        public override void Use()
        {
            base.Use();
            //other shit for placing tiles lol
        }
    }

    public enum TileLayer
    {
        addon = 1,
        ground = 2,
        liquid = 3,
        background = 4
    }
}