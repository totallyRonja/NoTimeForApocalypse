using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float walkSpeed = 1;
	public GameObject hitPrefab;
	public GameObject hitOrigin;
    [NonSerialized] public bool slowed = false;

	Rigidbody2D rigid;
	SpriteRenderer sprite;

	float direction = 0;
	float hitCooldown = 0;

	Animator anim;

	// Use this for initialization
	void Awake () {
		rigid = GetComponent<Rigidbody2D> ();
		sprite = transform.GetComponentInChildren<SpriteRenderer>();
		anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Movement();
		Punch();
		Anim();
	}

    private void FixedUpdate(){
        slowed = false;
    }

    private void Movement() {
		Vector2 velocity = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")*0.666f) * walkSpeed;
		rigid.velocity = slowed?velocity*0.25f:velocity;

		if (velocity != Vector2.zero) {
			direction = Mathf.Atan2 (-velocity.x, velocity.y);
			//Debug.Log (direction);
		}
	}

	private void Punch() {
		hitCooldown -= Time.deltaTime;
		if (Input.GetButtonDown ("Fire1") && hitCooldown < 0) {
			GameObject newHit = Instantiate(hitPrefab, hitOrigin.transform.position, Quaternion.AngleAxis(direction * Mathf.Rad2Deg, Vector3.forward));
			newHit.GetComponent<HitParticle>().add_speed(rigid.velocity);
			hitCooldown = 0.4f;
		}
	}
	private void Anim(){
		anim.SetFloat("speed_x", rigid.velocity.normalized.x);
		anim.SetFloat("speed_y", rigid.velocity.normalized.y);
		sprite.flipX = rigid.velocity.x < 0;
	}
}
