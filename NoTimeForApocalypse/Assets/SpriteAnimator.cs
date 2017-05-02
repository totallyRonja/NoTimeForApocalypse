using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour {

    Texture2D spritesheet;
    Sprite[] frames;
    SpriteRenderer render;
    float offset;

	// Use this for initialization
	void Start () {
        render = GetComponent<SpriteRenderer>();
        offset = UnityEngine.Random.value * 100;
	}
	
	// Update is called once per frame
	void Update () {
        if (frames != null && frames.Length > 0) {
            render.sprite = frames[(int)(Time.time+offset) % 4];
        }
	}

    public void setSpritesheet(Texture2D spritesheet) {
        this.spritesheet = spritesheet;
        frames = new Sprite[4];
        for(int i = 0; i < 4; i++) {
            frames[i] = Sprite.Create(this.spritesheet, new Rect(0, i * spritesheet.height / 4, spritesheet.width, spritesheet.height / 4), new Vector2(0.5f, 0f));
        }
    }

    public  void setSprites(Sprite[] sprites) {
        frames = sprites;
    }
}
