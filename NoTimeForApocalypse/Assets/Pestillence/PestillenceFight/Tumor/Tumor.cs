using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumor : MonoBehaviour {

    GameObject victim;

	// Use this for initialization
	void Start () {
        victim = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 difference = victim.transform.position - transform.position;
        Vector2 toVector2 = new Vector2(0, 1);
        Vector2 fromVector2 = difference;

        float ang = Vector2.Angle(fromVector2, toVector2);
        Vector3 cross = Vector3.Cross(fromVector2, toVector2);

        if (cross.z > 0)
            ang = 360 - ang;

        Debug.Log(ang);
        transform.rotation = Quaternion.Euler(0, 0, ang);
    }
}
