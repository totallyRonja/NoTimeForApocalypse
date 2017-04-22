using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadButton : MonoBehaviour {

    public string scene;

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnClick() {
        GameObject menu = GameObject.FindWithTag("PauseMenu");
        if (menu) menu.GetComponent<PauseMenu>().Unpause();
        
        SceneManager.LoadScene(scene==""?SceneManager.GetActiveScene().name:scene);
    }
}
