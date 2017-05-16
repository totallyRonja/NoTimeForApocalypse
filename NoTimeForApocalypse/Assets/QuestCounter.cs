using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestCounter : MonoBehaviour {

	void Start(){
		StaticSafeSystem.current.completedQuest.AddListener(UpdateScore);
		UpdateScore();
	}
	void UpdateScore(){
		print(StaticSafeSystem.current.activeTags.Count);
		GetComponent<Text>().text = ""+StaticSafeSystem.current.activeTags.Count;
	}
}
