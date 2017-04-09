using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pestillence : Hitable {

    [SerializeField] PuddleSpawner puddleSpawner;

    [SerializeField] float puddleDelay;
    private float puddleTimer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PuddleLogic();

    }

    void PuddleLogic(){
        puddleTimer += Time.deltaTime;
        if (puddleTimer > puddleDelay) {
            puddleTimer = 0;
            puddleSpawner.SpawnPuddle();
        }
    }

	public override void hit(GameObject source, float damage = 0, float directionAngle = 0){
        
	}
}
