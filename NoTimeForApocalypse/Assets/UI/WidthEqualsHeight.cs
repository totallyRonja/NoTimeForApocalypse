using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WidthEqualsHeight : MonoBehaviour {
	void OnRectTransformDimensionsChange(){
		((RectTransform)transform).sizeDelta = new Vector2(((RectTransform)transform).rect.height, 0);
	}
}
