using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleVault : MonoBehaviour {

	private Rigidbody2D rb; //Our Rigidbody

	private BoxCollider2D collider;
	
	public Rigidbody2D charRb; //Character rigidbody

	public float forceMod;
	
     //// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>(); //Assign Body
		collider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("wall")) //Did we touch wall
		{
			Debug.Log("hit");
			float f = GetKineticEnergy();
			charRb.AddForce(new Vector2(0f, f * forceMod));
			StartCoroutine(Bend());
		}
	}

	float GetKineticEnergy()
	{
		return 0.5f * Mathf.Pow(charRb.velocity.magnitude, 2f) * charRb.mass;
	}

	IEnumerator Bend()
	{
		rb.isKinematic = true;
		
		yield return new WaitForSeconds(0.2f);

		rb.isKinematic = false;
		//collider.enabled = false;
	}
}
