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
        if (Input.GetButton("Fire1") && active) {
            TagTracker.tracker.setTag(setTag);
            Destroy(gameObject);
        }
        if (TagTracker.tracker.isTag(setTag)) {
            Destroy(GetComponent<Collider2D>());
            Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !TagTracker.tracker.isTag(setTag)) {
            ui.Show("[pick up]", "");
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
