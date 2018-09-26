using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {
    public GameObject Player;
    public GameObject Enemy;
    public GameObject mainCamera;
    public GameObject pressSpace;
    public Slider healthbar;
    private int playerHealth = 3;
    private int enemyHealth = 3;
    private bool ended = false;
    // Use this for initialization
    void Start () {
        healthbar = this.GetComponentInChildren<Slider>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    private void LateUpdate()
    {
        if (!ended)
        {
            if (this.gameObject.name == "EnemyHealthbar")
            {
                transform.position = new Vector3(Enemy.transform.position.x + 3f, Enemy.transform.position.y + 3f, Enemy.transform.position.z - 2f);
                enemyHealth = Enemy.GetComponent<StatePatternEnemy>().GetHealth();
                if (enemyHealth <= 0)
                {
                    ended = true;
                    mainCamera.GetComponent<Animator>().SetBool("Win", true);
                    Instantiate(pressSpace);
                    Player.GetComponent<Character_Controller>().GameOver();
                }
                healthbar.value = enemyHealth;

            }
            else
            {
                transform.position = new Vector3(Player.transform.position.x + 3f, Player.transform.position.y + 3f, Player.transform.position.z - 2f);
                playerHealth = Player.GetComponent<Character_Controller>().GetHealth();
                if (playerHealth <= 0)
                {
                    ended = true;
                    mainCamera.GetComponent<Animator>().SetBool("End", true);
                    Instantiate(pressSpace);
                    Player.GetComponent<Character_Controller>().GameOver();
                }
                healthbar.value = playerHealth;
            }
        }
    }
}
