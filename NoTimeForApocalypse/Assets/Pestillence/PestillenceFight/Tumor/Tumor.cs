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
        Debug.Log(recursion + 1);
	}
	
	// Update is called once per frame
	void Update () {
        dmgCd -= Time.deltaTime;

        Vector2 difference = victim.transform.position - transform.position;
        Vector2 toVector2 = new Vector2(0, 1);
        Vector2 fromVector2 = difference;

        float ang = Vector2.Angle(fromVector2, toVector2);
        Vector3 cross = Vector3.Cross(fromVector2, toVector2);

        if (cross.z > 0)
            ang = 360 - ang;

        transform.rotation = Quaternion.Euler(0, 0, ang);
        //transform.Translate(Vector3.up * Time.deltaTime * speed);
        rigid.AddForce(transform.up * speed, ForceMode2D.Force);
    }
    
    public override void hit(GameObject source, float damage = 0, float directionAngle = 0) {
        split();
    }

    void split() {
        if (recursion < maxRecursion) {
            for (int i = 0; i < 2; i++) {
                GameObject child = Instantiate(gameObject);
                child.GetComponent<Tumor>().recursion = recursion + 1;
                child.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-speed, speed), Random.Range(-speed, speed)), ForceMode2D.Impulse);
            }
        }
        Destroy(gameObject);
    }

    void OnCollisionStay2D(Collision2D collision) {
        GameObject go = collision.gameObject;
        Hitable hit = go.GetComponent<Hitable>();
        if (hit == null || go.tag != "Player")
            return;
        hit.hit(gameObject, 1, transform.eulerAngles.z);
    }
}
