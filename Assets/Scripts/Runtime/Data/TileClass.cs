using UnityEngine;
using UnityEngine.Tilemaps;

namespace PixelWorlds.Runtime.Data
{
    [CreateAssetMenu(fileName = "newItem", menuName = "Pixel Worlds/Items/Tile")]
    public class TileClass : ItemClass
    {
        public TileBase tile;
        public TileLayer tileLayer;
        public AudioSource breakSound;
        public AudioSource placeSound;
        //[SerializeField] private Color mapPixel;

        public override void Use()
        {
            base.Use();
            //other shit for placing tiles lol
        }
    }

    public enum TileLayer
    {
        addon,
        ground,
        liquid,
        background
    }
}