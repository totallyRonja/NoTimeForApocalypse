using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningImpact : MonoBehaviour {

	
    public float waveDelay = 0.2f;
    public GameObject flash;

    [Header("Waves")]
    public Transform[] center;
    public Transform[] innerWave;
    public Transform[] outerWave;

    // Use this for initialization
    void Start () {
        StartCoroutine(Waves());
    }
	
	IEnumerator Waves(){
        SpawnWave(center);
        yield return new WaitForSeconds(waveDelay);
		SpawnWave(innerWave);
        yield return new WaitForSeconds(waveDelay);
		SpawnWave(outerWave);
        Destroy(gameObject);
    }

	void SpawnWave(Transform[] spawnLocations){
		foreach(Transform location in spawnLocations){
            Instantiate(flash, location.position, Quaternion.identity);
        }
	}
}
