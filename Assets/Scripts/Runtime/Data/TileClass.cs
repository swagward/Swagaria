using TerrariaClone.Runtime.Player;
using UnityEditor.Animations;
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
        public bool naturallyPlaced;
        public bool dropsItself;

        [Header("Lighting")]
        public int lightStrength;
        //removed bool because i can just check if lightStrength is > 0

        public override void Use(PlayerController caller)
        {
            if(Input.GetMouseButton(0))
            {
                if(caller.terrain.CanPlaceHere(caller.mousePos.x, caller.mousePos.y) && Vector2.Distance(caller.transform.position, caller.mousePos) <=
                caller.reach && Vector2.Distance(caller.transform.position, caller.mousePos) > 1.5f) //Stop player from placing tiles inside their collider)
                {
                    base.Use(caller);
                    caller.terrain.PlaceTile(GetTile(), caller.mousePos.x, caller.mousePos.y, true);
                    Debug.Log("tile used");
                }
            }
        }

        public override TileClass GetTile() { return this; }
    }

    public enum TileLayer
    {
        Addon = 0,
        Ground = 1,
        Liquid = 2,
        Background = 3
    }
}