using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFade : MonoBehaviour {

	public float fade{ 
		get { return _fade; } 
		set { _fade = value;
			ApplyTint(); } }

    [Range(0f, 1f)] public float _fade;

    private List<Graphic> graphics;

    void ApplyTint(){
        Awake();

        foreach (Graphic g in graphics){
            Color c = g.color;
            c.a = _fade;
            g.color = c;
        }
        //print("set tint to " + _fade);
    }

	void Awake(){
		graphics = new List<Graphic>(GetComponentsInChildren<Graphic>());
    }
}
