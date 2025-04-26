using TMPro;
using UnityEngine;
using UnityEngine.EventSystems; 
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] protected Image image;
    [SerializeField] protected TextMeshProUGUI text;

    public InventoryItem item;
    
    public void UpdateSlot(InventoryItem _item)
    {
        item = _item;
        image.color = Color.white;
        if (item != null)
        {
            image.sprite = item.data.icon;
            if (_item.stackSize <= 1)
                text.text = "";
            else
            {
                text.text = _item.stackSize.ToString();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (item != null && item.data.itemType == ItemType.Equipment)
        {
            Debug.Log("Clicked on " + item.data.itemName);
            Inventory.instance.EquipItem(item.data);
        }
    }
}
