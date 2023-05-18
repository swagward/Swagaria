using System.Runtime.CompilerServices;
using TerrariaClone.Runtime.Player;
using UnityEngine;

namespace TerrariaClone.Runtime.Boss
{
    public class MiniBossSummon : MonoBehaviour
    {
        private GameObject player;

        private void Start()
        {
            player = FindObjectOfType<PlayerController>().gameObject;
        }

        private void Update()
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, 6 * Time.deltaTime);
        }
    }
}
