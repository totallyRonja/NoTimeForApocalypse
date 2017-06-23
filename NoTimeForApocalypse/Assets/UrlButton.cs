using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UrlButton : MonoBehaviour {

    public string url;
	
    void Start () {
        GetComponent<Button>().onClick.AddListener(OpenUrl);
    }
	
	void OpenUrl(){
        Application.OpenURL(url);
    }
}
