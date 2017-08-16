using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Drawing : MonoBehaviour {
    public static Drawing current;
    bool drawing = false;
    float startDraw;
    Transform player;
    Vector3 playerPos;

	void Awake(){
        current = this;
    }

    // Use this for initialization
    void Start () {
        player = PlayerPhysics.current.transform;
    }
	
	void Update(){
		if(drawing){
			if((playerPos - player.position).magnitude > 1){
                DialogueRunner.current.StartDialogue("Technician.PictureFail");
                drawing = false;
            }else if(Time.time > startDraw + 30){
				DialogueRunner.current.StartDialogue("Technician.PictureSuccess");
				drawing = false;
			}
		}
	}
	
	[YarnCommand("StartDrawing")]
	public void StartDrawing () {
        print("Start Drawing");
        startDraw = Time.time;
        drawing = true;
        playerPos = player.position;
    }
}
