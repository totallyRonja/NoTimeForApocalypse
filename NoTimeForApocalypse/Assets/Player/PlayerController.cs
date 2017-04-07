using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float walkSpeed = 1;
	public GameObject hitPrefab;
	public GameObject hitOrigin;

	Rigidbody2D rigid;
	Transform visualization;

	float direction = 0;
	float hitCooldown = 0;

	// Use this for initialization
	void Awake () {
		rigid = GetComponent<Rigidbody2D> ();
		visualization = transform.FindChild ("Sprite");
	}
	
	// Update is called once per frame
	void Update () {
		Movement();
		Punch();
	}

	private void Movement() {
		Vector2 velocity = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")*0.666f) * walkSpeed;
		rigid.velocity = velocity;

		if (velocity != Vector2.zero) {
			direction = Mathf.Atan2 (-velocity.x, velocity.y);
			//Debug.Log (direction);

			if (velocity.x > 0) {
				visualization.localScale = new Vector3 (1, 1, 1);
			} else if (velocity.x < 0) {
				visualization.localScale = new Vector3 (-1, 1, 1);
			}
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
}
