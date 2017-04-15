using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Inklewriter;
using Inklewriter.Player;
using Inklewriter.Unity;

//namespace Inklewriter.Unity {
public class NPC : MonoBehaviour {

    public Transform textAnchor;

    private NpcUi ui;

    public string dialogueFile;

    private GameObject inRange = null;
    private StoryPlayer player = null;
    private PlayChunk chunk = null;
    private int chunkProgress = 0;
    private bool inControl = false;

    void Awake() {
        ui = GameObject.
            FindGameObjectWithTag("NPCDialogueBox").
            GetComponent<NpcUi>();
    }

    void Start() {
        var resource = Resources.Load(dialogueFile) as TextAsset;
        if (!resource) {
            Debug.LogWarning("Inklewriter story could not be loaded: " + dialogueFile);
            return;
        }
        string storyJson = resource.text;
        StoryModel model = StoryModel.Create(storyJson);
        player = new StoryPlayer(model, new UnityMarkupConverter());

    }

    private void Update() {
        if (Input.GetButtonDown("Fire1") && inRange != null) {
            if (inControl) {
                chunkProgress++;
                ui.caption = "";
                ui.content = chunk.Paragraphs[chunkProgress].Text;
                ui.Apply();
            } else {
                inRange.GetComponent<PlayerController>().enabled = false;
                inControl = true;
                chunk = player.CreateFirstChunk();
                chunkProgress = 0;
                ui.caption = "";
                ui.content = chunk.Paragraphs[chunkProgress].Text;
                ui.Apply();
            }
        }

        if (Input.GetButtonDown("Cancel") && inRange != null) {
            inRange.GetComponent<PlayerController>().enabled = true;
            inControl = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            ui.caption = "[action]";
            ui.content = "";
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
}
//}