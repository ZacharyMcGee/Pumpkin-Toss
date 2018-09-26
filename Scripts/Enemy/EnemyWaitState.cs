using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyWaitState : IEnemyState
{
    private readonly StatePatternEnemy enemy;
    private int nextWayPoint;

    public float wanderRadius = 1f;


    public EnemyWaitState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Wait();
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
        enemy.navMeshAgent.isStopped = false;
        enemy.anim.SetBool("Moving", true);
        enemy.currentState = enemy.walkState;
    }

    public void ToAttackState()
    {
        enemy.currentState = enemy.attackState;
    }

    public void Wait()
    {
        float rnd = Random.Range(.1f, .3f);
        if(enemy.pumpkin.transform.localScale.x >= rnd && enemy.equipped == false)
        {
            enemy.equipped = true;
            UnityEngine.Object.Destroy(enemy.pumpkin.GetComponent<Rigidbody>());
            enemy.pumpkin.GetComponent<Pumpkin>().Picked();
            enemy.anim.SetLayerWeight(1, 1);
            enemy.pumpkin.gameObject.transform.parent = enemy.hand.gameObject.transform;
            enemy.pumpkin.gameObject.transform.localPosition = enemy.pumpkinLocation;
            enemy.pumpkin.gameObject.transform.localRotation = Quaternion.Euler(enemy.pumpkinRotation);
        }
        if(enemy.equipped == true)
        {
            Vector3 pos = RandomNavSphere(enemy.player.transform.position, 15f, -1);
            enemy.navMeshAgent.SetDestination(pos);
            ToWalkState();
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}