using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GenerateFoliage : MonoBehaviour {

    public GameObject protoScrub;
    public Vector2 amount = Vector2.one * 2;
    public Vector2 random;
    public Sprite[] sprites;

    private Collider2D coll;

    private void Awake() {
        coll = GetComponent<Collider2D>();
    }

    // Use this for initialization
    void Start() {
        Stopwatch sw = Stopwatch.StartNew();
        Generate();
        print(this + " generated scrubs in " + sw.ElapsedMilliseconds);
    }

    void Generate() {
        //delete existing children
        //for (int i = 0; i < transform.childCount; i++)
        //    Destroy(transform.GetChild(i).gameObject);
        
        for (float x = coll.bounds.center.x - coll.bounds.extents.x; x <= coll.bounds.center.x + coll.bounds.extents.x; x += coll.bounds.extents.x * 2 / (amount.x - 1)) {
            for (float y = coll.bounds.center.y + coll.bounds.extents.y; y >= coll.bounds.center.y - coll.bounds.extents.y; y -= coll.bounds.extents.y * 2 / (amount.y - 1)) {
                Vector2 newPos = new Vector2(x+Random.Range(-random.x, random.x), y + Random.Range(-random.y, random.y));
                if (coll.OverlapPoint(newPos)) {
                    GameObject newScrub = Instantiate(protoScrub, newPos, transform.rotation, transform);
                    
                    newScrub.GetComponent<SpriteRenderer>().sprite = sprites[(int)(sprites.Length * Random.value)];
                }
            }
        }
    }
}