using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour
{
    [SerializeField] protected Image image;
    [SerializeField] protected TextMeshProUGUI text;

    public InventoryItem item;
    
    public void UpdateSlot(InventoryItem _item)
    {
        item = _item;
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
}
