using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpDisplay : MonoBehaviour {

    public static HpDisplay current;

    public Sprite[] stages;
    public float timeLeft = 600;
    [NonSerialized] public float countDownScale = 1;

    private Image render;
    private Text countDown;

	// Use this for initialization
	void Awake () {
        render = GetComponent<Image>();
        countDown = GetComponentInChildren<Text>();
        setHP(7);
        current = this;
    }

    void Start(){
        countDown.text = StaticSafeSystem.current.accessible ? "as long as you need" : String.Format("{0:00}:{1:00}", Mathf.Floor(Mathf.Ceil(timeLeft) / 60), Mathf.Floor(Mathf.Ceil(timeLeft) % 60));
    }

    private void Update() {
        if (timeLeft > 0 && !StaticSafeSystem.current.accessible) {
            timeLeft -= Time.deltaTime * countDownScale;
            if(timeLeft <= 0){
                timeLeft = 0;
                PauseMenu.current.death = true;
                PauseMenu.current.Pause("the time came and you couldn't stop the apocalypse");
            }
            countDown.text = String.Format("{0:00}:{1:00}", Mathf.Floor(Mathf.Ceil(timeLeft) / 60), Mathf.Floor(Mathf.Ceil(timeLeft) % 60));
        }
    }

    
    public void setHP (int hp) {
        int frame = Math.Min(Math.Max(7 - hp, 0), 7);
        render.sprite = stages[frame];
    }
}
