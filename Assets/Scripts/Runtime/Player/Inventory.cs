using TerrariaClone.Runtime.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TerrariaClone.Runtime.Player
{
    public class Inventory : MonoBehaviour
    {
        [Header("Inventory")] 
        [SerializeField] private GameObject inventory;
        [SerializeField] private GameObject inventorySlotsHolder;
        [SerializeField] private SlotClass[] startingItems;
        private SlotClass[] _items;
        private GameObject[] _inventorySlots;
        private bool _inventoryActive;

        [Header("Toolbar")]
        [SerializeField] private TMP_Text currentItemName;
        [SerializeField] private GameObject toolbar;
        [SerializeField] private GameObject toolbarSlotsHolder;
        [SerializeField] private GameObject toolbarSelector;
        private int _currentSlotIndex;
        private GameObject[] _toolbarSlots;
        [field: SerializeField] public ItemClass SelectedItem { get; private set; }
        //SelectedItem is different because it cannot be changed by any means necessary outside of this script

        [Header("Moving Data")] 
        [SerializeField] private GameObject itemCursor;
        [SerializeField] private GameObject heldItem;
        private bool _movingItem;
        private SlotClass _movingSlot;
        private SlotClass _tempSlot;
        private SlotClass _originalSlot;

        public void Init()
        {
            _inventorySlots = new GameObject[inventorySlotsHolder.transform.childCount];
            _toolbarSlots = new GameObject[toolbarSlotsHolder.transform.childCount];

            _items = new SlotClass[_inventorySlots.Length];
            
            for (var i = 0; i < _toolbarSlots.Length; i++)
                _toolbarSlots[i] = toolbarSlotsHolder.transform.GetChild(i).gameObject;

            for (var i = 0; i < inventorySlotsHolder.transform.childCount; i++)
                _inventorySlots[i] = inventorySlotsHolder.transform.GetChild(i).gameObject;

            for (var i = 0; i < _items.Length; i++)
                _items[i] = new SlotClass();

            for (var i = 0; i < startingItems.Length; i++)
                _items[i] = startingItems[i];

            RefreshInventory();

            toolbar.SetActive(true);
            inventory.SetActive(false);
        }
        
        private void Update()
        {
            if (!GameManager.Initialized) return;
            
            if (Input.GetMouseButtonDown(0)) //left click
            {
                if (_inventoryActive)
                {
                    if (_movingItem)
                        EndItemMove();
                    else
                        BeginItemMove();
                }
            }
            else if (Input.GetMouseButtonDown(1)) //right click
            {
                if (_inventoryActive)
                {
                    if (_movingItem)
                        EndItemMove_Separate();
                    else
                        BeginItemMove_Split();
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                //set to opposite of current state
                inventory.SetActive(!inventory.activeSelf);
                toolbar.SetActive(!toolbar.activeSelf);

                _inventoryActive = inventory.activeSelf;
            }

            //manage what happens when you pick up an item
            itemCursor.SetActive(_movingItem); //if moving item them set cursor active
            itemCursor.transform.position = Input.mousePosition;
            if (_movingItem)
            {
                itemCursor.GetComponent<Image>().sprite = _movingSlot.Item.icon; //set sprite to current item sprite
                itemCursor.GetComponentInChildren<TMP_Text>().text = $"{_movingSlot.Item.name} - {_movingSlot.Quantity}"; //get name and item amount 
            }

            //manage toolbar item selection / scrolling
            if (Input.GetAxis("Mouse ScrollWheel") < 0) //scrolling up
                _currentSlotIndex = Mathf.Clamp(_currentSlotIndex + 1, 0, _toolbarSlots.Length - 1); //clamp so it doesnt go under or over
            else if (Input.GetAxis("Mouse ScrollWheel") > 0) //scrolling down
                _currentSlotIndex = Mathf.Clamp(_currentSlotIndex - 1, 0, _toolbarSlots.Length - 1);

            toolbarSelector.transform.position = _toolbarSlots[_currentSlotIndex].transform.position;
            SelectedItem = _items[_currentSlotIndex].Item;
            if (SelectedItem != null)
            {
                currentItemName.transform.GetComponent<TMP_Text>().text = _items[_currentSlotIndex].Item.name;
            }


            

            // Show selected item in hand.
            heldItem.GetComponent<SpriteRenderer>().sprite = SelectedItem?.icon is not null ? SelectedItem.icon : null;
        }

        private void RefreshInventory()
        {
            for (var i = 0; i < _inventorySlots.Length; i++)
            {
                try
                {
                    _inventorySlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                    _inventorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = _items[i].Item.icon;
                    _inventorySlots[i].transform.GetChild(0).GetComponent<Image>().preserveAspect = true;

                    if (_items[i].Item.canStack)
                        _inventorySlots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = 
                            _items[i].Quantity + "";
                    else _inventorySlots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = "";
                }
                catch
                {
                    _inventorySlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                    _inventorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                    _inventorySlots[i].transform.GetChild(0).GetComponent<Image>().preserveAspect = false;
                    _inventorySlots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = "";
                }
            }
            //then run the same code for toolbar
            RefreshToolbar();
        }

        private void RefreshToolbar()
        {
            for (var i = 0; i < _toolbarSlots.Length; i++)
            {
                try
                {
                    //0 being image component, 1 being text
                    _toolbarSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                    _toolbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = _items[i].Item.icon;
                    _toolbarSlots[i].transform.GetChild(0).GetComponent<Image>().preserveAspect = true;
                    _toolbarSlots[i].transform.GetChild(1).GetComponent<TMP_Text>().text =
                        _items[i].Item.canStack ? _items[i].Quantity.ToString() : null;
                }
                catch
                {
                    _toolbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                    _toolbarSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                    _toolbarSlots[i].transform.GetChild(0).GetComponent<Image>().preserveAspect = false;
                    _toolbarSlots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = null;
                }
            }
        }

        public bool Add(ItemClass item, int quantity)
        {
            //check if inv contains item
            var slot = Contains(item);
            if (slot != null && slot.Item.canStack)
                slot.AddQuantity(1);
            else
            {
                foreach (var t in _items)
                {
                    if (t.Item is null) //empty slot
                    {
                        t.AddItem(_movingSlot.Item, _movingSlot.Quantity);
                        break;
                    }
                }
            }

            RefreshInventory();
            return true;
        }
        
        public bool Remove(ItemClass item)
        {
            // items.Remove(item);
            var temp = Contains(item);
            if (temp != null)
            {
                if (temp.Quantity > 1)
                    temp.SubtractQuantity(1);
                else
                {
                    var slotToRemove = 0;
                    for (var i = 0; i < _items.Length; i++)
                    {
                        if (_items[i].Item == item)
                        {
                            slotToRemove = i;
                            break;
                        }
                    }

                    _items[slotToRemove].Clear();
                }
            }
            else
                return false;

            RefreshInventory();
            return true;
        }
        
        public void UseItem()
        {
            _items[_currentSlotIndex].SubtractQuantity(1);
            RefreshInventory();
        }
        
        private SlotClass Contains(Object item)
        {
            foreach (var t in _items)
            {
                if (t.Item == item)
                    return t;
            }

            return null;
        }
        
        private bool BeginItemMove()
        {
            _originalSlot = GetClosestSlot();
            if (_originalSlot?.Item is null)
                return false; //no item to move

            _movingSlot = new SlotClass(_originalSlot); //create new slotclass taking in originSlot data
            _originalSlot.Clear(); //then clear originSlot data

            _movingItem = true;
            RefreshInventory();
            return true;
        }

        private bool EndItemMove()
        {
            _originalSlot = GetClosestSlot();
            if (_originalSlot is null)
            {
                Add(_movingSlot.Item, _movingSlot.Quantity);
                _movingSlot.Clear();
            }
            else
            {
                if (_originalSlot.Item is not null) //if item exists
                {
                    if (_originalSlot.Item == _movingSlot.Item) //they're the same
                    {
                        //stack items
                        if (_originalSlot.Item.canStack)
                        {
                            //items can stack so add together
                            _originalSlot.AddQuantity(_movingSlot.Quantity);
                            _movingSlot.Clear();
                        }
                        else
                            return false;
                    }
                    else
                    {
                        //not the same so swap items
                        _tempSlot = new SlotClass(_originalSlot);                               //a = b
                        _originalSlot.AddItem(_movingSlot.Item, _movingSlot.Quantity); //b = c
                        _movingSlot.AddItem(_tempSlot.Item, _tempSlot.Quantity);     //c = a

                        RefreshInventory();
                        return true;
                    }
                }
                else
                {
                    //place item as usual
                    _originalSlot.AddItem(_movingSlot.Item, _movingSlot.Quantity);
                    _movingSlot.Clear();
                }
            }

            _movingItem = false;
            RefreshInventory();
            return true;
        }

        private bool BeginItemMove_Split()
        {
            _originalSlot = GetClosestSlot();
            if (_originalSlot?.Item is null)
                return false; //no item to move

            _movingSlot = new SlotClass(_originalSlot.Item, Mathf.CeilToInt(_originalSlot.Quantity / 2)); //create new slotclass taking in originSlot data but half the quantity
            _originalSlot.SubtractQuantity(Mathf.CeilToInt(_originalSlot.Quantity / 2));

            if (_originalSlot.Quantity < 1)
            {
                _originalSlot.Clear();
                return false;
            }

            if(_movingSlot.Quantity <= 0)
            {
                _movingSlot.Clear();
                return false;
            }

            _movingItem = true;
            RefreshInventory();
            return true;
        }

        private bool EndItemMove_Separate()
        {
            _originalSlot = GetClosestSlot();
            if (_originalSlot == null)
                return false; //no item to move
            else if (_originalSlot.Item is not null && _originalSlot.Item != _movingSlot.Item)
                return false;

            if(_movingSlot.Quantity > 1)
            {
                _movingSlot.SubtractQuantity(1);
                if (_originalSlot.Item is not null && _originalSlot.Item == _movingSlot.Item)
                    _originalSlot.AddQuantity(1);
                else
                    _originalSlot.AddItem(_movingSlot.Item, 1);
            }

            if (_movingSlot.Quantity < 1)
            {
                _movingItem = false;
                _movingSlot.Clear();
            }
            else
                _movingItem = true;

            RefreshInventory();
            return true;
        }

        private SlotClass GetClosestSlot()
        {
            for (var i = 0; i < _inventorySlots.Length; i++)
            {
                if (Vector2.Distance(_inventorySlots[i].transform.position, Input.mousePosition) <= 45)
                    return _items[i];
            }
            return null;
        }
    }
}
