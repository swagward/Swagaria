using TerrariaClone.Runtime.Player;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TerrariaClone.Runtime.Data
{
    [CreateAssetMenu(fileName = "NewTorch", menuName = "Pixel Worlds/Items/Tiles/Torch", order = 1)]
    public class TorchTile : TileClass
    {
        public override void Use(PlayerController caller)
        {
            if(Input.GetMouseButton(0))
            {
                if(CanPlaceTorch(caller.mousePos.x, caller.mousePos.y) && caller.terrain.CanPlaceHere(caller.mousePos.x, caller.mousePos.y) && Vector2.Distance(caller.transform.position, caller.mousePos) <=
                caller.reach && Vector2.Distance(caller.transform.position, caller.mousePos) > 1.5f)
                {
                    //can place
                    base.Use(caller);
                    caller.terrain.PlaceTile(GetTile(), caller.mousePos.x, caller.mousePos.y, true);
                }
            }
        }

        private bool CanPlaceTorch(int x, int y)
        {
            if (WorldData.GetTile(x, y, 1) is not null) return false;
            if (WorldData.GetTile(x, y, 0) == TileAtlas.Torch) return false;
            if (WorldData.GetTile(x, y, 0) == TileAtlas.OakTree) return false;
            if (WorldData.GetTile(x, y, 0) == TileAtlas.OakBranch) return false;

            if (WorldData.GetTile(x, y - 1, 1) is not null) return true;
            if (WorldData.GetTile(x, y, 3) is not null) return true;

            else return false;
        }
    }
}