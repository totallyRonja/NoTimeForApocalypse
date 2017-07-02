using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGridManager : MonoBehaviour {

    Transform player;
    AudioGridNode[] currentSoundNodes;
    AudioSource[] sources;
    AudioGridNode[] nodes;

    void Start () {
        sources = GetComponents<AudioSource>();
        nodes = GetComponentsInChildren<AudioGridNode>();

        player = PlayerHP.current.transform;
		currentSoundNodes = getNearestSoundNodes(3);
    }
	
	// Update is called once per frame
	void Update () {
        float[] bary = BarycentricCoordinates(player.position, currentSoundNodes[0].transform.position, currentSoundNodes[1].transform.position, currentSoundNodes[2].transform.position);
        if (!IsInTriangle(bary)){
			int mostIrrelevantCorner = -1;
            do{
                foreach (AudioGridNode t in currentSoundNodes)
                    t.gameObject.SetActive(false);
                AudioGridNode newCorner = getNearestSoundNodes(1)[0];
                mostIrrelevantCorner = -1;
                for (int i = 0; i < currentSoundNodes.Length; i++)
                {
                    if (mostIrrelevantCorner < 0 || bary[i] < bary[mostIrrelevantCorner])
                    {
                        mostIrrelevantCorner = i;
                    }
                }
                currentSoundNodes[mostIrrelevantCorner] = newCorner;
                if(currentSoundNodes[mostIrrelevantCorner] == null){
					foreach (AudioGridNode t in nodes)
                        t.gameObject.SetActive(true);
					currentSoundNodes = getNearestSoundNodes(3);
					bary = BarycentricCoordinates(player.position, currentSoundNodes[0].transform.position, currentSoundNodes[1].transform.position, currentSoundNodes[2].transform.position);
                    break;
                }
                bary = BarycentricCoordinates(player.position, currentSoundNodes[0].transform.position, currentSoundNodes[1].transform.position, currentSoundNodes[2].transform.position);
            } while (!IsInTriangle(bary));

            foreach (AudioGridNode t in nodes)
                t.gameObject.SetActive(true);

			sources[mostIrrelevantCorner].clip = currentSoundNodes[mostIrrelevantCorner].clip;
			for (int j = 0; j < 3;j++){
				if(sources[mostIrrelevantCorner].clip == sources[j].clip && j != mostIrrelevantCorner){
					sources[mostIrrelevantCorner].timeSamples = sources[j].timeSamples;
					sources[mostIrrelevantCorner].Play();
				}
			}
			if (!sources[mostIrrelevantCorner].isPlaying)
				sources[mostIrrelevantCorner].Play();
        }
        for (int i = 0; i < 3; i++)
		{
            sources[i].volume = Mathf.Clamp01(bary[i] * currentSoundNodes[i].volume);

            Debug.DrawLine(player.position, currentSoundNodes[i].transform.position, Color.red);
			Debug.DrawLine(currentSoundNodes[i].transform.position, currentSoundNodes[(i + 1) % 3].transform.position, Color.green);
		}
    }

	public float[] BarycentricCoordinates(Vector2 location, Vector2 p1, Vector2 p2, Vector2 p3){
        Vector2 v0 = p2 - p1, v1 = p3 - p1, v2 = location - p1;
        float den = v0.x * v1.y - v1.x * v0.y;
        float[] bary = new float[3];
        bary[1] = (v2.x * v1.y - v1.x * v2.y) / den;
        bary[2] = (v0.x * v2.y - v2.x * v0.y) / den;
        bary[0] = 1.0f - bary[1] - bary[2];
        return bary;
    }

	public AudioGridNode[] getNearestSoundNodes(int amount){
        AudioGridNode[] nearest = new AudioGridNode[amount];
		if(amount == 0) return nearest;
		float minSqrDistance = -1;
        for (int i = 0; i < amount;i++){
            minSqrDistance = -1;
			foreach(AudioGridNode soundNode in nodes){
				if(!soundNode.gameObject.activeSelf)
                    continue;
                float sqrDistance = Vector3.SqrMagnitude(soundNode.transform.position - player.position);
                if(nearest[i] == null || sqrDistance <= minSqrDistance){
                    bool existing = false;
                    foreach (AudioGridNode t in nearest)
                        if (t == soundNode)
                            existing = true;
                    if (existing) continue;
                    nearest[i] = soundNode;
					minSqrDistance = sqrDistance;
                }
			}
        }
        return nearest;
    }

	public bool IsInTriangle(Vector2 location, Vector2 p1, Vector2 p2, Vector2 p3){
        float[] bary = BarycentricCoordinates(location, p1, p2, p3);
		foreach(float f in bary){
            if(f<0)
                return false;
        }
        return true;
    }
	public bool IsInTriangle(float[] bary)
    {
        foreach (float f in bary){
            if (f < 0)
                return false;
        }
        return true;
    }
}
