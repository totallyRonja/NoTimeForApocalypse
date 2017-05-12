using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	public Sprite brokenWall;
	public string breakerTag;
	private bool broken = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!broken && TagTracker.tracker.isTag(breakerTag)){
			GetComponentInChildren<SpriteRenderer>().sprite = brokenWall;
			foreach(Collider2D coll in GetComponents<Collider2D>()){
				coll.enabled = !coll.enabled;
			}
			broken = true;
		}
	}
}
