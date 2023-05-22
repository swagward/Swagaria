using TerrariaClone.Runtime.Player;
using UnityEngine;

namespace TerrariaClone.Runtime.Data
{
    [CreateAssetMenu(fileName = "HealthPotion", menuName = "Pixel Worlds/Items/Consumables/Life Crystal", order = 0)]
    public class LifeCrystalClass : ConsumableClass
    {
        public int healthToAdd;

        public override void Use(PlayerController caller)
        {
            if(Input.GetMouseButtonDown(0))
            {
                base.Use(caller);
                caller.health.IncreaseTotalHealth(healthToAdd);
            }
        }
    }
}
