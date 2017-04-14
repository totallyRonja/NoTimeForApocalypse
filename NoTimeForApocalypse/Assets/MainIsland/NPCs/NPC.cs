using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public Transform textAnchor;

    private NpcUi ui;

	// Use this for initialization
	void Awake () {
        ui = GameObject.
            FindGameObjectWithTag("NPCDialogueBox").
            GetComponent<NpcUi>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            ui.caption = "[action]";
            ui.content = "";
            ui.SetActive(textAnchor == null ? transform : textAnchor, true);
        }
    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.tag == "Player") {
            ui.SetActive(textAnchor == null ? transform : textAnchor, false);
        }
    }
}
