using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	public Sprite brokenWall;
	private Sprite defaultWall;
	public string breakerTag;
	private bool broken = false;

	// Use this for initialization
	void Start () {
		defaultWall = GetComponentInChildren<SpriteRenderer>().sprite;
	}
	
	// Update is called once per frame
	void Update () {
		if(!broken && ExampleVariableStorage.current.IsTag(breakerTag)){
			GetComponentInChildren<SpriteRenderer>().sprite = brokenWall;
			foreach(Collider2D coll in GetComponents<Collider2D>()){
				coll.enabled = !coll.enabled;
			}
			broken = true;
		}
	}

	public void Reset(){
		if(broken && GetComponentInChildren<SpriteRenderer>().sprite == brokenWall){
			GetComponentInChildren<SpriteRenderer>().sprite = defaultWall;
			foreach(Collider2D coll in GetComponents<Collider2D>()){
				coll.enabled = !coll.enabled;
			}
		}
	}
}
