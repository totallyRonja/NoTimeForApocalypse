using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TransformBasedOnLayout : MonoBehaviour {
    public float columns = 6;
    public float padding = 0;
    public float spacing = 0;

    private LayoutGroup layout;

	// Use this for initialization
	void Start () {
		layout = GetComponent<LayoutGroup>();
	}
	
	// Update is called once per frame
	void Update () {
		((RectTransform)transform).sizeDelta = new Vector2(0, layout.preferredHeight);
        GridLayoutGroup grid = GetComponent<GridLayoutGroup>();
		if(grid)grid.cellSize = Vector2.one * (((RectTransform)transform).rect.width-2*padding-columns*spacing)/columns;
    }
}
