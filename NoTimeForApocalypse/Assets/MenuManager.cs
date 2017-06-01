using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public static MenuManager current;

    public CanvasFade[] menus;
    public int activeMenu;

    public float interpolateDuration;

    // Use this for initialization
    void Awake () {
        current = this;

        for (int i = 0; i < menus.Length;i++){
            menus[i].gameObject.SetActive(i == activeMenu);
        }
    }

    public void setMenu(int menuIndex){
        int oldMenu = activeMenu;
        activeMenu = menuIndex;
        StartCoroutine(fadeMenus(oldMenu));
    }

	IEnumerator fadeMenus(int oldMenu){
        menus[activeMenu].gameObject.SetActive(true);
        menus[activeMenu].fade = 0;
        ((RectTransform)menus[activeMenu].transform).sizeDelta = Vector2.zero;
        for (float i = 0; i < interpolateDuration; i += Time.deltaTime)
        {
            menus[activeMenu].fade = i / interpolateDuration;
			((RectTransform)menus[activeMenu].transform).sizeDelta = Vector2.one * Mathf.Pow(i / interpolateDuration, 2) * 1000;

            menus[oldMenu].fade = 1- i / interpolateDuration;
			((RectTransform)menus[oldMenu].transform).sizeDelta = Vector2.one * Mathf.Pow(1 + i / interpolateDuration, 2) * 1000;
            yield return null;
        }
        menus[oldMenu].gameObject.SetActive(false);
		menus[activeMenu].fade = 1;
    }
}
