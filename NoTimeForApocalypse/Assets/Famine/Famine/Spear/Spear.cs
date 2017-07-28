using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Hitable {

    [SerializeField] float speed;
    [HideInInspector]public Transform player;
    Vector3 forward;
    Vector3 cachedPos;

    // Use this for initialization
    void Start () {
		if(!player){
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
		cachedPos = transform.localPosition;
        forward = player.localPosition - cachedPos;
        forward.Normalize();
        forward *= speed;

        float angle = Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
    }
	
	// Update is called once per frame
	void Update () {
        cachedPos += forward * Time.deltaTime;
        transform.localPosition = cachedPos;
		if((player.position - cachedPos).sqrMagnitude > 1000){
			Destroy(gameObject);
		}
    }

    public override void Hit(GameObject source, float damage = 0, Vector2 directionAngle = new Vector2()){
        forward *= -1;
        transform.Rotate(Vector3.forward, 180);
    }
}
