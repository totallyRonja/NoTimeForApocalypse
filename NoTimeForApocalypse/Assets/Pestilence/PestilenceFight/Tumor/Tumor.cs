using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumor : Hitable {

    public float speed = 1;
    public float acceleration = 1;
    public int maxRecursion = 3;
    [SerializeField] AudioClip hurtSound;
    [System.NonSerialized] public int recursion = 0;
    GameObject victim;

    private Rigidbody2D rigid;
    private AudioSource source;

    // Use this for initialization
    void Start () {
        victim = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
        transform.localScale = Vector3.one * (2 /(recursion + 2f));
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 goalVelocity = victim.transform.position - transform.position;
        goalVelocity.Normalize();
        goalVelocity *= speed;
        Vector2 difference = goalVelocity - rigid.velocity;
        rigid.velocity += difference.normalized * acceleration * Time.fixedDeltaTime * Mathf.Min(difference.magnitude, 1);
        float angle = Mathf.Atan2(rigid.velocity.y, rigid.velocity.x) - Mathf.PI * 0.5f;
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }
    
    public override void Hit(GameObject source, float damage = 0, Vector2 direction = new Vector2()) {
        StartCoroutine(split(direction));
    }

    IEnumerator split(Vector2 dir) {
        if (recursion < maxRecursion) {
            for (int i = 0; i < 2; i++) {
                GameObject child = Instantiate(gameObject);
                child.GetComponent<Tumor>().recursion = recursion + 1;
                child.GetComponent<Rigidbody2D>().velocity = rigid.velocity;
                child.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized * acceleration, ForceMode2D.Impulse);
            }
        }
        this.source.PlayOneShot(hurtSound);
        rigid.simulated = false;
        transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    void OnCollisionStay2D(Collision2D collision) {
        GameObject go = collision.gameObject;
        Hitable hit = go.GetComponent<Hitable>();
        if (hit == null || go.tag != "Player")
            return;
        hit.Hit(gameObject, 1, transform.up);
    }
}
