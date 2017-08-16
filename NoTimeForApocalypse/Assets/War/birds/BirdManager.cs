using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    public string requireNode;
    public string setTag;
	public Transform attackTarget;

    private NpcUi ui;
    [SerializeField]private bool active = false;

    // Use this for initialization
    void Start(){
        ui = NpcUi.current;
    }

    // Update is called once per frame
    void Update(){
        if (active && Input.GetButton("Submit") && Yarn.Unity.DialogueRunner.current.visited(requireNode)){
            //ExampleVariableStorage.current.SetTag(setTag);
            //Yarn.Unity.DialogueRunner.current.SwitchNode.Invoke();
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
        ExampleVariableStorage.current.SetTag(setTag);
        Yarn.Unity.DialogueRunner.current.SwitchNode.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player") && Yarn.Unity.DialogueRunner.current.visited(requireNode))
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