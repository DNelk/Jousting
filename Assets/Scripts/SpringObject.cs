using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class SpringObject : MonoBehaviour
{

	private Rigidbody2D rb; //Our Rigidbody

	private Camera mainCamera;
	private float startX; // Where our object starts before dragging

	public float k; //Spring constant

	public float forceMax;
	public bool clampForce;
	private float timeScale;
	private bool mouseEnabled; //If our object is already launched, we don't want to check anymore

	public ParticleSystem dust;

	private Vector3 scale;

	private Manager manager;

	private bool startTimer;
	private float failTimer;

	private AudioSource stretchSound;

	private AudioSource boingSound;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>(); //Assign Body
		mouseEnabled = true;
		timeScale = Time.timeScale;
		scale = transform.localScale;
		mainCamera = FindCamera(); //Get our camera so we can get our relative position
		manager = (Manager) GameObject.Find("manager").GetComponent(typeof(Manager));
		failTimer = 5f;
		startTimer = false;
		stretchSound = GameObject.Find("stretch").GetComponent<AudioSource>();
		boingSound = GameObject.Find("boing").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	private void FixedUpdate()
	{
		if (startTimer)
		{
			failTimer -= Time.deltaTime;
		
			//Debug.Log(failTimer);
			if (failTimer < 0 && !manager.IsComplete())
			{
				manager.AdvanceState(false);
				startTimer = false;
			}
		}
	}

	//When we click on our object, save our starting x position
	void OnMouseDown() 
	{
		if (mouseEnabled)
		{
			startX = transform.position.x; //Save our initial position
			//mainCamera.transform.LookAt(transform);
			//mainCamera.DOFieldOfView(10f, 0.5f);
			stretchSound.Play();
		}
	}
	
	//When we drag our mouse, move the object to the mouse
	void OnMouseDrag() 
	{
		if (mouseEnabled)
		{
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);	//Shoot a laser at the object relative to the camera
			Vector2 mousePosRel = new Vector2(ray.GetPoint(rb.position.x).x, rb.position.y); //New position based on local positioning
			transform.position = mousePosRel;
			float f = calcforce(startX - mousePosRel.x);
			f = Mathf.Clamp(f, 0f, forceMax);
			float stretchAmt = f / forceMax;
			float maxStretch = 2f;
			float newScaleX = stretchAmt * scale.x * maxStretch;
			newScaleX = Mathf.Clamp(newScaleX, scale.x, maxStretch * scale.x);
			transform.localScale = new Vector3(newScaleX, scale.y, scale.z);
			Time.timeScale = 0;
		}
	}
	//When we release the mouse, we calculate spring force and launch our object! But let's put that in a different method
	void OnMouseUp()
	{
		if (mouseEnabled)
		{
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);	//Shoot a laser at the object relative to the camera
			Vector2 mousePosRel = new Vector2(ray.GetPoint(transform.position.x).x, transform.position.y); //New position based on local positioning
			SpringRelease(mousePosRel.x);
			Time.timeScale = timeScale;
			transform.localScale = scale;
			dust.Play(true);
			mouseEnabled = false; //Disable the mouse
			startTimer = true;
			stretchSound.Stop();
			boingSound.Play();
		}
				
	}
	
	//Release our spring and launch our object!
	private void SpringRelease(float mouseX)
	{
		float x = startX - mouseX; //Calculate our distance
		float f = calcforce(x); //Force equals negative distance multiplied by spring constant. Hope you paid attention in physics!
//		Debug.Log(f);
		if(clampForce)
			f = Mathf.Clamp(f, 0f, forceMax);
		rb.AddForce(new Vector2(f,0.0f)); //Apply our force
//		Debug.Log(f);
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

	private float calcforce(float x)
	{
		float f = x * k * rb.mass;
		return f;
	}
}
