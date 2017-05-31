using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StaticSafeSystem : MonoBehaviour {

	public static StaticSafeSystem current;
	public List<string> availableTags; //tags available in this specific level
	public List<string> activeTags;
	public UnityEvent completedQuest;

	void Awake(){
		current = this; //singleton
		loadTags(); //load from playerprefs

		if(completedQuest == null)
			completedQuest = new UnityEvent(); // event
	}
	void Start () {
		TagTracker.current.tagsChanged.AddListener(tagsChanged);
	}
	
	void loadTags(){ // load existing tags from file
		string joinedString = PlayerPrefs.GetString("Quests");
		if(joinedString != "")
			activeTags = new List<string>(joinedString.Split(';'));
	}

	void saveTags(){ // load existing tags from file
		string joinedString = string.Join(";", activeTags.ToArray());
		PlayerPrefs.SetString("Quests", joinedString);
		PlayerPrefs.Save();
		print("saved quests");
	}

	// Update is called once per frame
	void tagsChanged () {
		foreach(string tag in TagTracker.current.activeTags){
			if(availableTags.Contains(tag) && !activeTags.Contains(tag)) {
				activeTags.Add(tag);
				saveTags();
				completedQuest.Invoke();
				break;
			}
		}
	}

	public void Reset(){
        activeTags = new List<string>();
        saveTags();
        completedQuest.Invoke();
    }
}
