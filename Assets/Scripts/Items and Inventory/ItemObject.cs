using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    private SpriteRenderer sr;

    private void OnValidate()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = itemData.icon;
        gameObject.name = "Item Object - " + itemData.name;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>() != null)
        {
            Inventory.instance.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}
