using System.Runtime.CompilerServices;
using TerrariaClone.Runtime.Player;
using UnityEngine;

namespace TerrariaClone.Runtime.Boss
{
    public class MiniBossSummon : MonoBehaviour
    {
        private HealthManager player;
        public int damage;

        private void Start()
        {
            player = FindObjectOfType<HealthManager>();
        }

        private void Update()
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, 6 * Time.deltaTime);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
/*                var player = collision.gameObject.GetComponent<HealthManager>();*/
                player.TakeDamage(10, collision.GetComponent<PlayerController>());
                //Debug.Log(collision.GetComponent<PlayerController>());
            }
        }
    }
}
