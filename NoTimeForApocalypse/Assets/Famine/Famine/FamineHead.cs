using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FamineHead : MonoBehaviour {

    public int hits = 5;
	public SpriteRenderer render;
    public string winScene;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll){
        print("hit by " + coll.gameObject);
        Spear s = coll.collider.GetComponent<Spear>();
		if(s){
            hits--;
            StartCoroutine(Blink());
        }
        if(hits <= 0){
            StaticSafeSystem.current.finishLevel(2);
            SceneManager.LoadScene(winScene);
            if (MenuManager.current)
                MenuManager.current.setMenu(1);
        }
    }

	IEnumerator Blink(){
        render.color = Color.black;
        yield return new WaitForSeconds(0.5f);
		render.color = Color.white;
    }
}
