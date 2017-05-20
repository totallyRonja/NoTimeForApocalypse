using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour {

	public float speed;
	public float duration;
    public AnimationCurve dynamic;

    private PlayerPhysics phys;
	private PlayerController controller;

	// Use this for initialization
	void Awake(){
        phys = GetComponent<PlayerPhysics>();
        controller = GetComponent<PlayerController>();
        controller.dash = this;
    }
	
	// Update is called once per frame
	public void Dash(){
        if (Input.GetButtonDown("Action")){
            StartCoroutine("DashAction");
        }
    }
    private IEnumerator DashAction() {
        enabled = false;
        float timer = 0;
        Vector2 direction = new Vector2(-Mathf.Sin(controller.direction), Mathf.Cos(controller.direction));
        while (timer < duration){
            phys.velocity = direction * dynamic.Evaluate(timer / duration) * speed;
            phys.velocityGoal = phys.velocity;
            timer += Time.deltaTime;
            yield return null;
        }
        phys.velocity = Vector2.zero;
        enabled = true;
    }
}
