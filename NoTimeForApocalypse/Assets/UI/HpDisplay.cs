using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpDisplay : MonoBehaviour {

    public Sprite[] stages;

    public float timeLeft = 600;

    private Image render;
    private Text countDown;
    public PauseMenu pause;

	// Use this for initialization
	void Start () {
        render = GetComponent<Image>();
        countDown = GetComponentInChildren<Text>();
        //pause = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>();
        setHP(7);
	}

    private void Update() {
        if (timeLeft > 0) {
            timeLeft -= Time.deltaTime;
            if(timeLeft <= 0){
                timeLeft = 0;
                pause.death = true;
                pause.Pause("the time came and you coudn't stop the apocalypse");
            }
            countDown.text = String.Format("{0:00}:{1:00}", Mathf.Floor(Mathf.Ceil(timeLeft) / 60), Mathf.Floor(Mathf.Ceil(timeLeft) % 60));
        }
    }

    
    public void setHP (int hp) {
        int frame = Math.Min(Math.Max(7 - hp, 0), 7);
        render.sprite = stages[frame];
    }
}
