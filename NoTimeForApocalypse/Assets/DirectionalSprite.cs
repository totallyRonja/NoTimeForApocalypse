using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalSprite : MonoBehaviour {

    public Sprite[] spriteSheets;
    public Sprite idleSheet;
    public bool mirror;
    public float angle;
    public bool idle;
    public bool autoAngle;

    public MonoBehaviour transitionTo;

    private Vector2 oldPos;
    private SpriteRenderer render;
    private Vector4 animProperties; //anim start, anim end, fps, time offset
    private Vector4 spriteProperties; //x, y, endtime, time
    private Material mat;

	// Use this for initialization
	void Awake () {
        oldPos = transform.position;
        render = GetComponent<SpriteRenderer>();
        mat = GetComponent<Renderer>().material;
        animProperties = mat.GetVector("_FrameProperties");
        spriteProperties = mat.GetVector("_Sprites");
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
            mat.SetVector("_Sprites", new Vector4(spriteProperties.x, spriteProperties.y, spriteProperties.z, Time.time));
        else {
            animProperties.w = 0;
            mat.SetVector("_FrameProperties", animProperties);
        }
            
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
        if (transitionTo != null && Time.time > spriteProperties.z) {
            enabled = false;
            transitionTo.enabled = true;
        }
	}

    private void OnEnable() {
        if (transitionTo != null) {
            spriteProperties.z = Time.time + (animProperties.y - animProperties.x) / animProperties.z; //amount of frames divided by fps
            animProperties.w = -Time.time;
            mat.SetVector("_FrameProperties", animProperties);
        } else {
            animProperties.w = 0;
            mat.SetVector("_FrameProperties", animProperties);
            spriteProperties.w = 0;
            mat.SetVector("_Sprites", spriteProperties);
        }
    }
}
