using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour, ISaveManager
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
    [SerializeField] private Transform statSlotParent;
    
    public UI_ItemSlot[] inventorySlots;
    public UI_ItemSlot[] stashSlots;
    public UI_EquipmentSlot[] equipmentSlots;
    public UI_StatSlot[] statSlots;

    [Header("Data base")] 
    public List<ItemData> loadedItems;
    
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        
        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();
        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();
        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();

        inventorySlots = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        stashSlots = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlots = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        statSlots = statSlotParent.GetComponentsInChildren<UI_StatSlot>();
    }

    void Start()
    {
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
           equipmentSlots[i].UpdateSlot(); 
        }
        
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].UpdateSlot();
        }
        
        for (int i = 0; i < stashSlots.Length; i++)
        {
            stashSlots[i].UpdateSlot();
        }

        UpdateStatUI();
    }
    

    public void UpdateStatUI()
    {
        for (int i = 0; i < statSlots.Length; i++)
        {
            statSlots[i].UpdateStat();
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
                if (stashSlots[i].item.data == null)
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
        //装备不叠加
        InventoryItem newItem = new InventoryItem(_item);
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].item.data == null)
            {
                inventorySlots[i].item = newItem;
                break;
            }
        }
        
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
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

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<int, string> pair in _data.inventory)
        {
            foreach (var item in GetItemDataBase())
            { 
                Debug.Log(item);
                Debug.Log(pair.Value);
                if (item.itemId == pair.Value)
                {
                    InventoryItem newItem = new InventoryItem(item);
                    inventorySlots[pair.Key].item = newItem;
                }
            }
        }

        foreach (KeyValuePair<string, int> pair in _data.stash)
        {
            foreach (var item in GetItemDataBase())
            {
                if (item.itemId == pair.Key)
                {
                    InventoryItem newItem = new InventoryItem(item);
                    newItem.stackSize = pair.Value;
                    int id = _data.stashPos[pair.Key];
                    stashSlots[id].item = newItem;
                    stashDictionary.Add(newItem.data, newItem);
                }
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();
        _data.stash.Clear();
        _data.stashPos.Clear();

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].item.data != null)
            {
               _data.inventory.Add(i, inventorySlots[i].item.data.itemId); 
            }
        }

        for (int i = 0; i < stashSlots.Length; i++)
        {
            if (stashSlots[i].item.data != null)
            {
               _data.stash.Add(stashSlots[i].item.data.itemId, stashSlots[i].item.stackSize);
               _data.stashPos.Add(stashSlots[i].item.data.itemId, i);
            }
        }
    }

    //返回"Assets/Data/Equipment"地址中的itemdata物品
    private List<ItemData> GetItemDataBase()
    {
        List<ItemData> itemDataBase = new List<ItemData>();
        //搜索Assets/Data/Equipment目录（包含子目录）下的所有资源，返回它们的全局唯一标识符（GUID）数组。
        string[] assetNames = AssetDatabase.FindAssets("", new[] {"Assets/Data/Items/Equipment", "Assets/Data/Items/Materials"});
        //todo:不知道为啥又突然不可以搜索子目录了
        //string[] assetNames = AssetDatabase.FindAssets("", new[] {"Assets/Data/Items"});
        
        foreach (string SOName in assetNames)
        {
            //将资源的GUID转换为项目相对路径（如"Assets/Data/Equipment/Sword.asset"）
            //路径字符串。若GUID无效或资源不存在，返回空字符串。
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            //根据路径加载指定类型的资源对象（此处为ItemData类型的ScriptableObject）
            var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);
            itemDataBase.Add(itemData);
        }
        
        return itemDataBase;
    }
}
