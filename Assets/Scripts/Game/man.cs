using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class man : MonoBehaviour {
	public float speed = 6.0F;
	public float gravity = 150.0F;

	private Vector3 move;

	private Transform sph;

	private GameObject sphObject;
	private Rigidbody sphRigidbody;
	// Use this for initialization
	void Start () {
		
	}

	void Awake()
	{
		sphObject = GameObject.FindWithTag("bullet");
		sph = sphObject.transform;
		sphRigidbody = sphObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		var x = Input.GetAxis("Horizontal");
		var z = Input.GetAxis("Vertical");

		CharacterController player = GetComponent<CharacterController>();
		move = new Vector3(x, 0, z);
		move = transform.TransformDirection(move);
		move *= speed;

		move.y -= gravity * Time.deltaTime ;
		player.Move(move * Time.deltaTime);


		if (Input.GetKey(KeyCode.Q))
		{
			sph.position = transform.TransformPoint(0, 1, 2);
			sph.parent = transform;
			sphRigidbody.isKinematic = true;
		}

		if (Input.GetKey(KeyCode.E))
		{
			if (sph.parent == transform)
			{
				transform.DetachChildren();
				Vector3 v = transform.TransformDirection(0,0,10);

				sphRigidbody.isKinematic = false;
				sphRigidbody.AddForce(v,ForceMode.Impulse);
			}
		}

	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag == "monster")
		{
			SceneManager.LoadScene(0);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "end")
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}
	}
}
