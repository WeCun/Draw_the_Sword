using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    [SerializeField] private List<InventoryItem> inventory;
    private Dictionary<ItemData, InventoryItem> inventoryDictionary;

    [SerializeField] private List<InventoryItem> stash;
    private Dictionary<ItemData, InventoryItem> stashDictionary;

    [SerializeField] private List<InventoryItem> equipment;
    private Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;

    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    
    public UI_ItemSlot[] inventorySlots;
    public UI_ItemSlot[] stashSlots;
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    void Start()
    {
        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();
        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();

        inventorySlots = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        stashSlots = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
    }

    public void UpdateSlot()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].UpdateSlot();
        }
        
        for (int i = 0; i < stashSlots.Length; i++)
        {
            stashSlots[i].UpdateSlot();
        }
    }

    public void EquipItem(ItemData _item)
    {
        ItemData_Equipment newEquipment = _item as ItemData_Equipment;
        InventoryItem newItem = new InventoryItem(newEquipment);

        ItemData_Equipment oldItem = null;

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType)
            {
               oldItem = item.Key; 
            }
        }

        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        RemoveItem(newEquipment);
        
        if (oldItem != null)
        {
            UnlodeEquipment(oldItem);
        }
        
        UpdateSlot();
    }

    private void UnlodeEquipment(ItemData_Equipment oldItem)
    {
        if (inventory.Count < inventorySlots.Length)
        {
            equipment.Remove(equipmentDictionary[oldItem]);
            equipmentDictionary.Remove(oldItem);
            AddItem(oldItem);
        }
    }

    public void AddItem(ItemData _item)
    {
        //直接通过 dict[key] 访问不存在的键会抛出 KeyNotFoundException
        //传统做法是先调用 ContainsKey 检查键是否存在，再通过 dict[key] 获取值，这会进行两次哈希查找。
        //TryGetValue 只需一次哈希查找，效率更高。
        if (_item.itemType == ItemType.Equipment)
        {
            AddEquipment(_item);
        }
        else
        {
            AddStash(_item);
        }
        
        UpdateSlot();
    }

    private void AddStash(ItemData _item)
    {
        if (stashDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            for (int i = 0; i < stashSlots.Length; i++)
            {
                if (stashSlots[i].item == null)
                {
                    stashSlots[i].item = newItem;
                    break;
                }
            }
            stash.Add(newItem);
            stashDictionary.Add(_item, newItem);
        }
    }

    private void AddEquipment(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].item == null)
                {
                    inventorySlots[i].item = newItem;
                    break;
                }
            }
            inventory.Add(newItem);
            inventoryDictionary.Add(_item, newItem);
        }

        
    }
    
    //移除物品
    public void RemoveItem(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (inventorySlots[i].item == value)
                    {
                        inventorySlots[i].item = null;
                        break;
                    }
                }
                inventory.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else
            {
                value.RemoveStack();
            }
        }

        if (stashDictionary.TryGetValue(_item, out InventoryItem stashValue))
        {
            if (stashValue.stackSize <= 1)
            {
                for (int i = 0; i < stashSlots.Length; i++)
                {
                    if (stashSlots[i].item == stashValue)
                    {
                        stashSlots[i].item = null;  
                        break;
                    }
                }
                stash.Remove(stashValue);
                stashDictionary.Remove(_item);
            }
            else
            {
                stashValue.RemoveStack();
            }
        }
        
        UpdateSlot();
    }
}
