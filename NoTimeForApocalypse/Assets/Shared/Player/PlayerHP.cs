using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : Hitable {

    public static PlayerHP current;

	public int maxHp = 7;
    /*[NonSerialized] */public int hp = -1;
	public float iFrames = 1; //actually in seconds but y'know
    private bool onCooldown;
    private PlayerPhysics phys;

    // Use this for initialization
    void Awake () {
        phys = GetComponent<PlayerPhysics>();
        hp = maxHp;
        current = this;
    }

	public override void Hit(GameObject source, float damage = 0, float directionAngle = float.MinValue)
    {
        if (hp < 0) return;
        if (!onCooldown)
        {
            onCooldown = true;
            StartCoroutine(Reactivate());
            hp -= (int)damage;
            HpDisplay.current.setHP(hp);
        }
        if (hp <= 0)
            Die();
        if (directionAngle != float.MinValue)
        {
            phys.velocity = new Vector2(Mathf.Sin(directionAngle) * 20, Mathf.Cos(directionAngle) * 20);
            //Debug.Log(directionAngle);
            //Debug.DrawRay(transform.position, rigid.velocity);
        }
    }

    public void SetHP(int newHp){
        hp = newHp;
        HpDisplay.current.setHP(hp);
        if (hp <= 0)
            Die();
    }

    IEnumerator Reactivate(){
        yield return new WaitForSeconds(iFrames);
        onCooldown = false;
    }

	public void Die(string deathMessage = "you died")
    {
        hp = -1;
        HpDisplay.current.setHP(0);
        PauseMenu.current.death = true;
        PauseMenu.current.Pause(deathMessage);
    }
}
