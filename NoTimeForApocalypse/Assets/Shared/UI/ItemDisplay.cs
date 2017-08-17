using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

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
            DialogueRunner.current.SwitchNode.AddListener(UpdateStatus);
        UpdateStatus();
    }
    public void UpdateStatus(){
        //List<string> trackerTags = global ? StaticSafeSystem.current.activeTags : TagTracker.current.activeTags;
        foreach (string tag in tags){
            if(global){
                if(!StaticSafeSystem.current.activeTags.Contains(tag)){
                    image.enabled = false;
                    return;
                }
            }else{
                if (tag.StartsWith("$")){
                    //check if a variable is met
                    if (!DialogueRunner.current.variableStorage.GetValue(tag).AsBool){
                        gameObject.SetActive(false);
                        image.enabled = false;
                        return;
                    }
                } else {
                    //check if the code has been visited
                    if(!DialogueRunner.current.visited(tag)){
                        gameObject.SetActive(false);
                        image.enabled = false;
                        return;
                    }
                }
            }
        }
        foreach (string nTag in notTags){
            if(global){
                if(StaticSafeSystem.current.activeTags.Contains(nTag)){
                    image.enabled = false;
                    return;
                }
            }else{
                if (tag.StartsWith("$")){
                    //check if a variable is met
                    if (DialogueRunner.current.variableStorage.GetValue(nTag).AsBool){
                        gameObject.SetActive(false);
                        image.enabled = false;
                        return;
                    }
                }else{
                    //check if the code has been visited
                    if (DialogueRunner.current.visited(nTag)){
                        gameObject.SetActive(false);
                        image.enabled = false;
                        return;
                    }
                }
            }
        }
        image.enabled = true;
        gameObject.SetActive(true);
    }
}
