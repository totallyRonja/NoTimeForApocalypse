using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour {


	public float initLifetime = 1;
	public float speed = 5;

	private float lifetime;

	// Use this for initialization
	void Start () {
		lifetime = initLifetime;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.up * Time.deltaTime * speed;

		transform.localScale = Vector3.one * (1.5f - 1.0f * (lifetime/initLifetime));
		lifetime -= Time.deltaTime;
		if (lifetime <= 0) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		//TODO punch stuff
	}
}
