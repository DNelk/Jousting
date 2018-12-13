using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class cloudmove : MonoBehaviour
{

	private GameObject bg;
	private float bgWidth;
	private float bgX;
	
	public Vector3 increment;

	private float width;
	// Use this for initialization
	void Start ()
	{
		bg = GameObject.FindWithTag("bg");
		bgWidth = bg.GetComponent<Renderer>().bounds.size.x;
		bgX = bg.gameObject.transform.position.x - (0.5f * bgWidth);
		width = GetComponent<Renderer>().bounds.size.x;
	}
	
	// Update is called once per frame
	void Update()
	{
		transform.position += increment;
		if (transform.position.x - (0.5*width) > bgWidth + bgX)
		{
			transform.position = new Vector3(bgX - width, transform.position.y, transform.position.z);
		}
	}
}
