using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randWobbleOffset : MonoBehaviour {

    public Vector2 offset = new Vector2(0f, Mathf.PI * 2f);
    public Vector2 speed = new Vector2(0.8f, 1.2f);

    // Use this for initialization
    void Start () {
        Material wobbleMat = GetComponent<Renderer>().material;
        wobbleMat.SetFloat("_Offset", Random.Range(offset.x, offset.y));
        wobbleMat.SetFloat("_Speed", Random.Range(speed.x, speed.y));
    }
}
