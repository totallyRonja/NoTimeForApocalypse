using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagTracker : MonoBehaviour {

    public List<string> activeTags;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool isTag(string tag) {
        return activeTags.Contains(tag);
    }

    public void setTag(string tag) {
        activeTags.Add(tag);
    }
}
