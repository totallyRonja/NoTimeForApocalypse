using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioClip intro;
    public AudioClip main;
    public AudioClip boss;

    private new AudioSource audio;

    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
        audio.loop = false;
        audio.clip = intro;
        audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (!audio.isPlaying) {
            audio.loop = true;
            audio.clip = main;
            audio.Play();
        }
    }

    public void InitBoss() {
        audio.loop = true;
        audio.clip = intro;
        audio.Play();
    }
}
