using TerrariaClone.Runtime.Boss;
using TerrariaClone.Runtime.Player;
using UnityEngine;

namespace TerrariaClone.Runtime.Data
{
    [CreateAssetMenu(fileName = "BossSummon", menuName = "Pixel Worlds/Items/Consumables/BossSummon", order = 0)]
    public class BossSummon : ConsumableClass
    {
        [Header("Boss Summon")]
        public int health;
        public GameObject boss;
        public float moveSpeed;
        public float rotateSpeed;

        [Header("Mini Boss")]
        public GameObject miniBossSummon;

        public override void Use(PlayerController caller)
        {
            if (GameManager.BossActive) return;
            //get direction player is facing
            //spawn outside of cameras range
            //have boss come from opposite players direction

            base.Use(caller);
                
            var newBoss = Instantiate(boss, caller.transform.position, Quaternion.identity);
            newBoss.GetComponent<BossAI>().thisBoss = this;
            newBoss.GetComponent<BossAI>().StartEvent();
        }
    }
}
