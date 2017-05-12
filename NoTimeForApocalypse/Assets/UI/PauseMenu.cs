﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    
    public static PauseMenu menu; //singleton variable
    public bool death = false;
    private GameObject title;
    private bool paused;
    
	// Use this for initialization
	void Awake () {
        SetVisible(false);
        Time.timeScale = 1;
        title = transform.Find("title").gameObject;
        menu = this;
	}
	
    void Update(){
        if(Input.GetButtonDown("Cancel")){
            print(Time.timeScale > 0.5f && !paused);
            if(Time.timeScale > 0.5f && !paused){
                Pause();
            } else
            if(paused && !death){
                Unpause();
            }
        }
    }

	// Update is called once per frame
	public void Pause () {
        Time.timeScale = 0;
        SetVisible(true);
        GameObject.Find("continue").GetComponent<Button>().interactable = !death;
        paused = true;
	}
    public void Pause (string message) {
        title.GetComponentInChildren<Text>().text = message;
        Pause();
	}
    public void Unpause() {
        SetVisible(false);
        Time.timeScale = 1;
        paused = false;
    }

    void SetVisible(bool visible){
        GetComponent<Image>().enabled = visible;
        for(int i=0;i<transform.childCount;i++)
            transform.GetChild(i).gameObject.SetActive(visible);
    }
}
