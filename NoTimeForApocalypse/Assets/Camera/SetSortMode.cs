using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SetSortMode : MonoBehaviour {

	public TransparencySortMode mode;
	public Vector3 axis = Vector3.up;

    private Camera[] cam = null;

	// Use this for initialization
	void Start () {
        Update();

        print("set mode of all cameras every frame, optimize this");
    }

    private void Update() {
        
            cam = Camera.allCameras;
            //print("camera:" + cam);
            foreach(Camera c in cam) {
                c.transparencySortMode = mode;
                c.transparencySortAxis = axis;
            }
    }
}
