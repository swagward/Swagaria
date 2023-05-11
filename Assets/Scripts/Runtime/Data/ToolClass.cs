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
                caller.terrain.RemoveTile(caller.mousePos.x, caller.mousePos.y, (int)toolType, true);
        }
    }

    public enum ToolType
    {
        Axe = 0,
        Pickaxe = 1,
        Hammer = 3,
        Bucket = 45
    }
}
