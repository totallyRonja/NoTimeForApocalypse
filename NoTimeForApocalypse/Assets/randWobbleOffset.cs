using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randWobbleOffset : MonoBehaviour {

    public float maxOffset = Mathf.PI / 2;

	// Use this for initialization
	void Start () {
        Material wobbleMat = GetComponent<Renderer>().material;
        wobbleMat.SetFloat("_Offset", Random.Range(0, maxOffset));
	}
}
