using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour {


	public float initLifetime = 1;
	public float speed = 5;

	private float lifetime;
	LinkedList<Health> punched = new LinkedList<Health>();

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
		Health hp = coll.GetComponent<Health>();
		if (hp == null || punched.Contains(hp))
			return;
		punched.AddLast (hp);
		hp.hurt (gameObject);
	}
}
