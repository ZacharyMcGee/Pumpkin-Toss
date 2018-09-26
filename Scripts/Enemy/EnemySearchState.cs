using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemySearchState : IEnemyState
{
    private readonly StatePatternEnemy enemy;
    private int nextWayPoint;


    public float wanderRadius = 16f;


    public EnemySearchState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Search();
    }

    public void ToSearchState()
    {
        enemy.navMeshAgent.isStopped = true;
        enemy.anim.SetBool("Moving", false);
        enemy.currentState = enemy.searchState;
    }

    public void ToWaitState()
    {
        enemy.navMeshAgent.isStopped = true;
        enemy.anim.SetBool("Moving", false);
        enemy.currentState = enemy.waitState;
    }


    public void ToWalkState()
    {
        enemy.timer = 5f;
        enemy.navMeshAgent.isStopped = false;
        enemy.anim.SetBool("Moving", true);
        enemy.currentState = enemy.walkState;
    }

    public void ToAttackState()
    {
        enemy.currentState = enemy.attackState;
    }

    public void Search()
    {
        GameObject[] pumpkins;
        pumpkins = GameObject.FindGameObjectsWithTag("EnemyProjectile");

        int rnd = Random.Range(0, pumpkins.Length);

        if (pumpkins.Length > 0)
        {
            enemy.pumpkin = pumpkins[rnd].gameObject;
            enemy.destination = enemy.pumpkin.transform.position;
            enemy.navMeshAgent.SetDestination(enemy.destination);
            ToWalkState();
        }
    }
}