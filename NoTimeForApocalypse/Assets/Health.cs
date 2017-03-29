using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void hurt (GameObject attacker) {
		rigid.AddForce (attacker.transform.up * 10, ForceMode2D.Impulse);
		Debug.Log ("AQAAAAAAAAAA");
	}
}
