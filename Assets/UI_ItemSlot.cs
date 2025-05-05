using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI text;
    public GameObject itemInSlot;

    public InventoryItem item;
    
    void Start()
    {
        //todo: 把item置为null，目前我的推论：由于InventoryItem类被[Serializable]了，Unity 可能会在反序列化时自动创建一个空实例（即使 Inspector 中未赋值），导致item不为null,所以这里还需要置为null
        //todo:但你看其他类型比如上面的image，就算他是public，但输出出来的还是null，定义了[Serializable]的类就不会
        Debug.Log("Start: " + item);
        item = null;
    }
    
    public virtual void UpdateSlot()
    {
        if (item != null)
        {
            itemInSlot.SetActive(true);
            image.sprite = item.data.icon;
            if (item.stackSize <= 1)
                text.text = "";
            else
            {
                text.text = item.stackSize.ToString();
            }
        }
        else
        {
            ClearSlot();
        }
    }

    public void SwapBasicItem(UI_ItemSlot otherSlot)
    {
        Debug.Log("123");
        (image, otherSlot.image) = (otherSlot.image, image);
        (text, otherSlot.text) = (otherSlot.text, text);
        (itemInSlot, otherSlot.itemInSlot) = (otherSlot.itemInSlot, itemInSlot);
    }

    public void SwapItem(UI_ItemSlot otherSlot)
    {
        //不能直接用析构的写法，otherSlot可能为空
        InventoryItem temp = item;
        item = otherSlot.item;
        otherSlot.item = temp;
    }

    public void ClearSlot()
    {
        image.sprite = null;
        text.text = "";
        itemInSlot.SetActive(false);
    }
}
