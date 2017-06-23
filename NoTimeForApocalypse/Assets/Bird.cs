using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

	public Sprite stillSprite;
    public Sprite flyingSprite;
    public float speed;
    public float acceleration;

    public bool flying;
	private SpriteRenderer render;
    private Rigidbody2D rigid;
    private Vector3 home;
    private Vector3 attackPoint;

    private bool attacking;
    private float offset;
    private float frequency;

    // Use this for initialization
    void Start () {
        render = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        offset = Random.Range(0f, 100f);
        frequency = Random.Range(0f, 0.5f);
        home = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 goalVelocity = ((attacking?attackPoint:home) - transform.position);
        //if(goalVelocity.magnitude > speed)
        goalVelocity = goalVelocity.normalized * speed;
        rigid.velocity += (goalVelocity - rigid.velocity).normalized * acceleration * Time.deltaTime;

        flying = rigid.velocity.magnitude > 0.1f;
        transform.rotation = flying ? Quaternion.AngleAxis((Mathf.Atan2(rigid.velocity.y, -rigid.velocity.x)) * -Mathf.Rad2Deg, Vector3.forward) : Quaternion.identity;
        render.flipY = flying ? rigid.velocity.x > 0 : false;
        render.flipX = flying ? false : Mathf.Sin(offset + Time.time * Mathf.PI * 2 * frequency) > 0;
        render.sprite = flying ? flyingSprite : stillSprite;
        render.sortingOrder = flying ? 1 : 0;
    }

    void FixedUpdate(){
        if(!attacking && rigid.velocity.magnitude * Time.deltaTime > (home - transform.position).magnitude){
            rigid.velocity = Vector2.zero;
            transform.position = home;
        }
    }

    public void attack(Vector3 point, float time = Mathf.Infinity){
        attackPoint = point;
        attacking = true;
        float angle = Random.Range(0, Mathf.PI*2);
        rigid.AddForce(new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * 5, ForceMode2D.Impulse);
        StartCoroutine(stopAttack(time));
    }

    IEnumerator stopAttack(float time){
        yield return new WaitForSeconds(time);
        attacking = false;
    }
}
