using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pestillence : Hitable {

    [SerializeField] PuddleSpawner puddleSpawner;
    private Collider2D puddleColl;
    public GameObject tumor;

    [SerializeField] float puddleDelay;
    private float puddleTimer = 0;

    public int max_hp;
    private int hp = 50;
    public int stages = 5;
    private int stage = 0;

    private Transform player;

    public Sprite[] stageSheets;

	// Use this for initialization
	void Start () {
        hp = max_hp;
        puddleColl = puddleSpawner.GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
            if (puddleColl.OverlapPoint(player.transform.position) && hp > 0) {
                GameObject newTumor = Instantiate(tumor, transform.position, transform.rotation);
                newTumor.GetComponent<Tumor>().recursion = 4;
            }
        }
    }

	public override void Hit(GameObject source, float damage = 0, float directionAngle = 0){
        hp -= (int)damage;

        if (hp <= 0) {
            print("YOU WON");
            //Destroy(gameObject);
            GetComponentInChildren<SpriteRenderer>().sprite =  stageSheets[5];
            return;
        }

        if (hp <= max_hp * (stages - stage - 1) / (stages)) {
            stage++;
            GameObject newTumor = Instantiate(tumor, transform.position, transform.rotation);
            newTumor.GetComponent<Tumor>().recursion = 4 - stage; //ugly hack, if I ever touch this script again please fix this!!
            print("stage " + stage + "at" + hp + " hp");

            GetComponentInChildren<SpriteRenderer>().sprite = stageSheets[stage];
            //GetComponentInChildren<SpriteRenderer>().sprite = sprites[stage];
        }
	}
}
