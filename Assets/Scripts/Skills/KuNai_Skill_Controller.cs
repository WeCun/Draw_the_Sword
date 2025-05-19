using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KuNai_Skill_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private int damage;
    private Vector2 knockback;
    private float knockbackDuration;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DestorySelf());
    }

    public void SetKuNai(float _kunaiGrivaty, Vector2 _finnalDir, int _damage, Vector2 _knockback, float _knockbackDuration)
    {
        rb.gravityScale = _kunaiGrivaty;      
        rb.velocity = _finnalDir;
        damage = _damage;
        knockback = _knockback;
        knockbackDuration = _knockbackDuration;
    }

    private void Update()
    {
        transform.right = rb.velocity;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Enemy>() != null)
        {
            CharacterStats target = collider.GetComponent<CharacterStats>();
            if (target != null)
            {
                PlayerManager.instance.player.stats.DoDamage(target, 1, knockback, knockbackDuration);
            }
        }
        Destroy(gameObject);
    }

    IEnumerator DestorySelf()
    {
        yield return new WaitForSeconds(20.0f);
        Destroy(gameObject);
    }
}
