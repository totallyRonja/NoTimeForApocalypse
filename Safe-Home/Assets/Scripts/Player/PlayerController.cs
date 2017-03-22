using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 10;

    private Rigidbody2D rigid;

    private 

    void Awake () {
        rigid = GetComponent<Rigidbody2D> ();
    }

	// Use this for initialization
	void Start () {
		
	}

	// PhysicsTick
	void FixedUpdate () {
        Vector2 velocity = rigid.velocity;
        velocity.x = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * speed;
        rigid.velocity = velocity;


	}
}
