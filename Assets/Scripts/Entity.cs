using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDis;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDis;
    
    public int facingDir = 1;
    protected bool facingRight = true;
    
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }
    
    public bool GroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDis, whatIsGround);
    public bool WallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDis, whatIsGround);
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDis));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDis * facingDir, wallCheck.position.y));
    }
    
    
    public void SetZeroVelocity()
    {
        rb.velocity = Vector2.zero;
    }
    
    public void SetVelocity(float _x, float _y)
    {
        rb.velocity = new Vector2(_x, _y);
        if(_x > 0 && !facingRight)
            Flip();
        if(_x < 0 && facingRight)
            Flip();
    }

    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
}
