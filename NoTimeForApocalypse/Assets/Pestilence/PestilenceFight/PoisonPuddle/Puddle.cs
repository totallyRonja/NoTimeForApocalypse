using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour {

    [SerializeField] float expansionTime = 1;
    [SerializeField] AnimationCurve size;

    private float timer = 0;
    private Renderer render;

    // Use this for initialization
    void Start () {
        transform.localScale = Vector3.zero;
        render = GetComponentInParent<Renderer>();

        Material mat = render.material;
        mat.SetFloat("_Size", 0);
        render.material = mat;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        float currentSize = size.Evaluate(timer / expansionTime);
        transform.localScale = Vector2.one * currentSize;
        Material mat = render.material;
        mat.SetFloat("_Size", currentSize);
        render.material = mat;
        if (timer > expansionTime)
            Destroy(transform.parent.gameObject);
	}

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<PlayerWalk>().slowed = true;
        }
    }
}
