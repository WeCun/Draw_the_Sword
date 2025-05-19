using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    protected SpriteRenderer sr;
    protected Color initialColor;
    
    public virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        initialColor = sr.color;
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
        CancelInvoke("InvincibleBlink");
        sr.color = Color.white;
    }

    public void InvincibleBlink()
    {
        if (sr.color != initialColor)
        {
            sr.color = initialColor;
        }
        else
        {
            sr.color = new Color(1, 1, 1, 0.3f);
        }
    }
}
