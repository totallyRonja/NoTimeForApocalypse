using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour {

    [SerializeField] float expansionTime = 1;
    [SerializeField] AnimationCurve size;

    private float timer = 0;

    // Use this for initialization
    void Start () {
        transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        transform.localScale = Vector2.one * size.Evaluate(timer/expansionTime);
        if (timer > expansionTime)
            Destroy(gameObject);
	}

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<PlayerController>().slowed = true;
        }
    }
}
