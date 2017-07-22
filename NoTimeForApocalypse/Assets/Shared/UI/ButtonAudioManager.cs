using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAudioManager : MonoBehaviour {

    public AudioClip clip;
    private AudioSource source;

    void Awake(){
        source = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
        Button[] buttons = Resources.FindObjectsOfTypeAll<Button>();
		foreach(Button b in buttons){
            ButtonAudio audio = b.GetComponent<ButtonAudio>();
			if(!audio){
                audio = b.gameObject.AddComponent<ButtonAudio>();
            }
            audio.clip = clip;
            audio.source = source;
			
        }
    }
}
