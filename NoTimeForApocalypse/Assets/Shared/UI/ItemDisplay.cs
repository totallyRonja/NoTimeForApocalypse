using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{


    public string[] tags;
    public string[] notTags;
    public bool global; //the static global safe stuff

    void Start()
    {
        if(global)
            StaticSafeSystem.current.completedQuest.AddListener(UpdateStatus);
        else
            TagTracker.current.tagsChanged.AddListener(UpdateStatus);
        UpdateStatus();
    }
    public void UpdateStatus()
    {
        List<string> trackerTags = global ? StaticSafeSystem.current.activeTags : TagTracker.current.activeTags;
        foreach (string tag in tags)
        {
            if (!trackerTags.Contains(tag))
            {
                gameObject.SetActive(false);
                return;
            }
        }
        foreach (string nTag in notTags)
        {
            if (trackerTags.Contains(nTag))
            {
                gameObject.SetActive(false);
                return;
            }
        }
        gameObject.SetActive(true);
    }
}
