using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState;

    public void Initialize(EnemyState _startState)
    {
        currentState = _startState;
        currentState.Start();
    }

    public void ChangeState(EnemyState _nowstate)
    {
        currentState.Exit();
        currentState = _nowstate;
        currentState.Start();
    }
}
