using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerWalk))]
public class PlayerPunch : MonoBehaviour {
	public GameObject hitPrefab;
    public GameObject hitOrigin;
	public DirectionalSprite defaultAnim;
    public DirectionalSprite hitAnim;
    public AudioSource attackAudio;

	private float hitCooldown = 0;
    private PlayerWalk walk;

    void Awake () {
        walk = GetComponent<PlayerWalk>();
    }

    void Update(){
        Punch();
    }
	
	// Update is called once per frame
	public void Punch () {
		hitCooldown -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && hitCooldown < 0)
        {
            GameObject closest = getClosestWithTag("Enemy");
            Vector2 direction = closest == null ? walk.direction : (Vector2)(closest.transform.position - hitOrigin.transform.position);
            GameObject newHit = Instantiate(hitPrefab, hitOrigin.transform.position, Quaternion.AngleAxis(Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg, Vector3.forward));
            newHit.GetComponent<HitParticle>().source = gameObject;
            hitCooldown = 0.5f;

            defaultAnim.enabled = false;
            hitAnim.angle = Mathf.Atan2(direction.y, direction.x);
            hitAnim.enabled = true;

            attackAudio.pitch = UnityEngine.Random.Range(0.9f, 1.3f);
            attackAudio.Play();
        }
	}

	public GameObject getClosestWithTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float distance = -1;
        foreach (GameObject go in objectsWithTag)
        {
            if (closest == null || (go.transform.position - transform.position).magnitude < distance)
            {
                closest = go;
                distance = (go.transform.position - transform.position).magnitude;
            }
        }
        return distance < 20 ? closest : null;
    }
}
