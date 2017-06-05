using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class StaticSafeSystem : MonoBehaviour {

	public static StaticSafeSystem current;
	public List<string> availableTags; //tags available in this specific level
	public List<string> activeTags;
    public int beatenLevels = 0; //just count up, 4 means all levels beaten
    public int upgrades = 0; //bit wise; 1 is dash 1 << 1 is war upgrade, 1 << 2 is famine, 1 << 3 death
    public UnityEvent completedQuest;

	void Awake(){
		current = this; //singleton
		Load(); //load from playerprefs

		if(completedQuest == null)
			completedQuest = new UnityEvent(); // event
	}
	void Start () {
		TagTracker.current.tagsChanged.AddListener(tagsChanged);
	}
	
	void Load(){ // load existing tags from file
		string joinedString = PlayerPrefs.GetString("Quests");
		if(joinedString != "")
			activeTags = new List<string>(joinedString.Split(';'));
        beatenLevels = PlayerPrefs.GetInt("Levels");
        upgrades = PlayerPrefs.GetInt("Upgrades");
    }
	void Save(){ // load existing tags from file
		string joinedString = string.Join(";", activeTags.ToArray());
		PlayerPrefs.SetString("Quests", joinedString);
        PlayerPrefs.SetInt("Upgrades", upgrades);
		PlayerPrefs.SetInt("Levels", beatenLevels);
        PlayerPrefs.Save();
		print("saved quests");
	}
	void tagsChanged () {
		foreach(string tag in TagTracker.current.activeTags){
			if(availableTags.Contains(tag) && !activeTags.Contains(tag)) {
				activeTags.Add(tag);
				Save();
				completedQuest.Invoke();
				break;
			}
		}
	}
	public List<int> getUpgradeList(){
        List<int> upgradeList = new List<int>();
        for(int i=0;i<8;i++)if((upgrades>>i&1)==1)upgradeList.Add(i);
        return upgradeList;
    }
	public void addUpgrade(int index){
        upgrades |= 1 << index;
        Save();
    }
	public void finishLevel(int beaten){
        beatenLevels = Math.Max(beatenLevels, beaten);
        Save();
    }
	public void Reset(){
        activeTags = new List<string>();
        upgrades = 0;
        beatenLevels = 0;
        Save();
        completedQuest.Invoke();
    }
}
