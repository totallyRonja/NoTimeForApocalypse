using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;
using System;
using UnityEngine.EventSystems;
using Yarn.Unity.Example;

public class NPC : MonoBehaviour {

    public string characterName = "";

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
        if (scriptToLoad != null){
            FindObjectOfType<Yarn.Unity.DialogueRunner>().AddScript(scriptToLoad);
        }
    }

    void Update(){
        cooldown -= Time.deltaTime;
        if (Input.GetButtonDown("Submit") && inRange != null && cooldown <= 0){
            FindObjectOfType<Yarn.Unity.DialogueRunner>().StartDialogue(talkToNode);
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