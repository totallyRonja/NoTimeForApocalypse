using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float walkSpeed = 1;

	Rigidbody2D rigid;

	// Use this for initialization
	void Awake () {
		rigid = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Movement();
	}

	private void Movement() {
		Vector2 velocity = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * walkSpeed;
		rigid.velocity = velocity;
	}
}
