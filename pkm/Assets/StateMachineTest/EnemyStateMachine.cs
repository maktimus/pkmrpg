using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentEnemyState { get; set; }

    public void Initialise(EnemyState startingState)
    {
        Debug.Log("Enemy State Machine Initialised");
        currentEnemyState = startingState;
        currentEnemyState.EnterState();
    }

    public void ChangeState(EnemyState newState)
    {
        Debug.Log("Enemy State Changed");
        currentEnemyState.ExitState();
        currentEnemyState = newState;
        currentEnemyState.EnterState();
    }
}
