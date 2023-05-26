using TerrariaClone.Runtime.Player;
using UnityEngine;

namespace TerrariaClone.Runtime.Data
{
    public class ItemPickup : MonoBehaviour
    {
        public ItemClass collectable;
        [SerializeField] private Inventory inventory;

        private void Awake()
        {
            inventory = FindObjectOfType<Inventory>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.CompareTag("Player"))
            {
                inventory.Add(collectable, 1);
                //debug.log("test");
                Destroy(this.gameObject);
            }
        }
    }

}
