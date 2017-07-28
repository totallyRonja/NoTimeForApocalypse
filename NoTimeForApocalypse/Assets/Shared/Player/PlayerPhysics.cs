using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerPhysics : MonoBehaviour {

    public static PlayerPhysics current;
    public Vector2 velocityGoal;
	public float acceleration;
	public Vector2 velocity{
        get { return rigid.velocity; }
        set { rigid.velocity = value; }
    }
    public Collider2D land;

	private Rigidbody2D rigid;
    private PlayerHP health;
    private Vector3 safePoint;

    void Awake(){
        current = this;
    }

    // Use this for initialization
    void Start () {
		rigid = GetComponent<Rigidbody2D>();
        health = GetComponent<PlayerHP>();
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
		if (!land.OverlapPoint(transform.position) && (health?health.hp>=0:true)) {
            if (!Input.GetButton("God")){
                if (health) health.Hit(gameObject, 1);
                rigid.velocity = rigid.velocity.normalized * -30;
                transform.position = safePoint;
                print(rigid.velocity);
            }
        } else {
            safePoint = transform.position;
        }
	}
}
