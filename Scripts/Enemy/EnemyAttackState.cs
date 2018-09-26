using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private readonly StatePatternEnemy enemy;
    private int nextWayPoint;
    private float pause = 1f;

    public EnemyAttackState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        enemy.timer -= Time.deltaTime;
        Vector3 relativePos = enemy.player.transform.position - enemy.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        enemy.transform.rotation = rotation;
        Attack();
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

    public void Attack()
    {
        if (enemy.equipped == true && enemy.timer <= 0f)
        {
            enemy.anim.SetBool("Prepare", false);
            enemy.anim.SetLayerWeight(1, 0);
            enemy.anim.SetTrigger("Throw");
            enemy.pumpkin.tag = "Untagged";
            enemy.pumpkin.transform.parent = enemy.player.GetComponent<Character_Controller>().ground.gameObject.transform;
            float rnd = Random.Range(.5f, 3f);
            Vector3 velocity = CalculateTrajectoryVelocity(enemy.transform.position, enemy.player.transform.position, rnd);
            enemy.pumpkin.AddComponent<Rigidbody>();
            enemy.pumpkin.GetComponent<Rigidbody>().velocity = velocity;
            enemy.pumpkin.GetComponent<Pumpkin>().ThrowPumpkin();
            enemy.pumpkin.tag = "EnemyAirborn";
            enemy.pumpkin = null;
            enemy.equipped = false;
            pause = Random.Range(0f, 1f);
        }

        if(enemy.equipped == false && pause > 0f)
        {
            pause -= Time.deltaTime;
        }
        else if(enemy.equipped == false && pause <= 0f)
        {
            ToSearchState();
        }
    }

    Vector3 CalculateTrajectoryVelocity(Vector3 origin, Vector3 target, float t)
    {
        float vx = (target.x - origin.x) / t;
        float vz = (target.z - origin.z) / t;
        float vy = ((target.y - origin.y) - 0.45f * Physics.gravity.y * t * t) / t;
        return new Vector3(vx, vy, vz);
    }

}
