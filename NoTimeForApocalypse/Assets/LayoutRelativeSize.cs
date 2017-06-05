using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LayoutRelativeSize : MonoBehaviour {

    public Vector2 relativeSize;

    // Use this for initialization
    void Update () {
		((RectTransform)transform).sizeDelta = 
				new Vector2(((RectTransform)transform.parent).rect.width * relativeSize.x, 
							((RectTransform)transform.parent).rect.height * relativeSize.y) * 
							((RectTransform)transform.parent).localScale.x;
        print(((RectTransform)transform.parent).rect.width * relativeSize.x);
    }
}
