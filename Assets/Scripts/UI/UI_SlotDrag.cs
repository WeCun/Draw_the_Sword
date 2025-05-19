using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SlotDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform orinalParent;
    private UI_ItemSlot itemSlot;
    private ItemType itemType;
    private EquipmentType equipmentType;
    
    //开始拖拽的时候
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemSlot = GetComponentInParent<UI_ItemSlot>();
        
        itemType = itemSlot.item.data.itemType;
        if(itemType == ItemType.Equipment) equipmentType = (itemSlot.item.data as ItemData_Equipment).equipmentType;
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
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //判断这个位置的物品是否在slot内
        bool isEquipmentSlot = false,  isItemSlot = false;
        UI_ItemSlot target = null;
        List<RaycastResult> results = new List<RaycastResult>();
        //获取当前鼠标所在位置的所有UI元素
        EventSystem.current.RaycastAll(eventData, results);
        
        //判断是否在物品上
        foreach (var result in results)
        {
            if (result.gameObject.GetComponent<UI_EquipmentSlot>() != null)
            {
               isEquipmentSlot = true;
               target = result.gameObject.GetComponent<UI_EquipmentSlot>();
            }
            if (result.gameObject.GetComponent<UI_ItemSlot>() != null)
            {
              isItemSlot = true;  
              target = result.gameObject.GetComponent<UI_ItemSlot>();
            }
        }
        
        //判断是否在slot上
        if (isItemSlot)
        {
            //判断这个slot是否是装备栏
            if (isEquipmentSlot)
            {
                if (itemType == ItemType.Equipment && equipmentType == (target as UI_EquipmentSlot).equipmentType)
                {
                    //先进行属性添加删除再进行物品item交换
                    if(target.item.data != null) (target.item.data as ItemData_Equipment).RemoveModifies();
                    (itemSlot.item.data as ItemData_Equipment).AddModifies();
                    itemSlot.SwapItem(target);
                    transform.SetParent(orinalParent);
                    transform.position = orinalParent.position;
                }
                //与装备栏的装备类型不匹配
                else
                {
                    transform.SetParent(orinalParent);
                    transform.position = orinalParent.position;
                }
            }
            //不是装备栏就是普通slot
            else
            {
                //判断这个slot起始是不是为装备槽
                if (itemSlot is UI_EquipmentSlot)//父类变量实际引用的子类对象: is
                {
                    if (target.item.data == null)
                    {
                        (itemSlot.item.data as ItemData_Equipment).RemoveModifies();
                        itemSlot.SwapItem(target);
                        transform.SetParent(orinalParent);
                        transform.position = orinalParent.position;
                    }
                    else if (target.item.data.itemType == ItemType.Equipment)
                    {
                        //判断要交换的两个装备是否是同一个类型
                        if ((target.item.data as ItemData_Equipment).equipmentType ==
                            (itemSlot.item.data as ItemData_Equipment).equipmentType)
                        {
                            (target.item.data as ItemData_Equipment).AddModifies();
                            (itemSlot.item.data as ItemData_Equipment).RemoveModifies();
                            itemSlot.SwapItem(target);
                            transform.SetParent(orinalParent);
                            transform.position = orinalParent.position;
                        }
                        else  //不是同一个类型就返回
                        {
                            transform.SetParent(orinalParent);
                            transform.position = orinalParent.position;
                        }
                    }
                    else  //起始是装备但终止是材料的话就不需要就换
                    {
                        transform.SetParent(orinalParent);
                        transform.position = orinalParent.position;
                    }
                }
                else
                {
                    itemSlot.SwapItem(target);
                    transform.SetParent(orinalParent);
                    transform.position = orinalParent.position;
                }
            }
        }
        //不在slot内
        else
        {
            transform.SetParent(orinalParent);
            transform.position = orinalParent.position;
        }
        
        Inventory.instance.UpdateSlot();
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
