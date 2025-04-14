using System;

public class EnemyState
{
    protected Enemy enemyBase;
    protected EnemyStateMachine stateMachine;    
    private string anim;

    public EnemyState(Enemy _enemybase, EnemyStateMachine _stateMachine, String _anim)
    {
        this.enemyBase = _enemybase;
        this.stateMachine = _stateMachine;
        this.anim = _anim;
    }
    
    public virtual void Start()
    {
        enemyBase.anim.SetBool(anim, true);
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(anim, false);
    }

    public virtual void Update()
    {
        
    }
}
