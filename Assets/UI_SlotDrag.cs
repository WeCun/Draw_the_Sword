using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SlotDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform orinalParent;
    private UI_ItemSlot itemSlot;
    
    //开始拖拽的时候
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemSlot = GetComponentInParent<UI_ItemSlot>();
        orinalParent = transform.parent;
        //拖拽开始时，该 ItemSlot 的 RectTransform 位置被修改（脱离原布局位置），
        //布局组件会立即重新计算剩余子项的排列顺序，导致其他 ItemSlot 突然移动，而拖拽对象的位置可能被错误地重置到原容器末尾的「下一个位置」。
        //设置为transform.root就可以避免了
        transform.SetParent(transform.root);
        transform.position = eventData.position;
        //防止被其他单元格给遮挡住，渲染顺序是越往下会先被渲染
        
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //假如这个位置有物品就进行两者之间的交换
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.name == "ItemIcon")
        {
            //进行优化，交换的只是两个slot之间的物品，而不是互相切换父母
            itemSlot.SwapItem(eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<UI_ItemSlot>());
            transform.SetParent(orinalParent);
            transform.position = orinalParent.position;
            
            // itemSlot.SwapBasicItem(eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<UI_ItemSlot>());
            // transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
            // transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.position;
            // eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(orinalParent);
            // eventData.pointerCurrentRaycast.gameObject.transform.parent.position = orinalParent.position; 
        }//再判断是否在格子上
        else if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UI_ItemSlot>() != null)
        {
            itemSlot.SwapItem(eventData.pointerCurrentRaycast.gameObject.GetComponent<UI_ItemSlot>());
            transform.SetParent(orinalParent);
            transform.position = orinalParent.position;
            
            // transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            // transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            // Debug.Log(eventData.pointerCurrentRaycast.gameObject.GetComponentInChildren<UI_SlotDrag>().gameObject.name);
            // eventData.pointerCurrentRaycast.gameObject.GetComponentInChildren<UI_SlotDrag>().gameObject.transform.SetParent(orinalParent);
            //eventData.pointerCurrentRaycast.gameObject.GetComponentInChildren<UI_SlotDrag>().gameObject.transform.position = orinalParent.position;
        }
        else
        {
            transform.SetParent(orinalParent);
            transform.position = orinalParent.position;
        }
        
        Inventory.instance.UpdateSlot();
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
