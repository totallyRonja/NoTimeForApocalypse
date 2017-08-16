using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;
using System;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class NPC : MonoBehaviour {

    //public string characterName = "";

    [FormerlySerializedAs("startNode")]
    public string talkToNode = "";
    public Transform textAnchor;

    [Header("Optional")]
    public TextAsset scriptToLoad;

    //private ExampleDialogueUI ui;
    private GameObject inRange = null;
    private float cooldown = 0;

    void Awake() {
        //ui = ExampleDialogueUI.current;
    }

    void Start() {
        //print(DialogueRunner.current);
        if (scriptToLoad != null){
            //print(scriptToLoad.text);
            DialogueRunner.current.AddScript(scriptToLoad);
        }
    }

    void Update(){
        if(cooldown > 0)
            cooldown -= Time.deltaTime;
        if (Input.GetButtonDown("Submit") && inRange != null && cooldown <= 0 && Time.timeScale > 0.5f){
            DialogueRunner.current.StartDialogue(talkToNode);
            cooldown = 0.5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            //ui.Show("talk", "");
            //ui.SetActive(textAnchor == null ? transform : textAnchor, true);
            inRange = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            //ui.SetActive(textAnchor == null ? transform : textAnchor, false);
            inRange = null;
        }
    }
}