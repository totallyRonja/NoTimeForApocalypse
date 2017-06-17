using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalSprite : MonoBehaviour {

    public Sprite[] spriteSheets;
    public Sprite idleSheet;
    public bool mirror = true;
    public float angle;
    public bool idle;
    public bool autoAngle;
    public MonoBehaviour transitionTo;

    private Vector2 oldPos;
    private SpriteRenderer render;
    public Vector4 spriteProperties; //x, y, endtime, time
    public Vector4 animProperties; //anim start, anim end, fps, time offset
    private Material mat;

	// Use this for initialization
	void Awake () {
        oldPos = transform.position;
        render = GetComponent<SpriteRenderer>();
        mat = GetComponent<Renderer>().material;
        if(animProperties==Vector4.zero)animProperties = mat.GetVector("_FrameProperties");
        if(spriteProperties==Vector4.zero)spriteProperties = mat.GetVector("_Sprites");
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
        if (transitionTo != null)
            mat.SetVector("_Sprites", new Vector4(spriteProperties.x, spriteProperties.y, 0, Time.time));
        if (mirror) {
            //print(Mathf.RoundToInt((spriteSheets.Length - 1) * (Mathf.Abs(angle) / Mathf.PI)));
            if (idle)
                render.sprite = idleSheet;
            else
                render.sprite = spriteSheets[Mathf.RoundToInt((spriteSheets.Length-1) * (Mathf.Abs(angle)/Mathf.PI))];
            render.flipX = angle < 0;
        } else {
            throw new NotImplementedException("360° animations aren't implemented yet, please do that now (or use 180°)");
        }
	}

    private void OnEnable() {
        DirectionalSprite[] dirs = GetComponents<DirectionalSprite>();
        foreach(DirectionalSprite d in dirs)
            if(d != this)
                d.enabled = false;
        if (transitionTo != null) {
            animProperties.w = Time.time;
            mat.SetVector("_FrameProperties", animProperties);
            StartCoroutine(Transition((animProperties.y - animProperties.x) / animProperties.z));
        } else {
            animProperties.w = 0;
            mat.SetVector("_FrameProperties", animProperties);
            spriteProperties.w = 0;
            mat.SetVector("_Sprites", spriteProperties);
        }
    }
    IEnumerator Transition(float waitTime){
        yield return new WaitForSeconds(waitTime);
        enabled = false;
        transitionTo.enabled = true;
    }
}
