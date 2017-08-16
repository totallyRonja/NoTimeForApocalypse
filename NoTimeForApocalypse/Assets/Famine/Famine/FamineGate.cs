using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamineGate : MonoBehaviour {

    public Collider2D[] closerColl;
    public string requireNode;
    public Sprite closedGate;
    public Sprite openGate;
    public SpriteRenderer gate;

    private NpcUi ui;
	private bool active;

    // Use this for initialization
    void Start () {
		ui = NpcUi.current;
	}
	
	// Update is called once per frame
	void Update () {
		if (active && Input.GetButtonDown("Submit") && Yarn.Unity.DialogueRunner.current.visited(requireNode))
        {
            if(gate.sprite == closedGate){
                gate.sprite = openGate;
				foreach(Collider2D coll in closerColl)
                    coll.enabled = false;
            }else{
				gate.sprite = closedGate;
                foreach (Collider2D coll in closerColl)
                    coll.enabled = true;
			}

        }
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Yarn.Unity.DialogueRunner.current.visited(requireNode))
        {
            ui.Show("use Key", "");
            ui.SetActive(transform, true);
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ui.SetActive(transform, false);
            active = false;
        }
    }
}
