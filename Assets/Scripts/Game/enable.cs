using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enable : MonoBehaviour
{

	public MeshRenderer mMesh;
	// Use this for initialization
	void Start ()
	{
		mMesh.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space))
		{
			mMesh.enabled = !mMesh.enabled;
		}
	}
}
