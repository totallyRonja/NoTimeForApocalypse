using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour {

    public float volume = 1;

	// Use this for initialization
	void Start () {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");
        print("loaded audio volume: " + AudioListener.volume);
    }
	
	// Update is called once per frame
	/*void Update () {
        if (Input.GetButtonDown("Mute")) {
            volume = volume == 0 ? 1 : 0;
        }
        AudioListener.volume = volume;
	}*/
}
