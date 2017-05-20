using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : Hitable {

	PlayerPhysics rigid;

	[NonSerialized] public float direction = 0;

    private HpDisplay hpDisplay;
    public int maxHp = 7;
    [NonSerialized] public int hp = -1;
    public float iFrames = 1; //actually in seconds but y'know
    private float iTimer;

    public PauseMenu pause;
   
    [NonSerialized] public PlayerWalk walk;
    [NonSerialized] public PlayerPunch punch;
    [NonSerialized] public PlayerDash dash;

    // Use this for initialization
    void Awake () {
		rigid = GetComponent<PlayerPhysics>();
        hpDisplay = GameObject.FindWithTag("HpDisplay").GetComponent<HpDisplay>();

        if (hp < 0)
            hp = maxHp;
	}
	
	// Update is called once per frame
	void Update () {
		if(walk) walk.Walk();
		if(punch) punch.Punch();
        if(dash) dash.Dash();

        iTimer -= Time.deltaTime;
	}

    public override void Hit(GameObject source, float damage = 0, float directionAngle = float.MinValue) {
        if (hp < 0) return;
        if (iTimer < 0) {
            iTimer = iFrames;
            hp -= (int)damage;
            hpDisplay.setHP(hp);
        }
        if (hp <= 0)
            Die();
        if(directionAngle != float.MinValue) {
            rigid.velocity = new Vector2(Mathf.Sin(directionAngle) * 20, Mathf.Cos(directionAngle) * 20);
            //Debug.Log(directionAngle);
            //Debug.DrawRay(transform.position, rigid.velocity);
        }
    }

    float getAngle(Vector2 fromVector2) {
        return Mathf.Atan2(-fromVector2.x, fromVector2.y) * Mathf.Rad2Deg ;
    }

    public void Die(string deathMessage = "you died") {
        hp = -1;
        hpDisplay.setHP(0);
        pause.death = true;
        pause.Pause(deathMessage);
    }
}
