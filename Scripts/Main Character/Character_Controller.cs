using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character_Controller : MonoBehaviour {
    private float speed = 150f;
    private int health = 3;
    private float power = 2f;
    public float powerSpeed = 5f;
    private bool equipped = false;
    private bool gameOver = false;
    private Animator anim;
    private GameObject pumpkin;
    public GameObject hand;
    public GameObject ground;
    public GameObject tutorial;
    public GameObject hit;

    private Vector3 velocity = new Vector3(0f, 0f, 0f);
    private Vector3 gravity = new Vector3(0f, -9.81f, 0f);

    private Vector3 pumpkinLocation = new Vector3(-0.008f, 0.0169f, 0.0003f);
    private Vector3 pumpkinRotation = new Vector3(130.415f, 61.44499f, 78.281f);

    LineRenderer lineRenderer;

    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("FirstPlay") == 0)
        {
            PlayerPrefs.SetInt("FirstPlay", 1);
            Instantiate(tutorial);
        }
        anim = GetComponent<Animator>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            if (Input.GetKey(KeyCode.Space) && equipped)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Rotate(-Vector3.up * (speed - 40) * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Rotate(Vector3.up * (speed - 40) * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {

                }

                lineRenderer.enabled = true;
                power += Time.deltaTime * powerSpeed;
                velocity = new Vector3(power * transform.forward.x, power * transform.up.y, power * transform.forward.z);
                anim.SetBool("Moving", false);
                anim.SetBool("Prepare", true);
                DrawTrajectory(pumpkin.transform.position, velocity);
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Rotate(-Vector3.up * speed * Time.deltaTime);
                    anim.SetBool("Moving", true);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Rotate(Vector3.up * speed * Time.deltaTime);
                    anim.SetBool("Moving", true);
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    anim.SetBool("Moving", true);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    anim.SetBool("Moving", true);
                }
                else
                {
                    anim.SetBool("Moving", false);
                }

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    anim.SetBool("Prepare", false);
                    anim.SetLayerWeight(1, 0);
                    anim.SetTrigger("Throw");
                    pumpkin.tag = "PlayerAirborn";
                    pumpkin.transform.parent = ground.gameObject.transform;
                    pumpkin.gameObject.AddComponent<Rigidbody>();
                    pumpkin.gameObject.GetComponent<Rigidbody>().velocity = velocity;
                    pumpkin.GetComponent<Pumpkin>().ThrowPumpkin();
                    lineRenderer.enabled = false;
                    power = 2;
                    equipped = false;
                }
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void DrawTrajectory(Vector3 initialPosition, Vector3 initialVelocity)
    {
        int steps = 100; 
        float timer = 1.0f / initialVelocity.magnitude;

        lineRenderer.positionCount = steps;

        Vector3 position = initialPosition;
        Vector3 velocity = initialVelocity;
        for (int i = 0; i < steps; ++i)
        {
            lineRenderer.SetPosition(i, position);

            position += velocity * timer + 0.5f * gravity * timer * timer;
            velocity += gravity * timer;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Pumpkin" && equipped == false)
        {
            pumpkin = collision.gameObject;
            pumpkin.GetComponent<Pumpkin>().Picked();
            Destroy(pumpkin.GetComponent<Rigidbody>());
            equipped = true;
            anim.SetLayerWeight(1, 1);
            collision.gameObject.transform.parent = hand.gameObject.transform;
            collision.gameObject.transform.localPosition = pumpkinLocation;
            collision.gameObject.transform.localRotation = Quaternion.Euler(pumpkinRotation);
        }
        if(collision.gameObject.tag == "EnemyAirborn")
        {
            PlayHit();
            collision.gameObject.tag = "EnemyProjectile";
            health -= 1;
        }
    }

    public void PlayHit()
    {
        Instantiate(hit);
    }

    public int GetHealth()
    {
        return health;
    }

    public void GameOver()
    {
        gameOver = true;
    }
}
