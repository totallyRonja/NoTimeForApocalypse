using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TagTracker : MonoBehaviour {

    public List<string> activeTags;
    public UnityEvent tagsChanged;
    public static TagTracker current; //singleton instance (last added tracker)
    public bool debug;

    // Use this for initialization
    void Awake () {
        current = this;

        if(tagsChanged == null)
            tagsChanged = new UnityEvent();
	}

    public bool isTag(string tag) {
        if(tag.StartsWith("?")){
            switch(tag.Substring(1)){
                case "$":
                    if(debug) print((StaticSafeSystem.current.canBuyUpgrade() ? "can" : "can't") + " buy stuff");
                    return StaticSafeSystem.current.canBuyUpgrade();
                default:
                    if (debug) print((StaticSafeSystem.current.canBuyUpgrade() ? "has " : "doesn't have") + int.Parse(tag.Substring(1)) + ". upgrade");
                    return StaticSafeSystem.current.hasUpgrade(int.Parse(tag.Substring(1)));
            }
        }
        if (debug) print((activeTags.Contains(tag) ? "has " : "doesn't have") + " tag " + tag);
        return activeTags.Contains(tag);
    }

    public void setTag(string tag){
        if (tag.StartsWith("-")){
            if(debug)print("remove tag" + tag.Substring(1));
            activeTags.Remove(tag.Substring(1));
        }
        else if (tag.StartsWith("!")){
            switch (tag.Substring(1)){
                case "heal":
                    if (debug) print("heal the player full");
                    PlayerHP.current.SetHP(PlayerHP.current.maxHp);
                    break;
                default:
                    setTag(tag.Substring(1));
                    return;
            }
        }else if (tag.StartsWith("$")){
            if (debug) print("buy" + int.Parse(tag.Substring(1)) + ". upgrade");
            StaticSafeSystem.current.buyUpgrade(int.Parse(tag.Substring(1)));
        }else if (!isTag(tag)){
            if (debug) print("add tag " + tag);
            activeTags.Add(tag);
        }
        tagsChanged.Invoke();
    }
    public void Reset(){
        if (debug) print("delete all tags");
        activeTags.Clear();
        tagsChanged.Invoke();
    }
}
