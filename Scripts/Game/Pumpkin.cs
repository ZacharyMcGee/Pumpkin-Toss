using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour {
    public GameObject owner;
    public GameObject explosion;
    public bool thrown = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && thrown == true)
        {
            ContactPoint contact = collision.contacts[0];
            GameObject particles = Instantiate(explosion, new Vector3(transform.position.x, 1.3f, transform.position.z), Quaternion.identity);
            particles.transform.localScale = this.transform.localScale;
            particles.GetComponentInChildren<Rigidbody>().velocity = Vector3.Reflect(-collision.relativeVelocity, contact.normal);
            Destroy(this.gameObject);
        }
    }

    public void ThrowPumpkin()
    {
        thrown = true;
    }

    public void AddOwner(GameObject pumpkinOwner)
    {
        owner = pumpkinOwner;
    }

    public GameObject GetOwner()
    {
        return owner;
    }

    public void Picked()
    {
        owner.GetComponent<SpawnPoint>().Picked();
    }
}
