using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class jump : MonoBehaviour {

	private int jumpCount = 0;
	private float speed = 90f;

	private const int jumpMax = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Click()
	{
		Transform player = GameObject.FindWithTag("player").transform;
		Vector3 jump = new Vector3(0, 0, 0);
		CharacterController cha = player.GetComponent<CharacterController>();
		if (cha.isGrounded)
		{
			jumpCount = 0;
		}

		if (jumpCount < jumpMax)
		{
			jump.y = speed;
			jumpCount++;
		}

		cha.Move(jump * Time.deltaTime);
	}
}
