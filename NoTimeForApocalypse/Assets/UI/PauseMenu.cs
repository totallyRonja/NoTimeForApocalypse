using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    /*
	// Use this for initialization
	void Awake () {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        print("unpaused at start");
	}*/
	
	// Update is called once per frame
	public void Pause () {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        print("paused");
	}

    public void Unpause() {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        print("Unpaused");
    }
}
