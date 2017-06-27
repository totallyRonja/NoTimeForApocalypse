using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume") - 1;
    }
}
