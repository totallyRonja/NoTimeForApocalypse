using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleSpawner : MonoBehaviour {

    [SerializeField] GameObject puddlePrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void SpawnPuddle() {
        Collider2D coll = GetComponent<Collider2D>();
        Bounds bounds = coll.bounds;

        Vector2 newPuddlePos = Vector2.zero;
        while(newPuddlePos == Vector2.zero){
            newPuddlePos = new Vector2(Random.Range(bounds.center.x-bounds.extents.x, bounds.center.x + bounds.extents.x),
                Random.Range(bounds.center.y - bounds.extents.y, bounds.center.y + bounds.extents.y));
            if (!coll.OverlapPoint(newPuddlePos))
                newPuddlePos = Vector2.zero;
        }
        Instantiate(puddlePrefab, newPuddlePos, transform.rotation, transform);
    }
}
