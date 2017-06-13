using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War : MonoBehaviour {
    public float speed = 10;
    public Collider2D area;
    private GameObject player;
    private Rigidbody2D rigid;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		if(area.OverlapPoint(player.transform.position)){
			rigid.velocity = (player.transform.position - transform.position).normalized * speed;
		} else {
            rigid.velocity = (area.transform.position - transform.position).normalized * speed;
        }
	}
}
