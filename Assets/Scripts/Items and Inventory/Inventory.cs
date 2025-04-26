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
    
    private UI_ItemSlot[] inventorySlots;
    private UI_ItemSlot[] stashSlots;
    private UI_ItemSlot[] equipmentSlots;
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
        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();

        inventorySlots = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        stashSlots = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlots = equipmentSlotParent.GetComponentsInChildren<UI_ItemSlot>();
    }

    void UpdateSlot()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            inventorySlots[i].UpdateSlot(inventory[i]);
        }
        
        for (int i = 0; i < stash.Count; i++)
        {
            stashSlots[i].UpdateSlot(stash[i]);
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

        if (oldItem != null)
        {
            equipment.Remove(equipmentDictionary[oldItem]);
            equipmentDictionary.Remove(oldItem);
            AddItem(oldItem);
        }
        
        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        RemoveItem(newEquipment);
        UpdateSlot();
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
            inventory.Add(newItem);
            inventoryDictionary.Add(_item, newItem);
        }

        
    }

    public void RemoveItem(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
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
                stash.Remove(value);
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
