using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Inklewriter;
using Inklewriter.Player;
using Inklewriter.Unity;
using System;
using UnityEngine.EventSystems;

//namespace Inklewriter.Unity {
public class NPC : MonoBehaviour, IOptionHolder {

    public Transform textAnchor;

    private NpcUi ui;
    private TagTracker tags;

    public string dialogueFile;

    private GameObject inRange = null;
    private StoryModel model = null;
    private StoryPlayer player = null;
    private PlayChunk chunk = null;
    private int chunkProgress = 0;
    private bool inControl = false;
    private List<Option> isChoosing = null;
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
        model = StoryModel.Create(storyJson);
        player = new StoryPlayer(model, new UnityMarkupConverter());

    }

    private void LateUpdate() {
        if (Input.GetButtonDown("Submit") && inRange != null && cooldown <= 0) {
            if (inControl) {
                chunkProgress++;
                if (chunkProgress >= chunk.Paragraphs.Count) {
                    if (chunk.IsEnd) {
                        Release();
                    } else {
                        List<Option> o = new List<Option>(chunk.Stitches[chunk.Stitches.Count-1].Content.Options);
                        List<Option> remove = new List<Option>();
                        foreach(Option i in o) {
                            
                            List<string> con = i.IfConditions;
                            List<string> nonCon = i.NotIfConditions;
                            foreach (string c in con) {
                                //print("check for " +c);
                                if (!tags.isTag(c)) {
                                    //print("you need "+c);
                                    remove.Add(i);
                                    break;
                                }
                            }
                            foreach (string c in nonCon) {
                                //print("check for not " + c);
                                if (tags.isTag(c)) {
                                    //print("you don't need " + c);
                                    remove.Add(i);
                                    break;
                                }
                            }
                        }
                        foreach(Option i in remove) {
                            o.Remove(i);
                        }
                        string[] sOptions = new string[o.Count];
                        for(int i=0;i<o.Count;i++) {
                            sOptions[i] = o[i].Text;
                        }
                        ui.Connect(this);
                        ui.ShowOptions(sOptions);
                        isChoosing = o;
                    }
                } else {
                    ui.Show("", chunk.Paragraphs[chunkProgress].Text);
                    foreach (string flag in chunk.Stitches[chunkProgress].Content.Flags) {
                        print("set: " + flag);
                        tags.setTag(flag);
                    }
                }
            } else {
                inRange.GetComponent<PlayerController>().enabled = false;
                inControl = true;
                chunk = player.CreateFirstChunk();
                chunkProgress = 0;
                ui.Show("", chunk.Paragraphs[chunkProgress].Text);
                foreach (string flag in chunk.Stitches[chunkProgress].Content.Flags) {
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
        if(inRange != null)
            inRange.GetComponent<PlayerController>().enabled = true;
        else
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = true;;
        inControl = false;
        ui.Show("talk", "");

        cooldown = 0.5f;
        Time.timeScale = 1;
    }

    public void ChooseOption(int index) {
        if (isChoosing == null)
            return;

        try {
            chunk = player.CreateChunkForOption(isChoosing[index]);
        } catch (NullReferenceException) {
            Release();
            return;
        }
        if(chunk == null) {
            Release();
            return;
        }
        
        isChoosing = null;
        chunkProgress = 0;
        ui.Show("", chunk.Paragraphs[chunkProgress].Text);
        foreach (string flag in chunk.Stitches[chunkProgress].Content.Flags) {
            print("set: " + flag);
            tags.setTag(flag);
        }
        EventSystem.current.SetSelectedGameObject(null);
        chunkProgress = -1; //I HAVE NO CLUE WHY I HAVE TO DO THIS BUT IT WORKS
    }
}