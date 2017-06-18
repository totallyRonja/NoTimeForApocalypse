using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TagTracker : MonoBehaviour {

    public List<string> activeTags;
    public UnityEvent tagsChanged;
    public static TagTracker current; //singleton instance (last added tracker)

	// Use this for initialization
	void Awake () {
        current = this;

        if(tagsChanged == null)
            tagsChanged = new UnityEvent();
	}

    public bool isTag(string tag) {
        if(tag.StartsWith("?$")) return StaticSafeSystem.current;
        return activeTags.Contains(tag);
    }

    public void setTag(string tag)
    {
        if (tag.StartsWith("-"))
            activeTags.Remove(tag.Substring(1));
        else if (tag.StartsWith("!"))
        {
            switch (tag.Substring(1))
            {
                case "heal":
                    PlayerHP.current.SetHP(PlayerHP.current.maxHp);
                    print("powered up");
                    break;
                default:
                    setTag(tag.Substring(1));
                    return;
            }
        }
        else if(!isTag(tag))
            activeTags.Add(tag);
        tagsChanged.Invoke();
    }
    public void Reset()
    {
        activeTags = new List<string>();
        tagsChanged.Invoke();
    }
}
