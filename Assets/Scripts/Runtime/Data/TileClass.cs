using TerrariaClone.Runtime.Player;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TerrariaClone.Runtime.Data
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Pixel Worlds/Items/Tiles/Tile", order = 0)]
    public class TileClass : ItemClass
    {
        [Header("Tile Data")]
        public TileBase tile;
        public TileLayer tileLayer;
        public TileClass wallVariant;
        
        [Header("Lighting")]
        public int lightStrength;
        //removed bool because i can just check if lightStrength is > 0

        public override void Use(PlayerController caller)
        {
            base.Use(caller);
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