using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WidthEqualsHeight : MonoBehaviour {
    public float factor = 1;
    void OnRectTransformDimensionsChange(){
		((RectTransform)transform).sizeDelta = new Vector2(((RectTransform)transform).rect.height*factor, 0);
	}
}
