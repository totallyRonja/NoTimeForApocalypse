using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NpcUi : MonoBehaviour {

    public string caption;
    public string content;

    public float captionSize = 30;

    private Transform active = null;

    private Text textComponent;
    private Image speechFrame;
    private GUIStyle style;
    private Camera cam;
    private bool showOptions = false;

    GameObject[] buttons;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        textComponent = GetComponent<Text>();
        textComponent.supportRichText = true;
        speechFrame = GetComponentInParent<Image>();
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
        if (active != null) {
            Vector2 pos = cam.WorldToScreenPoint(active.position);
            transform.parent.position = pos;
        }
    }

    public void SetActive(Transform tr, bool setActive) {
        if (setActive) {
            active = tr;
            textComponent.enabled = true;
            speechFrame.enabled = true;
            Apply();
        } else {
            if(tr == active) {
                active = null;
                textComponent.enabled = false;
                speechFrame.enabled = false;
            }
        }
    }

    public void Apply() {
        
        textComponent.text = "<size=" + captionSize + "><b>" + caption + "</b></size>"+((content != "" && caption!="")?("\n"):"")+"<b>" + content + "</b>";
        //print(textComponent.preferredHeight + 36);
        if (!showOptions) {
            ((RectTransform)transform.parent).sizeDelta = new Vector2(Mathf.Min(500, 36 + textComponent.preferredWidth), 36 + textComponent.preferredHeight);
            ((RectTransform)transform.parent).sizeDelta = new Vector2(Mathf.Min(500, 36 + textComponent.preferredWidth), 36 + textComponent.preferredHeight);
        }
    }

    public void Show(string caption, string content) {
        foreach (GameObject o in buttons)
            o.SetActive(false);

        this.content = content;
        this.caption = caption;
        Apply();
        showOptions = false;
    }

    public void ShowOptions(string[] options) {
        //just to be sure that all unnessecary buttons are deactivated
        Show("", "");

        for(int i=0;i<options.Length;i++) {
            buttons[i].SetActive(true);
            buttons[i].GetComponentInChildren<Text>().text = options[i];
        }
        EventSystem.current.SetSelectedGameObject(buttons[0]);
        print((RectTransform)transform.parent);
        ((RectTransform)transform.parent).sizeDelta = new Vector2(500, 36 + 50 * options.Length);

        showOptions = true;
    }

    public void Connect(IOptionHolder OptionChooseMethod) {
        for (int i = 0; i < buttons.Length; i++) {
            int a = i;
            buttons[a].GetComponent<Button>().onClick.AddListener(() => OptionChooseMethod.ChooseOption(a));
        }
    }
}
