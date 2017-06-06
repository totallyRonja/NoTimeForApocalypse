using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLock : MonoBehaviour {
	public int levelIndex;
	// Use this for initialization
	void OnEnable () {
        Button b = GetComponent<Button>();
        b.interactable = StaticSafeSystem.current.beatenLevels >= levelIndex;
    }
}
