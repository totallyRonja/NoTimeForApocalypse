using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NpcUi : MonoBehaviour {
    public static NpcUi current;

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
    private Vector3 offset = Vector2.zero;

    void Awake(){
        current = this;
    }

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        textComponent = GetComponent<Text>();
        textComponent.supportRichText = true;
        speechFrame = GetComponentInParent<Image>();

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
            Vector2 pos = cam.WorldToScreenPoint(active.position + offset);
            transform.parent.position = pos;
        }
    }

    public void SetActive(Transform tr, bool setActive) {
        SetActive(tr, setActive, Vector2.zero);
    }

    public void SetActive(Transform tr, bool setActive, Vector2 offset){
        if (setActive)
        {
            this.offset = offset;
            active = tr;
            textComponent.enabled = true;
            speechFrame.enabled = true;
            Apply();
        }
        else
        {
            if (tr == active)
            {
                active = null;
                textComponent.enabled = false;
                speechFrame.enabled = false;
            }
        }
    }

    public void Apply() {
        
        textComponent.text = "<b>" + caption + "</b>"+((content != "" && caption!="")?("\n"):"")+"<b>" + content + "</b>";
        //print(textComponent.preferredHeight + 36);
        if (!showOptions) {
            //((RectTransform)transform.parent).sizeDelta = new Vector2(Mathf.Min(500, 36 + textComponent.preferredWidth), 36 + textComponent.preferredHeight);
            //((RectTransform)transform.parent).sizeDelta = new Vector2(Mathf.Min(500, 36 + textComponent.preferredWidth), 36 + textComponent.preferredHeight);
        }
    }

    public void Show(string caption, string content) {
        foreach (GameObject o in buttons)
            o.SetActive(false);

        this.content = content;
        this.caption = caption;
        showOptions = false;
        Apply();
    }
}
