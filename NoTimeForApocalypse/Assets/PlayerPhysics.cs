using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerController))]
public class PlayerPhysics : MonoBehaviour {

	public Vector2 velocityGoal;
	public float acceleration;
	public Vector2 velocity{
        get { return rigid.velocity; }
        set { rigid.velocity = value; }
    }
    public Collider2D land;

	private Rigidbody2D rigid;
    private PlayerController controller;

    // Use this for initialization
    void Start () {
		rigid = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
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
		if (!land.OverlapPoint(transform.position) && controller.hp >= 0 && !Input.GetButton("God")) {
            controller.Hit(gameObject, 1);
            rigid.velocity = -rigid.velocity + rigid.velocity.normalized * -10;
            transform.position += (Vector3)rigid.velocity * Time.fixedDeltaTime;
        }
	}
}
