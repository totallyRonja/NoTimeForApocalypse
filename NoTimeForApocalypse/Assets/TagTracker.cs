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
        if(tag.StartsWith("-"))
            activeTags.Remove(tag.Substring(1));
        else
            if(!isTag(tag))
                activeTags.Add(tag);
    }
}
