using UnityEngine;

namespace PixelWorlds.Runtime.Data
{
    [CreateAssetMenu]
    public class ItemClass : ScriptableObject
    {
        [SerializeField] private string name;
        [SerializeField] private Sprite icon;
        [SerializeField] private bool canStack;
        [SerializeField] private bool removeOnUse;

        public virtual void Use(/*PlayerController caller*/)
        {
            //Do shit eventually that other classes can inherit from
        }
        
        public virtual ItemClass GetItem() { return this; }
        //public virtual TileClass GetTile() { return null; }
        //public virtual ToolClass GetTool() { return null; }
        //public virtual ConsumableClass GetConsumable() { return null; }
        //public virtual WeaponClass GetWeapon() { return null; }
    }
}
