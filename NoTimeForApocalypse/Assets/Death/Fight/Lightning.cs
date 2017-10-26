using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour {

    public float damage;
    public float radius;
    public float damageDelay;
    public float lifetime;

	// Use this for initialization
	void Start () {
        StartCoroutine(Life());
    }
	
	IEnumerator Life(){
        yield return new WaitForSeconds(damageDelay);
        DamagePlayer();
        yield return new WaitForSeconds(lifetime - damageDelay);
        Destroy(gameObject);
    }

	void OnDrawGizmosSelected(){
        Gizmos.matrix = Matrix4x4.Scale(new Vector3(1, 1, 0));
        Gizmos.DrawWireSphere(transform.position, radius);
    }

	void DamagePlayer(){
        float distance = Vector3.Distance(transform.position, PlayerPhysics.current.transform.position);
		if(distance < radius){
            PlayerHP.current.Hit(gameObject, damage);
        }
    }
}
