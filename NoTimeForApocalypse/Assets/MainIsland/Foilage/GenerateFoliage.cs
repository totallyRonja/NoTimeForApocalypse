using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFoliage : MonoBehaviour {



    // Use this for initialization
    void Start() {
        Generate();
    }

    void Generate() {
        //delete existing children
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);


    }
}