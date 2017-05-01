using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnim : MonoBehaviour {

    Animation a;

	// Use this for initialization
	void Start () {
        a = GetComponent<Animation>();
        a.Play("aargh");
        print(a.isPlaying);
	}
	
	// Update is called once per frame
	void Update () {
        print(a.isPlaying);
    }
}
