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

    int tickCounter = 0;

    GameObject[,] existingObjects;
    Vector2 atomSize; //distance between objects
    Vector2 startIndex = Vector2.zero;
    Vector2 indexPos;

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
            UpdateObjects();
        }
        tickCounter++;
    }

    void Generate() {
        existingObjects = new GameObject[Mathf.FloorToInt((cameraHalfExtents.x * 2 + offset_size.width) * amount.x / coll.bounds.size.x), 
                                        Mathf.FloorToInt((cameraHalfExtents.y * 2 + offset_size.height) * amount.y / coll.bounds.size.y)];
        
        indexPos = new Vector2(Mathf.Floor((camera.position.x - cameraHalfExtents.x - offset_size.x - coll.bounds.min.x) * amount.x / coll.bounds.size.x),
                Mathf.Floor((camera.position.y - cameraHalfExtents.y - offset_size.y - coll.bounds.min.y) * amount.y / coll.bounds.size.y));

        atomSize = new Vector2(coll.bounds.size.x / amount.x, coll.bounds.size.y / amount.y);

        for (int x = 0; x < existingObjects.GetLength(0); x++) {
            for (int y = 0; y < existingObjects.GetLength(1); y++) {
                Vector2 indexBasePos = new Vector2(indexPos.x + x, indexPos.y + y);
                Vector2 basePos = new Vector2(indexBasePos.x * coll.bounds.size.x / amount.x + coll.bounds.min.x,
                        indexBasePos.y * coll.bounds.size.y / amount.y + coll.bounds.min.x);

                existingObjects[x, y] = Instantiate(protoScrub, basePos, transform.rotation,transform);
            }
        }
        {//so I can hide the comment
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
    }

    void UpdateObjects() {
        Vector2 currentIndexPos = new Vector2(Mathf.Floor((camera.position.x - cameraHalfExtents.x - offset_size.x - coll.bounds.min.x) * amount.x / coll.bounds.size.x),
                Mathf.Floor((camera.position.y - cameraHalfExtents.y - offset_size.y - coll.bounds.min.y) * amount.y / coll.bounds.size.y));
        print("saved: " + indexPos + " | current: " + currentIndexPos);
        if(currentIndexPos.x > indexPos.x) {
            for(int y=0;y<existingObjects.GetLength(1); y++) {
                existingObjects[(int)startIndex.x, y].transform.position += Vector3.right * atomSize.x * existingObjects.GetLength(0);
            }
            startIndex.x = ((startIndex.x + 1) % existingObjects.GetLength(0) + existingObjects.GetLength(0)) % existingObjects.GetLength(0);
            indexPos.x++;
        }
        if (currentIndexPos.x < indexPos.x) {
            for (int y = 0; y < existingObjects.GetLength(1); y++) {
                existingObjects[iAdd((int)startIndex.x, -1, existingObjects.GetLength(0)), y].transform.position -= Vector3.right * atomSize.x * existingObjects.GetLength(0);
            }
            startIndex.x = ((startIndex.x - 1) % existingObjects.GetLength(0) + existingObjects.GetLength(0)) % existingObjects.GetLength(0);
            indexPos.x--;
        }

        if (currentIndexPos.y > indexPos.y) {
            for (int x = 0; x < existingObjects.GetLength(0); x++) {
                existingObjects[x, (int)startIndex.y].transform.position += Vector3.up * atomSize.y * existingObjects.GetLength(1);
            }
            startIndex.y = ((startIndex.y + 1) % existingObjects.GetLength(1) + existingObjects.GetLength(1)) % existingObjects.GetLength(1);
            indexPos.y++;
        }
        if (currentIndexPos.y < indexPos.y) {
            for (int x = 0; x < existingObjects.GetLength(0); x++) {
                existingObjects[x, iAdd((int)startIndex.y, -1, existingObjects.GetLength(1))].transform.position -= Vector3.up * atomSize.y * existingObjects.GetLength(1);
            }
            startIndex.y = iAdd((int)startIndex.y, -1, existingObjects.GetLength(1));
            indexPos.y--;
        }
    }

    int iAdd(int index, int summand, int max) {
        return ((index + summand) % max + max) % max;
    }

    /*void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        foreach (Vector2 pos in existingObjects.Keys) {
            Gizmos.DrawSphere(pos, 0.5f);
            //print(pos);
        }
    }*/
}