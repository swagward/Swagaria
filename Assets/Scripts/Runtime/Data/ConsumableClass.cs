using UnityEngine;
using PixelWorlds.Runtime.Data;
using PixelWorlds.Runtime.Player;

namespace PixelWorlds.Runtime.Data
{
    public class ConsumableClass : ItemClass
    {
        //default class, nothing happens here because shit has to inherit from this
        [Header("Consumable Type")]
        public ConsumableType consType;

        public override void Use(PlayerController caller)
        {
            base.Use(caller);
        }

        public override ConsumableClass GetConsumable() { return this; }
    }
   

    public enum ConsumableType
    {
        food,
        permaBoost,
        potions,
        bossSummons
    }
}
