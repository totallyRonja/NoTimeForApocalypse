using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    [SerializeField] AnimationCurve spikeCurve;
    [SerializeField] float shiftScale = 0.2f;
	[SerializeField] float duration;
    [SerializeField] float relativeDamageTime = 0.5f;
    Collider2D hurtbox;
    Collider2D playerCollider;
    PlayerHP playerHp;
    Material mat;
    SpriteRenderer render;


    void Start () {
        hurtbox = GetComponent<Collider2D>();
        playerCollider = PlayerPhysics.current.GetComponent<Collider2D>();
        playerHp = playerCollider.GetComponent<PlayerHP>();
        render = GetComponent<SpriteRenderer>();
        mat = render.material;

        StartCoroutine(Spike());
    }
	
	IEnumerator Spike(){
        render.color = new Color(.2f, 0, 0, .2f);
        yield return new WaitForSeconds(1);
        render.color = Color.white;
        float startTime = Time.time;
        float time = startTime;
        bool dealtDamage = false;
        while (time < startTime + duration)
        {
            if (!dealtDamage && time > startTime + (duration * relativeDamageTime)){
                DealDamage();
                dealtDamage = true;
            }
            mat.SetFloat("_Shift", spikeCurve.Evaluate((time - startTime) / duration) * shiftScale);
            yield return null;
            time = Time.time;
        }
        Destroy(gameObject);
    }

	void DealDamage(){
		if(!hurtbox.IsTouching(playerCollider))
            return;
        playerHp.Hit(gameObject, 2, Vector2.zero);
    }
}
