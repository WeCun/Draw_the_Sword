using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    [SerializeField] private List<InventoryItem> inventory;
    private Dictionary<ItemData, InventoryItem> inventoryDictionary;

    [SerializeField] private Transform inventorySlotParent;
    private UI_ItemSlot[] inventorySlots;

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

        inventorySlots = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
    }

    void UpdateSlot()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            inventorySlots[i].UpdateSlot(inventory[i]);
        }
    }
    
    public void AddItem(ItemData _item)
    {
        //直接通过 dict[key] 访问不存在的键会抛出 KeyNotFoundException
        //传统做法是先调用 ContainsKey 检查键是否存在，再通过 dict[key] 获取值，这会进行两次哈希查找。
        //TryGetValue 只需一次哈希查找，效率更高。
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
        
        UpdateSlot();
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
        
        UpdateSlot();
    }
}
