using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pestillence : Hitable {

    [SerializeField] PuddleSpawner puddleSpawner;
    public GameObject tumor;

    [SerializeField] float puddleDelay;
    private float puddleTimer = 0;

    public int max_hp;
    private int hp = 50;
    public int stages = 5;
    private int stage = 0;

	// Use this for initialization
	void Start () {
        hp = max_hp;
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
        hp -= (int)damage;

        if (hp <= max_hp * (stages - stage - 1) / (stages)) {
            stage++;
            GameObject newTumor = Instantiate(tumor, transform.position + (Vector3.down * 5), transform.rotation);
            newTumor.GetComponent<Tumor>().maxRecursion = stage;
            print("stage " + stage + "at" + hp + " hp");
        }

        if (hp <= 0) {
            print("YOU WON");
            Time.timeScale = 0;
            Destroy(gameObject);
        }
	}
}
