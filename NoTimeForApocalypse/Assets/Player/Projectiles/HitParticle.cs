﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour {

	public AnimationCurve opacity;
	public AnimationCurve scale;
	public float initLifetime = 1;
	public float base_speed = 5;
	private Vector2 speed = Vector2.zero;

	private float lifetime;
	List<Hitable> punched = new List<Hitable>();

	private SpriteRenderer sprite;

	// Use this for initialization
	void Awake () {
		lifetime = initLifetime;
		add_speed(transform.up * base_speed);
		sprite = GetComponent<SpriteRenderer>();
		transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += ((Vector3)speed * Time.deltaTime);
		
		transform.localScale = Vector3.one * scale.Evaluate(1 - lifetime/initLifetime);

		sprite.color = new Color(1, 1, 1, opacity.Evaluate(1 - lifetime/initLifetime));
		

		lifetime -= Time.deltaTime;
		if (lifetime <= 0) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		Hitable hp = coll.GetComponent<Hitable>();
		print("hit"+(hp==null));
		if (hp == null || punched.Contains(hp))
			return;
		punched.Add(hp);
		hp.hit(gameObject, 1, transform.rotation.z);
	}

	public void add_speed(Vector2 speed){
		this.speed += speed;
	}
}