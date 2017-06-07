using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessibilityToggle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Toggle>().onValueChanged.AddListener(Changed);
    }

	void Changed(bool value){
        StaticSafeSystem.current.SetAccessible(value);
    }
}
