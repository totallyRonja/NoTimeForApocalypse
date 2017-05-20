using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerPunch : MonoBehaviour {
	public GameObject hitPrefab;
    public GameObject hitOrigin;
	public DirectionalSprite defaultAnim;
    public DirectionalSprite hitAnim;
    public AudioSource attackAudio;

	private float hitCooldown = 0;
    private PlayerController controller;

    void Awake () {
        controller = GetComponent<PlayerController>();
        controller.punch = this;
    }
	
	// Update is called once per frame
	public void Punch () {
		hitCooldown -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && hitCooldown < 0)
        {
            GameObject closest = getClosestWithTag("Enemy");
            Vector2 direction = closest == null ? Vector2.zero : (Vector2)(closest.transform.position - hitOrigin.transform.position);
            float angle = direction == Vector2.zero ? controller.direction * Mathf.Rad2Deg : (Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg);
            GameObject newHit = Instantiate(hitPrefab, hitOrigin.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            newHit.GetComponent<HitParticle>().source = gameObject;
            hitCooldown = 0.5f;

            defaultAnim.enabled = false;
            hitAnim.angle = -angle * Mathf.Deg2Rad;
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
