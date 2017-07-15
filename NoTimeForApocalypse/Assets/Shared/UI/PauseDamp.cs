using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseDamp : MonoBehaviour {

	public float fadeSpeed = 1;
	private float pause = 0;
	private Image render;
	// Use this for initialization
	void Start () {
		render = GetComponent<Image>();

		Color c = render.color;
		c.a = pause;
		render.color = c;
	}
	
	// Update is called once per frame
	void Update () {
		if(pause < 1 && Time.timeScale == 0){
			pause = Mathf.Min(pause + Time.unscaledDeltaTime * fadeSpeed, 1);
			Color c = render.color;
			c.a = pause;
			render.color = c;
		}
		if(pause > 0 && Time.timeScale != 0){
			pause = Mathf.Max(pause - Time.unscaledDeltaTime * fadeSpeed, 0);
			Color c = render.color;
			c.a = pause;
			render.color = c;
		}
        render.enabled = pause > 0;
    }
}
