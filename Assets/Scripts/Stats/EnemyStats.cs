using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private PigKing pigKing;

    public override void Start()
    {
        pigKing = GetComponent<PigKing>();
    }

    public override void Die()
    {
        pigKing.stateMachine.ChangeState(pigKing.dieState);
    }
}
