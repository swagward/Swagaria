using PixelWorlds.Runtime.Player;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PixelWorlds.Runtime.Data
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Pixel Worlds/Items/Tiles/Tile", order = 0)]
    public class TileClass : ItemClass
    {
        [Header("Tile Data")]
        public TileBase tile;
        public TileLayer tileLayer;
        public TileClass wallVariant;
        // public AudioSource breakSound;
        // public AudioSource placeSound;
        //[SerializeField] private Color mapPixel;

        public override void Use(PlayerController caller)
        {
            base.Use(caller);
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