using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {

    private Slider slider;
    private AudioSource testAudio;

    public float defaultVolume = 0.3f;

    // Use this for initialization
    void Start () {
        slider = GetComponent<Slider>();
        print(PlayerPrefs.GetFloat("Volume"));
        float volume = PlayerPrefs.GetFloat("Volume") - 1;
        if(volume < 0)
            volume = defaultVolume;

        testAudio = GetComponent<AudioSource>();
        slider.value = volume;
        slider.onValueChanged.AddListener(UpdateVolume);
    }
	
	// Update is called once per frame
	void UpdateVolume (float val) {
//        print("volume set to " + val);
        AudioListener.volume = val;
        PlayerPrefs.SetFloat("Volume", 1 + val);
		if(testAudio) testAudio.Play();
    }
}
