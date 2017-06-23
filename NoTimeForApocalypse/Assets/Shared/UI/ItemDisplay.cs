using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{


    public string[] tags;
    public string[] notTags;
    public bool global; //the static global safe stuff

    private Image image;

    void Start(){
        image = GetComponent<Image>();
        if(global)
            StaticSafeSystem.current.completedQuest.AddListener(UpdateStatus);
        else
            TagTracker.current.tagsChanged.AddListener(UpdateStatus);
        UpdateStatus();
    }
    public void UpdateStatus(){
        List<string> trackerTags = global ? StaticSafeSystem.current.activeTags : TagTracker.current.activeTags;
        foreach (string tag in tags){
            if (!trackerTags.Contains(tag)){
                image.enabled = false;
                return;
            }
        }
        foreach (string nTag in notTags){
            if (trackerTags.Contains(nTag)){
                image.enabled = false;
                return;
            }
        }
        image.enabled = true;
    }
}
