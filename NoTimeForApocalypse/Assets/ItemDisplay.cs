using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour {

    public string[] tags;
    public string[] notTags;

    public void UpdateStatus() {
        foreach (string tag in tags) {
            if (!TagTracker.tracker.isTag(tag)) {
                gameObject.SetActive(false);
                return;
            }
        }
        foreach (string nTag in notTags) {
            if (TagTracker.tracker.isTag(nTag)) {
                gameObject.SetActive(false);
                return;
            }
        }
        gameObject.SetActive(true);
    }
}
