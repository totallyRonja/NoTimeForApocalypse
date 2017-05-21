using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryIntro : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
        GetComponentInChildren<Button>().onClick.AddListener(Accept);
    }
	
	// Update is called once per frame
	void Accept () {
        PauseMenu.current.Unpause();
        transform.parent.gameObject.SetActive(false);
    }
}
