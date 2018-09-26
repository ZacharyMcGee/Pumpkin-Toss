using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyWalkState : IEnemyState
{
    private readonly StatePatternEnemy enemy;
    private int nextWayPoint;

    public float wanderRadius = 16f;


    public EnemyWalkState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        enemy.timer -= Time.deltaTime;
        Walk();
    }

    public void ToSearchState()
    {
        enemy.navMeshAgent.isStopped = true;
        enemy.anim.SetBool("Moving", false);
        enemy.currentState = enemy.searchState;
    }


    public void ToWalkState()
    {
        enemy.navMeshAgent.isStopped = false;
        enemy.anim.SetBool("Moving", true);
        enemy.currentState = enemy.walkState;
    }

    public void ToWaitState()
    {
        enemy.navMeshAgent.isStopped = true;
        enemy.anim.SetBool("Moving", false);
        enemy.currentState = enemy.waitState;
    }


    public void ToAttackState()
    {
        enemy.navMeshAgent.isStopped = true;
        enemy.anim.SetBool("Moving", false);
        enemy.currentState = enemy.attackState;
    }

    public void Walk()
    {
        if (enemy.navMeshAgent.remainingDistance <= 2f || enemy.timer <= 0f)
        {
            if (enemy.equipped)
            {
                float rnd = Random.Range(.1f, 2f);
                enemy.timer = rnd;
                ToAttackState();
            }
            else
            {
                ToWaitState();
            }
        }
    }


}