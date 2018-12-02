using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpringObject : MonoBehaviour
{

	private Rigidbody2D rb; //Our Rigidbody

	private float startX; // Where our object starts before dragging

	public float k; //Spring constant

	public float forceMax;
	public bool clampForce;
	
	private bool mouseEnabled; //If our object is already launched, we don't want to check anymore
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>(); //Assign Body
		mouseEnabled = true;
	}
	
	// Update is called once per frame
	private void FixedUpdate()
	{
		rb.MoveRotation(90f);
	}

	//When we click on our object, save our starting x position
	void OnMouseDown() 
	{
		if(mouseEnabled)
			startX = transform.position.x; //Save our initial position
	}
	
	//When we drag our mouse, move the object to the mouse
	void OnMouseDrag() 
	{
		if (mouseEnabled)
		{
			var mainCamera = FindCamera(); //Get our camera so we can get our relative position
			var ray = mainCamera.ScreenPointToRay(Input.mousePosition);	//Shoot a laser at the object relative to the camera
			Vector2 mousePosRel = new Vector2(ray.GetPoint(rb.position.x).x, rb.position.y); //New position based on local positioning
			rb.MovePosition(mousePosRel);
		}

		
		
	}
	//When we release the mouse, we calculate spring force and launch our object! But let's put that in a different method
	void OnMouseUp()
	{
		if (mouseEnabled)
		{
			var mainCamera = FindCamera(); //Get our camera so we can get our relative position
			var ray = mainCamera.ScreenPointToRay(Input.mousePosition);	//Shoot a laser at the object relative to the camera
			Vector2 mousePosRel = new Vector2(ray.GetPoint(transform.position.x).x, transform.position.y); //New position based on local positioning
			SpringRelease(mousePosRel.x);
			//mouseEnabled = false; //Disable the mouse
		}
				
	}
	
	//Release our spring and launch our object!
	private void SpringRelease(float mouseX)
	{
		float x = startX - mouseX; //Calculate our distance
		float f = x * k * rb.mass; //Force equals negative distance multiplied by spring constant. Hope you paid attention in physics!
		Debug.Log(f);
		if(clampForce)
			f = Mathf.Clamp(f, 0f, forceMax);
		rb.AddForce(new Vector2(f,0.0f)); //Apply our force
		Debug.Log(f);
	}
		
	//Gets the camera. I stole this from standardassets
	private Camera FindCamera()
	{
		if (GetComponent<Camera>())
		{
			return GetComponent<Camera>();
		}

		return Camera.main;
	}
}
