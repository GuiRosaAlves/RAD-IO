﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {

    public string agent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void OnParticleCollision(GameObject coll)
    {
        if (coll.tag == agent)
        {
            GetComponent<Animator>().SetTrigger("Activate");
            Destroy(GetComponent<Collider2D>());
            Destroy(GetComponent("Halo"));
            Destroy(transform.GetChild(1).gameObject); 
        }
    }
}
