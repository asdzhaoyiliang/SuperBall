using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class rand_move : MonoBehaviour
{

	private float speed = 0.1f;
	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(Vector3.forward * speed);
	}

	private void OnCollisionEnter(Collision other)
	{
		Random random = new Random();
		float dir = random.Next(0, 360);
		transform.rotation = Quaternion.Euler(0, dir, 0);
	}

}
