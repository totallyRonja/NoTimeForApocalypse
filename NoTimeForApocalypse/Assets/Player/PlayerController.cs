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

    public Collider2D land;

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

        if (!land.OverlapPoint(transform.position)) {
            Die();
        }
	}

	private void Punch() {
		hitCooldown -= Time.deltaTime;
		if (Input.GetButtonDown ("Fire1") && hitCooldown < 0) {
            Vector2 direction = getClosestWithTag("Enemy").transform.position - transform.position;
            float angle = getAngle(direction);
			GameObject newHit = Instantiate(hitPrefab, hitOrigin.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
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
        if (hp < 0)
            Die();
    }

    public GameObject getClosestWithTag(string tag) {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float distance = -1;
        foreach(GameObject go in objectsWithTag) {
            if(closest == null || (go.transform.position - transform.position).magnitude < distance) {
                closest = go;
                distance = (go.transform.position - transform.position).magnitude;
            }
        }
        return closest;
    }

    float getAngle(Vector2 fromVector2) {
        Vector2 toVector2 = new Vector2(0, 1);

        float ang = Vector2.Angle(fromVector2, toVector2);
        Vector3 cross = Vector3.Cross(fromVector2, toVector2);

        if (cross.z > 0)
            ang = 360 - ang;

        return ang;
    }

    public void Die() {
        print("played died");
        hpDisplay.text = "u ded lol";
        Time.timeScale = 0.0f;
    }
}
