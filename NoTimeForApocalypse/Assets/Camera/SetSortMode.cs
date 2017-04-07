using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSortMode : MonoBehaviour {

	public TransparencySortMode mode;
	public Vector3 axis = Vector3.up;

	// Use this for initialization
	void Awake () {
		Camera cam = GetComponent<Camera>();

		cam.transparencySortMode = mode;
		cam.transparencySortAxis = axis;
	}
}
