﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pestilence : Hitable {

    [SerializeField] PuddleSpawner puddleSpawner;
    private Collider2D puddleColl;
    public GameObject tumor;

    [SerializeField] float puddleDelay;
    [SerializeField] AudioClip hurtSound;
    private AudioSource source;
    private float puddleTimer = 0;

    public int max_hp;
    private int hp = 50;
    public int stages = 5;
    private int stage = 0;

    private Transform player;

    public Sprite[] stageSheets;
    public string winScene;
    public Wall wall;
    private Material pestMat;

    // Use this for initialization
    void Start () {
        hp = max_hp;
        source = GetComponent<AudioSource>();
        puddleColl = puddleSpawner.GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pestMat = GetComponentInChildren<Renderer>().material;
        pestMat.SetFloat("_Flashing", 0);
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
                newTumor.SetActive(true);
                if(wall != null)
                    wall.Reset();
            }
        }
    }

	public override void Hit(GameObject source, float damage = 0, Vector2 direction = new Vector2()){
        hp -= (int)damage;

        if (hp <= 0) {
            print("YOU WON");
            StartCoroutine(WinScreen());
            GetComponentInChildren<SpriteRenderer>().sprite =  stageSheets[5];
            return;
        }

        if (hp <= max_hp * (stages - stage - 1) / (stages)) {
            stage++;
            GameObject newTumor = Instantiate(tumor, transform.position, transform.rotation);
            newTumor.GetComponent<Tumor>().recursion = 4 - stage; //ugly hack, if I ever touch this script again please fix this!!
            newTumor.SetActive(true);
            print("stage " + stage + "at" + hp + " hp");

            GetComponentInChildren<SpriteRenderer>().sprite = stageSheets[stage];
            //GetComponentInChildren<SpriteRenderer>().sprite = sprites[stage];
        }
        this.source.PlayOneShot(hurtSound);
        print("playsound");
        StartCoroutine(blink(0.1f));
    }

    IEnumerator WinScreen(){
        HpDisplay.current.countDownScale = 0;
        yield return new WaitForSeconds(1);
        StaticSafeSystem.current.finishLevel(0);
        SceneManager.LoadScene(winScene);
        yield return null;
        if(MenuManager.current)
            MenuManager.current.setMenu(1);
    }

    IEnumerator blink(float time){
        pestMat.SetFloat("_Flashing", 1);
        yield return new WaitForSeconds(time);
        pestMat.SetFloat("_Flashing", 0);
    }
}
