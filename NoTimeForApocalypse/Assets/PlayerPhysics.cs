using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour {

	public Vector2 velocityGoal;
	public float acceleration;
	public Vector2 velocity{
        get { return rigid.velocity; }
        set { rigid.velocity = value; }
    }

	private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(velocity != velocityGoal){
			Vector2 difference = velocityGoal - velocity;
			if(difference.magnitude < acceleration * Time.deltaTime){
				velocity = velocityGoal;
			} else {
				difference.Normalize();
				velocity += difference * acceleration * Time.deltaTime;
			}
			rigid.velocity = velocity;
		}
	}
}
