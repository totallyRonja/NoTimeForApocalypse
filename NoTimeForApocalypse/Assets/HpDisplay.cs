using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpDisplay : MonoBehaviour {

    public Sprite[] stages;

    private Image render;

	// Use this for initialization
	void Start () {
        render = GetComponent<Image>();
        render.sprite = stages[0];
	}
	
	// Update is called once per frame
	public void setHP (int hp) {
        int frame = Math.Min(Math.Max(7 - hp, 0), 7);
        render.sprite = stages[frame];
    }
}
