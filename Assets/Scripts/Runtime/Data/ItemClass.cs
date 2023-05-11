using TerrariaClone.Runtime.Player;
using UnityEngine;

namespace TerrariaClone.Runtime.Data
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Pixel Worlds/Items/Item")]
    public class ItemClass : ScriptableObject
    {
        [Header("Universal Data")]
        [SerializeField] private new string name;
        public Sprite icon;
        public bool canStack;
        [SerializeField] private bool removeOnUse; 
        [SerializeField] private AudioClip useSound;

        public virtual void Use(PlayerController caller)
        {
            //Play sounds
            caller.audioPlayer.clip = useSound;
            caller.audioPlayer.Play();

            //Do shit eventually that other classes can inherit from
            Debug.Log($"{name} was used");
        }
        
        public virtual ItemClass GetItem() { return this; }
        public virtual TileClass GetTile() { return null; }
        //public virtual ToolClass GetTool() { return null; }
        public virtual ConsumableClass GetConsumable() { return null; }
        //public virtual WeaponClass GetWeapon() { return null; }
    }
    
    
}
