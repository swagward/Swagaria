using TerrariaClone.Runtime.Player;
using UnityEngine;

namespace TerrariaClone.Runtime.Data
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "Pixel Worlds/Items/Weapon", order = 1)]
    public class WeaponClass : ItemClass
    {
        [Header("Weapon Info")]
        public int damage;

        public override void Use(PlayerController caller)
        {
            base.Use(caller);
        }
    }

    public enum WeaponType
    {
        Melee,
        Range
    }
}
