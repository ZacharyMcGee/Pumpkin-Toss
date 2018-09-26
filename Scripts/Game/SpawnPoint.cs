using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {
    public GameObject pumpkin;
    public GameObject spawnedPumpkin;
    private float scaleSpeed = .6f;
    public float scale = .01f;
    public bool spawned = false;
    private Vector3 maxSize = new Vector3(.25f, .25f, .25f);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (spawned == false)
        {
            spawnedPumpkin = Instantiate(pumpkin, transform.position, Quaternion.identity);
            spawnedPumpkin.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            spawnedPumpkin.GetComponent<Pumpkin>().AddOwner(this.gameObject);
            spawned = true;
        }
        else if (spawnedPumpkin.transform.localScale.x < maxSize.x)
        {
            spawnedPumpkin.transform.localScale = new Vector3(spawnedPumpkin.transform.localScale.x + spawnedPumpkin.transform.localScale.x * scaleSpeed * Time.deltaTime, spawnedPumpkin.transform.localScale.y + spawnedPumpkin.transform.localScale.y * scaleSpeed * Time.deltaTime, spawnedPumpkin.transform.localScale.z + spawnedPumpkin.transform.localScale.z * scaleSpeed * Time.deltaTime);
        }
    }

    public void Picked()
    {
        spawnedPumpkin.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        spawned = false;
        spawnedPumpkin = null;
    }
}
