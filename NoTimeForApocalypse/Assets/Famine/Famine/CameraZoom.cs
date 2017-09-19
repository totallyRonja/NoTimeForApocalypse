using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    Camera cam;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
    }
	
	void OnTriggerEnter2D(Collider2D coll){
        if (coll.CompareTag("Player"))
        {
            cam.transform.SetParent(transform);
            cam.transform.localPosition = Vector3.forward * -10;
            cam.orthographicSize = 20;
        }
    }

	void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            cam.transform.SetParent(PlayerPhysics.current.transform);
            cam.transform.localPosition = new Vector3(0, -0.14f, -10);
            cam.orthographicSize = 8;
        }
    }
}
