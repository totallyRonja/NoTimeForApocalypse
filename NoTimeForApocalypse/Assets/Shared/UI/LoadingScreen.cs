using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour {

    public static LoadingScreen current;

    void Awake(){
        current = this;
    }
	void OnEnable () {
        StartCoroutine(Loaded());
    }
	
	// Update is called once per frame
	IEnumerator Loaded () {
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }
}
