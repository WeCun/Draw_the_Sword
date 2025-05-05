using System.Collections.Generic;
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
        bool isEquipmentSlot = false,  isItemSlot = false;
        List<RaycastResult> results = new List<RaycastResult>();
        //获取当前鼠标所在位置的所有UI元素
        EventSystem.current.RaycastAll(eventData, results);
        
        //判断是否在物品上
        foreach (var result in results)
        {
            
                
        }
        
        //假如这个位置有物品就进行两者之间的交换
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.name == "ItemIcon")
        {
            //进行优化，交换的只是两个slot之间的物品，而不是互相切换父母
            itemSlot.SwapItem(eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<UI_ItemSlot>());
            transform.SetParent(orinalParent);
            transform.position = orinalParent.position;
        }//再判断是否在格子上
        else if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UI_ItemSlot>() != null)
        {
            itemSlot.SwapItem(eventData.pointerCurrentRaycast.gameObject.GetComponent<UI_ItemSlot>());
            transform.SetParent(orinalParent);
            transform.position = orinalParent.position;
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
