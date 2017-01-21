﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shot : MonoBehaviour {

    public float speed;
    public float damage;

	// Use this for initialization
	protected virtual void Start ()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
}