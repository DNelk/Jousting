using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour
{

	private Manager manager;

	private bool collided;
	// Use this for initialization
	void Start () {
		manager = (Manager) GameObject.Find("manager").GetComponent(typeof(Manager));
		collided = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("lance")||other.gameObject.CompareTag("horse"))
		{
			if (!collided)
			{
				manager.AdvanceState(true);
				collided = true;
			}
		}
	}
}
