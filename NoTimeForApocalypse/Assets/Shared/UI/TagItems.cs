using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TagItems : MonoBehaviour {

    RectTransform layout;
    RectTransform[] items;
    Image background;

	// Use this for initialization
	void Start () {
        background = GetComponent<Image>();
        layout = (RectTransform) transform.GetChild(0);
        items = new RectTransform[layout.childCount];
        for(int i = 0; i < items.Length; i++) {
            items[i] = (RectTransform)layout.GetChild(i);
        }
	}
	
	// Update is called once per frame
	void Update () {
        float height = layout.rect.height;
        int active = 0;
        for (int i = 0; i < items.Length; i++) {
            items[i].sizeDelta = Vector2.one * height;
            if (items[i].gameObject.activeSelf)
                active++;
        }
        background.enabled = active > 0;
        ((RectTransform)transform).sizeDelta = new Vector2(2 * 20 + active * (height + 10) - 10, ((RectTransform)transform).sizeDelta.y);
	}
}
