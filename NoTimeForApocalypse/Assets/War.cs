using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War : Hitable {
    public float speed = 10;
    public float acceleration = 20;
    public Collider2D area;
    public GameObject projectile;
    public DirectionalSprite throwAnim;
    public Transform projectileOffset;
    public int hp = 20;
    public float decayTime = 5;

    private GameObject player;
    private Rigidbody2D rigid;
    private Vector2 velocityGoal;
    private bool canThrow = true;

    // Use this for initialization
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        if (area.OverlapPoint(player.transform.position))
        {
            if (hp > 0)
                velocityGoal = (player.transform.position - transform.position).normalized * speed;
        }else{
            Vector3 difference = area.transform.position - transform.position;
            if (hp > 0)
                velocityGoal = difference.normalized * speed * Mathf.Min(difference.magnitude, 1);
        }
    }

    void FixedUpdate(){
        Vector2 difference = velocityGoal - rigid.velocity;
        rigid.velocity += difference.normalized * acceleration * Time.fixedDeltaTime * Mathf.Min(difference.magnitude, 1);

        float playerDistance = (transform.position - player.transform.position).magnitude;
        if(playerDistance > 5 && playerDistance < 30 && canThrow && Random.value < 0.01f)
            StartCoroutine(throwHead());
    }
    void OnCollisionEnter2D(Collision2D coll){
        if (!coll.gameObject.CompareTag("Player")) return;
        Hitable hpPool = coll.gameObject.GetComponent<Hitable>();
        if (!hpPool) return;
        hpPool.Hit(gameObject, 2, -coll.contacts[0].normal);
    }
    public override void Hit(GameObject source, float damage = 0, Vector2 direction = new Vector2()){
        if (hp <= 0) return;
        direction.Normalize();
        rigid.velocity = rigid.velocity/2;
        hp -= (int)damage;
        if (hp <= 0)
        {
            StartCoroutine(dying());
            velocityGoal = Vector2.zero;
            rigid.simulated = false;
        }
    }
    IEnumerator throwHead(){
        canThrow = false;
        throwAnim.enabled = true;
        rigid.simulated = false;
        yield return new WaitForSeconds(0.7f);
        rigid.simulated = true;
        rigid.velocity = Vector2.zero;
        Instantiate(projectile, (projectileOffset?projectileOffset:transform).position, transform.rotation, transform.parent);
        yield return new WaitForSeconds(2);
        canThrow = true;
    }
    IEnumerator dying(){
        float startTime = Time.time;
        while(Time.time < startTime+decayTime){
            float t = (Time.time - startTime) / decayTime;
            transform.localScale = new Vector3(1, 1-t, 1);
            yield return null;
        }
        Destroy(gameObject);
    }
}
