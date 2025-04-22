using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    protected SpriteRenderer sr;

    public virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    public void RedColorBlink()
    {
        if (sr.color == Color.red)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }

    public void CancelColorChange()
    {
        CancelInvoke("RedColorBlink");
        sr.color = Color.white;
    }
}
