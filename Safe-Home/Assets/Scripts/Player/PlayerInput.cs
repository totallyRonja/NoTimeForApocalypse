using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	public float walkSpeed = 10;
	public float jumpSpeed = 100;

	private PlayerController controller;

	// Use this for initialization
	void Awake () {
		controller = GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		controller.SetVertSpeed(Input.GetAxis("Horizontal") * walkSpeed);

		if (Input.GetButtonDown ("Jump") && controller.IsGrounded()) {
			controller.SetHorizSpeed(jumpSpeed);
		}
	}
}
