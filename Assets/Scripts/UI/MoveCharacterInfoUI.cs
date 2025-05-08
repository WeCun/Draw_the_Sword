using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCharacterInfoUI : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("11123");
        rectTransform.anchoredPosition += eventData.delta;
    }
}
