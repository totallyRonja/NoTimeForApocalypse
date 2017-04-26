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

    GameObject[] buttons;

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

        buttons = new GameObject[transform.childCount];
        for(int i = 0; i < transform.childCount; i++) {
            buttons[i] = transform.GetChild(i).gameObject;
            buttons[i].SetActive(false);
        }
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

    public void Apply() {
        textComponent.text = "<size=" + captionSize + "><b>" + caption + "</b></size> \n<b>" + content + "</b>";
    }

    public void Show(string caption, string content) {
        foreach (GameObject o in buttons)
            o.SetActive(false);

        this.content = content;
        this.caption = caption;
        Apply();
    }

    public void ShowOptions(string[] options) {
        //just to be sure that all unnessecary buttons are deactivated
        Show("", "");

        for(int i=0;i<options.Length;i++) {
            buttons[i].SetActive(true);
            buttons[i].GetComponentInChildren<Text>().text = options[i];
        }
        GetComponent<RectTransform>().sizeDelta = new Vector2(300, 30 * options.Length);
    }

    public void Connect(IOptionHolder OptionChooseMethod) {
        for (int i = 0; i < buttons.Length; i++) {
            int a = i;
            buttons[a].GetComponent<Button>().onClick.AddListener(() => OptionChooseMethod.ChooseOption(a));
        }
    }
}
