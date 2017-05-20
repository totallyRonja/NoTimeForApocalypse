using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumor : Hitable {

    public float speed = 1;
    public int maxRecursion = 3;
    [System.NonSerialized] public int recursion = 0;
    GameObject victim;

    private Rigidbody2D rigid;

    public float dmgCd = 0;

	// Use this for initialization
	void Start () {
        victim = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
        transform.localScale = Vector3.one * (2 /(recursion + 2f));
        //Debug.Log(recursion + 1);
	}
	
	// Update is called once per frame
	void Update () {
        dmgCd -= Time.deltaTime;

        Vector2 difference = victim.transform.position - transform.position;
        float ang = Mathf.Atan2(-difference.x, difference.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, ang);
        //transform.Translate(Vector3.up * Time.deltaTime * speed);
        Vector2 randomness = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)) * 0.5f;
        rigid.AddForce(((Vector2)transform.up + randomness) * speed, ForceMode2D.Force);
    }
    
    public override void Hit(GameObject source, float damage = 0, float directionAngle = float.MinValue) {
        split(directionAngle);
    }

    void split(float dir) {
        if (recursion < maxRecursion) {
            for (int i = 0; i < 2; i++) {
                GameObject child = Instantiate(gameObject);
                child.GetComponent<Tumor>().recursion = recursion + 1;
                child.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)).normalized, ForceMode2D.Impulse);
                if(dir != float.MinValue){
                    child.GetComponent<Rigidbody2D>().AddForce(transform.up * -20, ForceMode2D.Impulse);
                }
            }
        }
        Destroy(gameObject);
    }

    void OnCollisionStay2D(Collision2D collision) {
        GameObject go = collision.gameObject;
        Hitable hit = go.GetComponent<Hitable>();
        if (hit == null || go.tag != "Player")
            return;
        hit.Hit(gameObject, 1, transform.eulerAngles.z * Mathf.Deg2Rad);
    }
}
