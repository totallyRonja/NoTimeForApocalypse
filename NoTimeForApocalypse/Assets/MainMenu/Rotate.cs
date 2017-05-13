using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class Rotate : MonoBehaviour {

	public float rps;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 0, Time.deltaTime * rps * 360);
	}
	#if UNITY_EDITOR
	void OnEnable(){
		EditorApplication.update += Update;
	}
	void OnDisable(){
		EditorApplication.update -= Update;
	}
	#endif
}
