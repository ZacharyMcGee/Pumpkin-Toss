using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {
    float life = 3f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        life -= Time.deltaTime;
		if(life <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
