using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pustule : MonoBehaviour {

    public string setTag;

    private NpcUi ui;
    private bool active = false;

    // Use this for initialization
    void Start() {
        ui = GameObject.FindGameObjectWithTag("NPCDialogueBox").GetComponent<NpcUi>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButton("Submit") && active) {
            TagTracker.current.setTag(setTag);
            gameObject.SetActive(false);
        }
        if (TagTracker.current.isTag(setTag)) {
            Destroy(GetComponent<Collider2D>());
            Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !TagTracker.current.isTag(setTag)) {
            ui.Show("pick up " + setTag, "");
            ui.SetActive(transform, true);
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            ui.SetActive(transform, false);
            active = false;
        }
    }
}
