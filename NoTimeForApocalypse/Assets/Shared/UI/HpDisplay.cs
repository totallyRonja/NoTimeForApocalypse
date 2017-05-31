using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpDisplay : MonoBehaviour {

    public static HpDisplay current;

    public Sprite[] stages;
    public float timeLeft = 600;

    private Image render;
    private Text countDown;

	// Use this for initialization
	void Awake () {
        render = GetComponent<Image>();
        countDown = GetComponentInChildren<Text>();
        //pause = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>();
        setHP(7);
        current = this;
    }

    private void Update() {
        if (timeLeft > 0) {
            timeLeft -= Time.deltaTime;
            if(timeLeft <= 0){
                timeLeft = 0;
                PauseMenu.current.death = true;
                PauseMenu.current.Pause("the time came and you coudn't stop the apocalypse");
            }
            countDown.text = String.Format("{0:00}:{1:00}", Mathf.Floor(Mathf.Ceil(timeLeft) / 60), Mathf.Floor(Mathf.Ceil(timeLeft) % 60));
        }
    }

    
    public void setHP (int hp) {
        int frame = Math.Min(Math.Max(7 - hp, 0), 7);
        render.sprite = stages[frame];
    }
}
