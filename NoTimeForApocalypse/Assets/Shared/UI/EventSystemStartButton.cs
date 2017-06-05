using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class EventSystemStartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(EventSystem.current.currentSelectedGameObject == null || 
				!EventSystem.current.currentSelectedGameObject.activeInHierarchy)
            EventSystem.current.SetSelectedGameObject(gameObject);
	}
}
