using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuNai_Skill_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetKuNai(float _kunaiGrivaty, Vector2 _finnalDir)
    {
        rb.gravityScale = _kunaiGrivaty;      
        rb.velocity = _finnalDir;
    }

    private void Update()
    {
        transform.right = rb.velocity;
    }
}
