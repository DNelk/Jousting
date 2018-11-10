using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringObject : MonoBehaviour
{

	private Rigidbody rb; //Our Rigidbody

	private float startX; // Where our object starts before dragging

	public float k; //Spring constant

	private bool mouseEnabled; //If our object is already launched, we don't want to check anymore
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>(); //Assign Body
		mouseEnabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
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
			Vector3 mousePosRel = new Vector3(ray.GetPoint(transform.position.x).x, transform.position.y, transform.position.z); //New position based on local positioning
			transform.position = mousePosRel; //Move to mouse
		}

		
		
	}
	//When we release the mouse, we calculate spring force and launch our object! But let's put that in a different method
	void OnMouseUp()
	{
		if (mouseEnabled)
		{
			var mainCamera = FindCamera(); //Get our camera so we can get our relative position
			var ray = mainCamera.ScreenPointToRay(Input.mousePosition);	//Shoot a laser at the object relative to the camera
			Vector3 mousePosRel = new Vector3(ray.GetPoint(transform.position.x).x, transform.position.y, transform.position.z); //New position based on local positioning
			SpringRelease(mousePosRel.x);
			//mouseEnabled = false; //Disable the mouse
		}
				
	}
	
	//Release our spring and launch our object!
	private void SpringRelease(float mouseX)
	{
		float x = startX - mouseX; //Calculate our distance
		float f = x * k; //Force equals negative distance multiplied by spring force. Hope you paid attention in physics!
		rb.AddForce(new Vector3(f,0.0f,0.0f)); //Apply our force
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
