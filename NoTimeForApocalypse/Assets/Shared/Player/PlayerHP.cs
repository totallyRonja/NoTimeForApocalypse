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
    private Material[] playerMats;

    // Use this for initialization
    void Awake () {
        phys = GetComponent<PlayerPhysics>();
        hp = maxHp;
        current = this;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        playerMats = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++){
            playerMats[i] = renderers[i].material;
            playerMats[i].SetFloat("_Flashing", 0);
        }
    }

	public override void Hit(GameObject source, float damage = 0, Vector2 direction = new Vector2())
    {
        if(!enabled) return;
        if (hp < 0) return;
        if (!(onCooldown || StaticSafeSystem.current.accessible))
        {
            onCooldown = true;
            StartCoroutine(Reactivate());
            hp -= (int)damage;
            HpDisplay.current.setHP(hp);
        }
        if (hp <= 0)
            Die();
        phys.velocity = direction.normalized * 20;
        StartCoroutine(Blink(0.5f));
    }

    [Yarn.Unity.YarnCommand("SetHP")]
    public void YarnSetHP(string newHp){
        SetHP(int.Parse(newHp));
    }
    public void SetHP(int newHp){
        hp = newHp;
        HpDisplay.current.setHP(hp);
        if (hp <= 0)
            Die();
    }

    public void Die(string deathMessage = "you died")
    {
        hp = -1;
        HpDisplay.current.setHP(0);
        PauseMenu.current.death = true;
        PauseMenu.current.Pause(deathMessage);
    }

    IEnumerator Reactivate(){
        yield return new WaitForSeconds(iFrames);
        onCooldown = false;
    }

    IEnumerator Blink(float length){
        foreach(Material m in playerMats)
            m.SetFloat("_Flashing", 1);
        yield return new WaitForSeconds(length);
        foreach (Material m in playerMats)
            m.SetFloat("_Flashing", 0);
    }
}
