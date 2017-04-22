using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnClick() {
        GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>().Unpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
