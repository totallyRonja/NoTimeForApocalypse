using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMultiple : MonoBehaviour {

    public string setTag;
    [Range(0, 1)]public float probability = 1;
    private NpcUi ui;
    private bool active = false;

    // Use this for initialization
    void Start() {
        ui = GameObject.FindGameObjectWithTag("NPCDialogueBox").GetComponent<NpcUi>();
        TagTracker.current.tagsChanged.AddListener(checkLife);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButton("Submit") && active) {
            if(probability!=1)Random.InitState(System.Environment.TickCount);
            if(Random.value < probability)
                TagTracker.current.setTag(setTag);
            gameObject.SetActive(false);
        }
    }
    public void checkLife(){
        if (TagTracker.current.isTag(setTag)){
            foreach (Collider2D c in GetComponents<Collider2D>())
                if (c.isTrigger)
                    Destroy(c);
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
