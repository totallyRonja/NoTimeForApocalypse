using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War : Hitable {
    public float speed = 10;
    public float acceleration = 20;
    public Collider2D area;
    private GameObject player;
    private Rigidbody2D rigid;
    private Vector2 velocityGoal;
	
    public override void Hit(GameObject source, float damage = 0, float directionAngle = 0){
        //FUCK THIS
        /*print(directionAngle);
        rigid.velocity = new Vector2(Mathf.Sin(directionAngle) * speed, Mathf.Cos(directionAngle) * speed);
		Debug.DrawRay(rigid.position, rigid.velocity, Color.blue, 1);*/

        //Vector2 direction = transform.position - source.transform.position;
        //direction.Normalize();
        rigid.velocity = Vector2.zero;
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		if(area.OverlapPoint(player.transform.position)){
            velocityGoal = (player.transform.position - transform.position).normalized * speed;
		} else {
            Vector3 difference = area.transform.position - transform.position;
            velocityGoal = difference.normalized * speed * Mathf.Min(difference.magnitude, 1);
        }
	}

	void FixedUpdate(){
		Vector2 difference = velocityGoal - rigid.velocity;
        rigid.velocity += difference.normalized * acceleration * Time.fixedDeltaTime * Mathf.Min(difference.magnitude, 1);
    }
}
