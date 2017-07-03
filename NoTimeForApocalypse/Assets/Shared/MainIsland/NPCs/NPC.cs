using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//using Inklewriter;
//using Inklewriter.Player;
//using Inklewriter.Unity;
using System;
using UnityEngine.EventSystems;

public class NPC : MonoBehaviour, IOptionHolder {

    public Transform textAnchor;

    private NpcUi ui;
    private TagTracker tags;

    public string dialogueFile;

    private GameObject inRange = null;
    private InkleDialogue dialogue = null;
    private List<InkleOption> isChoosing = null;
    private bool inControl = false;
    private float cooldown = 0;

    void Awake() {
        ui = GameObject.
            FindGameObjectWithTag("NPCDialogueBox").
            GetComponent<NpcUi>();
        tags = GameObject.FindWithTag("InfoTags").GetComponent<TagTracker>();
    }

    void Start() {
        var resource = Resources.Load(dialogueFile) as TextAsset;
        if (!resource) {
            Debug.LogWarning("Inklewriter story could not be loaded: " + dialogueFile);
            return;
        }
        string storyJson = resource.text;
        dialogue = new InkleDialogue(storyJson);
    }

    private void LateUpdate() {
        if (Input.GetButtonDown("Submit") && inRange != null && cooldown <= 0) {
            if (inControl) {
                if (dialogue.NextDialogue() == null) {
                    if (dialogue.IsEnd()) {
                        Release();
                    } else {
                        List<InkleOption> o = new List<InkleOption>(dialogue.getOptions());
                        List<InkleOption> remove = new List<InkleOption>();
                        foreach(InkleOption i in o) {
                            string[] con = i.ifConditions;
                            string[] nonCon = i.notIfConditions;
                            foreach (string c in con) {
                                if (!tags.isTag(c)) {
                                    remove.Add(i);
                                    break;
                                }
                            }
                            foreach (string c in nonCon) {
                                if (tags.isTag(c)) {
                                    remove.Add(i);
                                    break;
                                }
                            }
                        }
                        foreach(InkleOption i in remove) {
                            o.Remove(i);
                        }

                        isChoosing = o;
                        
                        remove = new List<InkleOption>();
                        foreach (InkleOption i in o)
                            if (i.text.Length <= 1)
                                remove.Add(i);
                        if(o.Count == remove.Count){
                            StartCoroutine(ChooseOption(0));
                            return;
                        }
                        foreach (InkleOption i in remove)
                            o.Remove(i);
                        
                        isChoosing = o;
                        string[] sOptions = new string[o.Count];
                        for(int i=0;i<o.Count;i++)
                            sOptions[i] = o[i].text;
                        ui.Connect(this);
                        ui.ShowOptions(sOptions);
                    }
                } else {
                    ui.Show("", dialogue.GetText());
                    foreach (string flag in dialogue.GetFlags())
                        tags.setTag(flag);
                }
            } else {
                //inRange.GetComponent<PlayerController>().enabled = false;
                inControl = true;
                dialogue.StartDialogue();
                ui.Show("", dialogue.GetText());
                foreach (string flag in dialogue.GetFlags()) {
                    print("set: " + flag);
                    tags.setTag(flag);
                }
                Time.timeScale = 0;
            }
        }

        if (Input.GetButtonDown("Cancel") && inRange != null) {
            Release();
        }

        if (cooldown > 0)
            cooldown -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            ui.Show("talk", "");
            ui.SetActive(textAnchor == null ? transform : textAnchor, true);
            inRange = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            ui.SetActive(textAnchor == null ? transform : textAnchor, false);
            inRange = null;
        }
    }

    void Release() {
        /*if(inRange != null)
            inRange.GetComponent<PlayerController>().enabled = true;
        else
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = true;*/
        inControl = false;
        ui.Show("talk", "");

        cooldown = 0.5f;
        Time.timeScale = 1;
    }

    public IEnumerator ChooseOption(int index) {
        yield return null;
        if(isChoosing.Count == 0){
            Debug.LogError("Empty Options???");
            Release();
            yield break;
        }
        try {
            if (TagTracker.current.debug){
                print("options:");
                foreach (InkleOption o in isChoosing)
                    print(o.text);
            }
            dialogue.activeNode = isChoosing[index].linkPath;
        } catch (NullReferenceException) {
            Release();
            yield break;
        }
        if(dialogue.activeNode.IsNull) {
            Release();
            yield break;
        }
        
        ui.Show("", dialogue.activeNode[0].Value);
        foreach (string flag in dialogue.GetFlags()) {
            tags.setTag(flag);
        }
    }
}