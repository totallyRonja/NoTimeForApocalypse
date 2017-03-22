using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rigid;

    private 

    void Awake () {
        rigid = GetComponent<Rigidbody2D> ();
    }

	// Use this for initialization
	void Start () {
		
	}

	// Tick
	void Update () {
		//Debug.DrawLine (transform.position, transform.position + Vector3.down * 3, Color.cyan, 1, false);
	}

	public void SetVertSpeed (float vSpeed){
		Vector2 velocity = rigid.velocity;
		velocity.x = vSpeed;
		rigid.velocity = velocity;
	}

	public void SetHorizSpeed (float hSpeed){
		Vector2 velocity = rigid.velocity;
		velocity.y = hSpeed;
		rigid.velocity = velocity;
	}

	public bool IsGrounded(){
		RaycastHit2D hit = Physics2D.Raycast ((Vector2)transform.position+Vector2.down, Vector2.down, 0.01f);
		Debug.Log (hit.collider);
		return hit.collider != null;
	}
}
