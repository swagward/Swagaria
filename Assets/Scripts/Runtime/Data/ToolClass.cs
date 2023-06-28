using TerrariaClone.Runtime.Player;
using TerrariaClone.Runtime.Terrain;
using UnityEngine;

namespace TerrariaClone.Runtime.Data
{
    [CreateAssetMenu(fileName = "NewTool", menuName = "Pixel Worlds/Items/Tool")]
    public class ToolClass : ItemClass
    {
        public int reach = 6;
        public ToolType toolType;

        public override void Use(PlayerController caller)
        {
            //base.Use(caller);
            if (Vector2.Distance(caller.transform.position, caller.mousePos) <= caller.reach)
            {
                caller.terrain.RemoveTile(caller.mousePos.x, caller.mousePos.y, (int)toolType, true);
                /*if(toolType == ToolType.Axe && CheckIfTreeStump(caller.mousePos.x, caller.mousePos.y))
                {
                    for (int i = 0; i < TerrainConfig.Settings.maxTreeHeight; i++)
                    {
                        if (WorldData.GetTile(caller.mousePos.x, caller.mousePos.y + i) == TileAtlas.OakTree)
                            caller.terrain.RemoveTile(caller.mousePos.x, caller.mousePos.y + i, 0, false);
                    }
                }*/
            }
        }
    }

    public enum ToolType
    {
        Axe = 0,
        Pickaxe = 1,
        Hammer = 3,
        Bucket = 4
    }
}
