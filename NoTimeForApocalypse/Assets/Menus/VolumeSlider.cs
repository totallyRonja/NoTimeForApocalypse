using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {

    private Slider slider;
    private AudioSource testAudio;

    // Use this for initialization
    void Start () {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(UpdateVolume);
        slider.value = PlayerPrefs.GetFloat("Volume");

        testAudio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void UpdateVolume (float val) {
        print("volume set to " + val);
        AudioListener.volume = val;
        PlayerPrefs.SetFloat("Volume", val);
		if(testAudio) testAudio.Play();
    }
}
