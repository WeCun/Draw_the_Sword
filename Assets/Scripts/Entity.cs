using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public CharacterStats stats { get; private set; }
    public EntityFX fx { get; private set; }

    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDis;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDis;
    
    public int facingDir = 1;
    protected bool facingRight = true;
    public int knockbackDir = 1;
    public bool isKnocked = false;
    
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterStats>();
        fx = GetComponent<EntityFX>();    
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    public void DamageImpact()
    {
        
    }
    
    public virtual void SetKnockbackDir(Transform _damageDir)
    {
        if (_damageDir.position.x < transform.position.x)
            knockbackDir = 1;
        else if (_damageDir.position.x > transform.position.x)
            knockbackDir = -1;
    }

    public virtual IEnumerator HitKnockback(Vector2 knockbackPower, float knockbackDuration)
    {
        if(knockbackDir == facingDir) Flip();
        float knockbackTimer = 0;
        while (knockbackTimer < knockbackDuration)
        {
            rb.velocity = new Vector2(knockbackPower.x * knockbackDir, knockbackPower.y);
            knockbackTimer += Time.deltaTime;
            yield return null;
        }
        
        rb.velocity = Vector2.zero;
    }
    
    #region Collision
    
    public virtual bool GroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDis, whatIsGround);
    public virtual bool WallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDis, whatIsGround);
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDis));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDis * facingDir, wallCheck.position.y));
    }
    #endregion
    
    #region Velocity & Flip
    public void SetZeroVelocity()
    {
        rb.velocity = Vector2.zero;
    }
    
    public void SetVelocity(float _x, float _y)
    {
        rb.velocity = new Vector2(_x, _y);
        if (_x > 0 && !facingRight)
        {
            Flip();
        }

        if (_x < 0 && facingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    #endregion
}
