using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
    float life = 5f;
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
