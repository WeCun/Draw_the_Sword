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
        //会立即停止当前脚本中所有通过Invoke或InvokeRepeating启动的方法
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
            sr.color = new Color(0, 0, 0, 1f);
        }
    }
}
