using System.Collections.Generic;
using UnityEngine;

public class AppleTree : Hitable {

    public GameObject apple;

    Dictionary<Transform, float> appleSpeed = new Dictionary<Transform, float>();

    public override void Hit(GameObject source, float damage = 0, float directionAngle = 0) {
        Vector3 origin = transform.GetChild(UnityEngine.Random.Range(0, transform.childCount)).localPosition;
        GameObject newApple = Instantiate(apple, transform.position + Vector3.down * 0.1f + Vector3.right * origin.x, transform.rotation, transform.parent);
        newApple.transform.GetChild(0).localPosition = Vector3.up * origin.y;
        appleSpeed[newApple.transform.GetChild(0)] = 0;
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        List<Transform> keys = new List<Transform>(appleSpeed.Keys);
		foreach(Transform apple in keys) {
            if (apple == null || apple.transform.localPosition.y < 0) {
                appleSpeed.Remove(apple);
                continue;
            }

            appleSpeed[apple] -= Time.deltaTime;
            apple.transform.localPosition -= Vector3.down * appleSpeed[apple];
        }
	}
}
