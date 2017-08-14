using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Pickup : MonoBehaviour {

    public string itemName;
    public string setTag;

    private NpcUi ui;
    private bool active = false;

    // Use this for initialization
    void Start () {
        ui = NpcUi.current;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Submit") && active) {
            ExampleVariableStorage.current.SetTag(setTag);
            DialogueRunner.current.SwitchNode.Invoke();
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            ui.Show("pick up "+itemName, "");
            ui.SetActive(transform, true, new Vector2(0, 1));
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
