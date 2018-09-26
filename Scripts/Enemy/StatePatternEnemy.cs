using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePatternEnemy : MonoBehaviour
{
    private int health = 3;
    public Transform eyes;
    public Quaternion targetRotation;
    public Animator anim;
    public GameObject pumpkin;
    public GameObject hand;
    public GameObject player;
    public GameObject hit;
    public ParticleSystem explode;
    public float timer;

    public bool equipped = false;

    public Vector3 destination;
    public Vector3 pumpkinLocation = new Vector3(-0.008f, 0.0169f, 0.0003f);
    public Vector3 pumpkinRotation = new Vector3(130.415f, 61.44499f, 78.281f);

    [HideInInspector] public IEnemyState currentState;
    [HideInInspector] public EnemySearchState searchState;
    [HideInInspector] public EnemyWaitState waitState;
    [HideInInspector] public EnemyWalkState walkState;
    [HideInInspector] public EnemyAttackState attackState;
    public NavMeshAgent navMeshAgent;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        searchState = new EnemySearchState(this);
        waitState = new EnemyWaitState(this);
        walkState = new EnemyWalkState(this);
        attackState = new EnemyAttackState(this);
    }
    // Use this for initialization
    void Start()
    {
        currentState = searchState;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PlayerAirborn")
        {
            Instantiate(hit);
            health -= 1;
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
