using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pestillence : Hitable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void hit(GameObject source, float damage = 0, float directionAngle = 0){
		print("ouch");
	}
}
