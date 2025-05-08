using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    private RectTransform rectTransform;
    
    public Vector3 offset;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void UpdataTip(ItemData item, Transform itemPos)
    {
        nameText.text = item.name;
        descriptionText.text = item.description;
        
        gameObject.SetActive(true);
        
        //强制刷新Canvas
        Canvas.ForceUpdateCanvases();
        
        // 强制重建布局
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        
        float height = rectTransform.rect.height;
        float width = rectTransform.rect.width;
        var finnalPos = itemPos.position;
        if (itemPos.position.x + offset.x + width > Screen.width - Screen.width / 10)
            finnalPos.x -= offset.x + width;
        else
            finnalPos.x += offset.x;

        finnalPos.y += offset.y;
        if (itemPos.position.y + offset.y - height <  Screen.height / 10)
        {
            finnalPos.y += (Screen.height / 10) - (itemPos.position.y + offset.y - height);
        }
        
        transform.position = finnalPos;
    }
    

    public void CloseTip()
    {
        gameObject.SetActive(false);
    }
}
