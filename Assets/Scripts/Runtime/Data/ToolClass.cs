using TerrariaClone.Runtime.Player;
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
                if(toolType == ToolType.Axe && CheckIfTreeStump(caller.mousePos.x, caller.mousePos.y)
                {
                    //pain
                }
            }
        }

        //FORE AXE ONLY
        public bool CheckIfTreeStump(int x, int y)
        {
            return WorldData.GetTile(x, y - 1, 0) is null && WorldData.GetTile(x, y, 0) == TileAtlas.OakTree;
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
