using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnFan : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(0f,0f,0.1f);
	}
}
