using UnityEngine;
using PixelWorlds.Runtime.Player;

namespace PixelWorlds.Runtime.Data
{
    [CreateAssetMenu(fileName = "BossSummon", menuName = "Pixel Worlds/Items/Consumables/BossSummon", order = 0)]
    public class BossSummon : ConsumableClass
    {
        public GameObject boss;

        public override void Use(PlayerController caller)
        {
            //caller.anim.SetTrigger("SummonBoss");
            var newBoss = Instantiate(boss, caller.transform.position, Quaternion.identity);

        }
    }
}
