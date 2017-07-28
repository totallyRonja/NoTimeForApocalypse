using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Famine : MonoBehaviour {

    Animator anim;
    [SerializeField] Collider2D playerArea;
    [SerializeField] Transform eye;
    [SerializeField] GameObject spear;
    [SerializeField] GameObject spikes;
    [SerializeField] Collider2D spikeArea;
    Transform player;

    bool canCast;

    // Use this for initialization
    void Start () {
        player = PlayerPhysics.current.transform;
        anim = transform.GetComponentInChildren<Animator>();
        InvokeRepeating("CastAbility", 0, 5);
    }
	
	void CastAbility(){
        if(!playerArea.OverlapPoint(player.position)){
            return;
        }
        int abilityIndex = (int)(Random.value * 2);
        switch(abilityIndex){
            case 0:
                ThrowSpear();
                break;
            case 1:
                MakeSpikes();
                break;
            default:
                print("invalid ability: " + abilityIndex);
                break;
        }
    }

    void ThrowSpear(){
        GameObject newSpear = GameObject.Instantiate(spear);
        newSpear.transform.position = eye.position;
        Spear spearComp = newSpear.GetComponent<Spear>();
        spearComp.player = player;
    }
    void MakeSpikes()
    {
        Vector2 spikePosition = new Vector2();
        for (int i = 0; i < 8;i++){
            spikePosition.x = Random.Range(spikeArea.bounds.min.x, spikeArea.bounds.max.x);
            spikePosition.y = Random.Range(spikeArea.bounds.min.y, spikeArea.bounds.max.y);
            if(spikeArea.OverlapPoint(spikePosition))
                break;
        }
        GameObject newSpear = GameObject.Instantiate(spikes);
        newSpear.transform.position = spikePosition;
    }
}
