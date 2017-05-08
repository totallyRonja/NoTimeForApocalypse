using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GenerateFoliage : MonoBehaviour {

    public GameObject protoScrub;
    public Vector2 amount = Vector2.one * 2;
    public Vector2 random;
    public Sprite[] sprites;

    public Rect offset_size; //offset AND size

    private Collider2D coll;

    new Transform camera;
    Vector2 cameraHalfExtents;

    Rect indexBounds;

    int tickCounter = 0;

    Dictionary<Vector2, GameObject> existingObjects = new Dictionary<Vector2, GameObject>();

    private void Awake() {
        coll = GetComponent<Collider2D>();
    }

    // Use this for initialization
    void Start() {
        camera = Camera.main.transform;
        cameraHalfExtents = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);

        

        //Stopwatch sw = Stopwatch.StartNew();
        Generate();
        //print(this + " generated scrubs in " + sw.ElapsedMilliseconds);
    }

    private void Update() {
        if (tickCounter % 6 == 0) {
            Generate();
        }
        tickCounter++;
    }

    void Generate() {
        existingObjects.Clear();
        int i = 0;

        Vector2 indexOriginPos = new Vector2((camera.position.x - cameraHalfExtents.x - offset_size.x - coll.bounds.min.x) * amount.x / coll.bounds.size.x,
                (camera.position.y - cameraHalfExtents.y - offset_size.y - coll.bounds.min.y) * amount.y / coll.bounds.size.y);
        indexBounds = new Rect(Mathf.Floor(indexOriginPos.x), Mathf.Floor(indexOriginPos.y),
                Mathf.Floor((cameraHalfExtents.x * offset_size.width) * amount.x / coll.bounds.size.x), 
                Mathf.Floor((cameraHalfExtents.y * offset_size.height) * amount.y / coll.bounds.size.x));


        for(int x = 0; x < indexBounds.width; x++) {
            for (int y = 0; y < indexBounds.height; y++) {
                Vector2 indexBasePos = new Vector2(indexBounds.x + x, indexBounds.y + y);
                Vector2 basePos = new Vector2(indexBasePos.x * coll.bounds.size.x / amount.x + coll.bounds.min.x,
                        indexBasePos.y * coll.bounds.size.y / amount.y + coll.bounds.min.x);

                existingObjects[basePos] = null;

                i++;
            }
        }
        //print(i);

        /*//delete existing children
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
        }*/
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        foreach (Vector2 pos in existingObjects.Keys) {
            Gizmos.DrawSphere(pos, 0.5f);
            //print(pos);
        }
    }
}