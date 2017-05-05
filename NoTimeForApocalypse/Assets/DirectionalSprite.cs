using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalSprite : MonoBehaviour {

    public Sprite[] spriteSheets;
    public Sprite idleSheet;
    public bool mirror;
    public float angle;
    public bool autoAngle;

    private Vector2 oldPos;
    private SpriteRenderer render;
    private bool idle;

	// Use this for initialization
	void Start () {
        oldPos = transform.position;
        render = GetComponent<SpriteRenderer>();
	}

    private void FixedUpdate() {
        if (autoAngle) {
            Vector2 diff = ((Vector2)transform.position - oldPos).normalized;
            oldPos = transform.position;
            idle = diff.sqrMagnitude < 0.1;
            angle = Mathf.Atan2(diff.x, diff.y);
        }
    }

    // Update is called once per frame
    void Update () {
        if (mirror) {
            //print(Mathf.RoundToInt((spriteSheets.Length - 1) * (Mathf.Abs(angle) / Mathf.PI)));
            if (idle)
                render.sprite = idleSheet;
            else
                render.sprite = spriteSheets[Mathf.RoundToInt((spriteSheets.Length-1) * (Mathf.Abs(angle)/Mathf.PI))];
            render.flipX = angle < 0;
        } else {
            throw new NotImplementedException("360° animations aren't implemented yet, please do that now");
        }
	}
}
