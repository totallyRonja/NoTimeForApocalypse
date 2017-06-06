using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDescriptionAndLevel : MonoBehaviour {
    public ReloadButton descriptionApply;
    public int descriptionMenuIndex;
    public string scene;
	[TextArea] public string description;

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	void OnClick () {
        MenuManager.current.setMenu(descriptionMenuIndex);
        descriptionApply.scene = scene;
		if(description != ""){
            Debug.LogWarning("Please implement setting the description!!");
        }
    }
}
