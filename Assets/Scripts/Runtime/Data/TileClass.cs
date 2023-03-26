using UnityEngine;
using UnityEngine.Tilemaps;

namespace PixelWorlds.Runtime.Data
{
    [CreateAssetMenu(fileName = "newItem", menuName = "Pixel Worlds/Items/Tile")]
    public class TileClass : ItemClass
    {
        public TileBase tile;
        public TileLayer tileLayer;
        // public AudioSource breakSound;
        // public AudioSource placeSound;
        //[SerializeField] private Color mapPixel;

        public override void Use()
        {
            base.Use();
            //other shit for placing tiles lol
        }
    }

    public enum TileLayer : int
    {
        Addon = 0,
        Ground = 1,
        Liquid = 2,
        Background = 3
    }
}