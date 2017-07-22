using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    public string requireTag;
    public string setTag;
	public Transform attackTarget;

    private NpcUi ui;
    private bool active = false;

    // Use this for initialization
    void Start(){
        ui = GameObject.FindGameObjectWithTag("NPCDialogueBox").GetComponent<NpcUi>();
    }

    // Update is called once per frame
    void Update(){
        if (active && Input.GetButton("Submit") && !TagTracker.current.isTag(setTag)){
            StartCoroutine(Peck());
            ui.SetActive(transform, false);
            active = false;
        }
    }

	IEnumerator Peck(){
        Bird[] birbs = GetComponentsInChildren<Bird>();
		foreach(Bird birb in birbs)
            birb.attack(attackTarget.position, 10);
        yield return new WaitForSeconds(10);
        TagTracker.current.setTag(setTag);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player") && TagTracker.current.isTag(requireTag) && !TagTracker.current.isTag(setTag))
        {
            ui.Show("give seeds to birds", "");
            ui.SetActive(transform, true);
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Player"))
        {
            ui.SetActive(transform, false);
            active = false;
        }
    }
}
