using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : Hitable {
    [SerializeField] State state = State.SLEEPING;
    public DirectionalSprite unmelt;
    public float speed;

    Rigidbody2D rigid;
    Vector3 origin;

    void Start () {
        origin = transform.position;

        rigid = GetComponent<Rigidbody2D>();
    }
	
	void Update () {
        switch (state)
        {
            case State.SLEEPING:
                Sleep();
                break;
            case State.MOVING:
                Move();
                break;
        }
    }

	void Sleep(){
		rigid.velocity = (origin - transform.position).normalized * speed;

		if(Vector2.Distance(PlayerHP.current.transform.position, origin) < 11)
            TransitionTo(State.MOVING);
	}

	void Move(){
        rigid.velocity = Vector2.zero;
    }

	IEnumerator Attack(){
        Vector2 fleePoint = origin + (origin - PlayerHP.current.transform.position).normalized * 6;
        float startAttack = Time.time;
		while(Time.time < startAttack + 1f){
            rigid.velocity = (fleePoint - (Vector2)transform.position).normalized * speed;
            yield return null;
        }
		rigid.isKinematic = true;
        yield return new WaitForSeconds(2.0f);
        TransitionTo(State.MOVING);
    }

	void TransitionTo(State newState){
        print("transition to " + newState.ToString());
        switch (newState)
        {
            case State.SLEEPING:
                break;
            case State.MOVING:
                if(Vector2.Distance(PlayerHP.current.transform.position, origin) > 11){
                    TransitionTo(State.SLEEPING);
                    return;
                }
				unmelt.enabled = true;
                StartCoroutine(Attack());
                break;
        }
        state = newState;
    }

	public override void Hit(GameObject source, float damage = 0, Vector2 directionAngle = default(Vector2)){

	}

	enum State
    {
		SLEEPING,
		MOVING
    }
}
