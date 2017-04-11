using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Hitable {

	public float walkSpeed = 1;
	public GameObject hitPrefab;
	public GameObject hitOrigin;
    [NonSerialized] public bool slowed = false;

	Rigidbody2D rigid;
	SpriteRenderer sprite;

	float direction = 0;
	float hitCooldown = 0;

	Animator anim;

    public Text hpDisplay;
    public int maxHp = 7;
    private int hp = -1;
    public float iFrames = 1; //actually in seconds but y'know
    private float iTimer;

	// Use this for initialization
	void Awake () {
		rigid = GetComponent<Rigidbody2D> ();
		sprite = transform.GetComponentInChildren<SpriteRenderer>();
		anim = GetComponentInChildren<Animator>();

        if (hp < 0)
            hp = maxHp;

	}
	
	// Update is called once per frame
	void Update () {
		Movement();
		Punch();
		Anim();

        iTimer -= Time.deltaTime;
	}

    private void FixedUpdate(){
        slowed = false;
    }

    private void Movement() {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (input.magnitude > 1)
            input.Normalize();
        input.Scale(new Vector2(1, 2f/3f));
		Vector2 velocity = input * walkSpeed;
		rigid.velocity = slowed?velocity*0.4f:velocity;

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
            newHit.GetComponent<HitParticle>().source = gameObject;
			hitCooldown = 0.4f;
		}
	}
	private void Anim(){
		anim.SetFloat("speed_x", rigid.velocity.normalized.x);
		anim.SetFloat("speed_y", rigid.velocity.normalized.y);
		sprite.flipX = rigid.velocity.x < 0;
	}

    public override void hit(GameObject source, float damage = 0, float directionAngle = 0) {
        if (iTimer < 0) {
            iTimer = iFrames;
            hp -= (int)damage;
            hpDisplay.text = hp + "HP";
        }
    }
}
