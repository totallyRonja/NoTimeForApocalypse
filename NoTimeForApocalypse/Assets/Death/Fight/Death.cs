using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : Hitable {
    public PlayerState state = PlayerState.SLEEPING;
    public DirectionalSprite unmelt;
    public float speed;
    public GameObject killObject;
    public float health = 3;
    
    Material mat;
    Rigidbody2D rigid;
    Vector3 origin;
    Collider2D coll;


    void Start () {
        origin = transform.position;

        rigid = GetComponent<Rigidbody2D>();

        mat = GetComponentInChildren<Renderer>().material;
        coll = GetComponent<Collider2D>();
    }
	
	void Update () {
        switch (state)
        {
            case PlayerState.SLEEPING:
                Sleep();
                break;
            case PlayerState.MOVING:
                Move();
                break;
        }
    }

    void Sleep(){
		rigid.velocity = (origin - transform.position).normalized * speed;

		if(Vector2.Distance(PlayerHP.current.transform.position, origin) < 11)
            TransitionTo(PlayerState.MOVING);
	}

	void Move(){
        rigid.velocity = Vector2.zero;
    }

	IEnumerator Attack(){
        Vector2 fleePoint = origin + (origin - PlayerHP.current.transform.position).normalized * 7;
        float startAttack = Time.time;
        coll.enabled = false;
        while(rigid.position != fleePoint){
            rigid.MovePosition(Vector2.MoveTowards(transform.position, fleePoint, speed * Time.deltaTime));
            yield return null;
        }
        coll.enabled = true;
        rigid.isKinematic = true;
        unmelt.enabled = true;
        yield return new WaitForSeconds(1f);
        Instantiate(killObject, PlayerPhysics.current.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        TransitionTo(PlayerState.MOVING);
    }

	void TransitionTo(PlayerState newState){
        print("transition to " + newState.ToString());
        switch (newState)
        {
            case PlayerState.SLEEPING:
                break;
            case PlayerState.MOVING:
                if(Vector2.Distance(PlayerHP.current.transform.position, origin) > 11){
                    TransitionTo(PlayerState.SLEEPING);
                    return;
                }
                StartCoroutine(Attack());
                break;
        }
        state = newState;
    }

	public override void Hit(GameObject source, float damage = 0, Vector2 directionAngle = default(Vector2)){
        health -= damage;
        StartCoroutine(Blink(0.5f));
    }

    IEnumerator Blink(float duration){
        mat.SetFloat("_Flashing", 1);
        yield return new WaitForSeconds(duration);
        mat.SetFloat("_Flashing", 0);
    }

	public enum PlayerState
    {
		SLEEPING,
		MOVING
    }
}
