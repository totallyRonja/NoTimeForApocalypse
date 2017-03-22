using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Vector3 offset = new Vector3(0, 0, -20);

    Transform player;

	// Use this for initialization
	void Awake () {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.position + (Vector3)offset;
	}
}
