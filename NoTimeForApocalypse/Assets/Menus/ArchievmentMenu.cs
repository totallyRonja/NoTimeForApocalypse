using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchievmentMenu : MonoBehaviour {

	public static ArchievmentMenu current;

	// Use this for initialization
	void Awake () {
        gameObject.SetActive(false);
        current = this;
    }
	
}
