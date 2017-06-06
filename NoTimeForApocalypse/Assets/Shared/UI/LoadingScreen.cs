using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

    public static LoadingScreen current;
    private List<Graphic> graphics;

    void Awake(){
        current = this;
        graphics = new List<Graphic>(GetComponentsInChildren<Graphic>());
        foreach (Behaviour foo in graphics)
            foo.enabled = false;
    }
	public void StartLoading () {
        StartCoroutine(Loaded());
    }
	
	// Update is called once per frame
	IEnumerator Loaded () {
        foreach (Behaviour foo in graphics)
            foo.enabled = true;
        
        yield return new WaitForEndOfFrame();

        foreach (Behaviour foo in graphics)
            foo.enabled = false;
    }
}
