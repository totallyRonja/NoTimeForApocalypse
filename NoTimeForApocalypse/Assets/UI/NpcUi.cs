using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcUi : MonoBehaviour {

    public string caption;
    public string content;

    public float captionSize = 30;

    private Transform active = null;

    private Text textComponent;
    private GUIStyle style;
    private Camera cam;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        textComponent = GetComponent<Text>();
        textComponent.supportRichText = true;
        /*style = new GUIStyle();
        style.richText = true;
        GUILayout.Label("<size=100>ass</size>wipe", style);
        //textComponent.Layout*/

        SetActive(null, false);
	}
	
	// Update is called once per frame
	void Update () {
		if(active != null) {
            Vector2 pos = cam.WorldToScreenPoint(active.position);
            transform.position = pos;
        }
	}

    public void SetActive(Transform tr, bool setActive) {
        if (setActive) {
            active = tr;
            textComponent.enabled = true;
            Apply();
        } else {
            if(tr == active) {
                active = null;
                textComponent.enabled = false;
            }
        }
    }

    void Apply() {
        textComponent.text = "<size=" + captionSize + "><b>" + caption + "</b></size> \n" + content;
    }
}
