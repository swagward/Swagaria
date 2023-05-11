using UnityEngine;

namespace TerrariaClone.Runtime.Data
{
    [System.Serializable]
    public class SlotClass
    {
        [field: SerializeField] public ItemClass Item { get; private set; }
        [field: SerializeField] public int Quantity { get; private set; }

        public SlotClass()
        {
            Item = null;
            Quantity = 0;
        }

        public SlotClass(ItemClass item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public SlotClass(SlotClass slot)
        {
            this.Item = slot.Item;
            this.Quantity = slot.Quantity;
        }

        public void Clear()
        {
            this.Item = null;
            this.Quantity = 0;
        }
        
        public void AddQuantity(int quantity) { Quantity += quantity; }
        public void SubtractQuantity(int quantity)
        {
            Quantity -= quantity;

            if (Quantity <= 0)
                Clear();
        }
        public void AddItem(ItemClass item, int quantity)
        {
            this.Item = item;
            this.Quantity = quantity;
        }
    }
}