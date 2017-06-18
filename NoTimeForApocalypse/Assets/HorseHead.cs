using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HorseHead : Hitable {
    public int hp;
    public float speed;
    public float acceleration;
    public AudioClip hurtSound;

    private GameObject player;
    private Rigidbody2D rigid;
    private AudioSource audio;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }
	
	void FixedUpdate () {
		if(hp<=0)return;
        Vector2 goalVelocity = player.transform.position - transform.position;
        goalVelocity.Normalize();
        goalVelocity *= speed;
        Vector2 difference = goalVelocity - rigid.velocity;
        rigid.velocity += difference.normalized * acceleration * Time.fixedDeltaTime * Mathf.Min(difference.magnitude, 1);
        float angle = Mathf.Atan2(rigid.velocity.y, rigid.velocity.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        transform.localScale = new Vector3(1, rigid.velocity.x>0?1:-1, 1);
    }

    public override void Hit(GameObject source, float damage = 0, Vector2 direction = new Vector2()){
        audio.PlayOneShot(hurtSound);
        if (hp > 0){
            hp -= Mathf.FloorToInt(damage);
            if (hp <= 0){
                StartCoroutine(remove());
				rigid.velocity = direction.normalized;
            } else {
				rigid.velocity = Vector2.zero;
			}
        } else {
			rigid.velocity += direction.normalized;
		}
    }

	IEnumerator remove(){
        //float startTime = Time.time;
        //yield return null;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

	void OnCollisionEnter2D(Collision2D coll){
		if(!coll.gameObject.CompareTag("Player"))return;
        Hitable hpPool = coll.gameObject.GetComponent<Hitable>();
		if(!hpPool)return;
        hpPool.Hit(gameObject, 1, -coll.contacts[0].normal);
    }
}