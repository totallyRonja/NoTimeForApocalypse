﻿using System.Collections;
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
	public int usedCoins;
	public bool accessible;
    public UnityEvent completedQuest;

	void Awake(){
		current = this; //singleton
		Load(); //load from playerprefs

		if(completedQuest == null)
			completedQuest = new UnityEvent(); // event
	}
	void Start () {
		if(TagTracker.current)
			TagTracker.current.tagsChanged.AddListener(tagsChanged);
	}
	
	void Load(){ // load existing tags from file
        string joinedString = PlayerPrefs.GetString("Quests");
		if(joinedString != "")
			activeTags = new List<string>(joinedString.Split(';'));
        beatenLevels = PlayerPrefs.GetInt("Levels");
        upgrades = PlayerPrefs.GetInt("Upgrades");
        accessible = (upgrades >> 31 & 1) == 1;
        usedCoins = 0;
        for (int i = 0; i < 16;i++)
			if(((upgrades >> i)&1)==1)
				usedCoins+=6;
    }
	void Save(){ // load existing tags from file
		string joinedString = string.Join(";", activeTags.ToArray());
		PlayerPrefs.SetString("Quests", joinedString);
		upgrades ^= (-(accessible?1:0) ^ upgrades) & (1 << 31);

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
	public bool getUpgrade(int index){
        return ((upgrades >> index) & 1) == 1;
    }
	public bool getBuyable(){
        return activeTags.Count - usedCoins >= 6;
    }
	public bool buyUpgrade(int index){
        if(activeTags.Count - usedCoins < 6)
            throw new Exception("This cannot be bought with this amount of money, fix your dialogue");
        //return false if upgrade is already set or no money is there
        if(((upgrades >> index) & 1) == 1 || activeTags.Count-usedCoins < 6)
            return false;
        upgrades |= 1 << index;
        usedCoins += 6;
        Save();
        return true;
    }
	public int getFreeCoins(){
        return activeTags.Count - usedCoins;
    }
	public void finishLevel(int beaten){
        beatenLevels = Math.Max(beatenLevels, beaten+1);
        Save();
    }
	public void SetAccessible(bool accessible){
        this.accessible = accessible;
        Save();
    }
	public void Reset(){
        activeTags = new List<string>();
        upgrades &= 1 << 31;
        beatenLevels = 0;
        Save();
        completedQuest.Invoke();
    }
}