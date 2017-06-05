using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerWalk : MonoBehaviour {

	public float walkSpeed = 1;
    public AudioSource walkAudio;
    public AudioClip defaultWalk;
    public AudioClip mudWalk;
	[NonSerialized] public bool slowed = false;
    [NonSerialized] public Vector2 direction;
    
	private PlayerPhysics phys;
    //private PlayerController controller;
    void Awake () {
        //controller = GetComponent<PlayerController>();
        //controller.walk = this;

        phys = GetComponent<PlayerPhysics>();
    }

    void Update(){
        Walk();
    }

	public void Walk() {
        if(Time.timeScale <= 0){
            direction = Vector2.zero;
            walkAudio.pitch = 0;
            return;
        } else {
            walkAudio.pitch = 1;
        }


        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (input.magnitude > 1)
            input.Normalize();
        input.Scale(new Vector2(1, 2f / 3f));
        Vector2 velocity = input * walkSpeed;
        if (Input.GetButton("God")) velocity *= 10; // speed cheat
        phys.velocityGoal = slowed ? velocity * 0.4f : velocity;

        walkAudio.mute = velocity == Vector2.zero;
        if (velocity != Vector2.zero){ //this used to drive the hit direction
            direction = velocity.normalized;
        }

        if (slowed && walkAudio.clip != mudWalk)
        {
            int pos = walkAudio.timeSamples;
            try
            {
                walkAudio.clip = mudWalk;
                walkAudio.timeSamples = pos;
                walkAudio.Play();
            }
            catch
            {
                walkAudio.time = 0;
                walkAudio.Play();
            }
        }
        if (!slowed && walkAudio.clip != defaultWalk)
        {
            int pos = walkAudio.timeSamples;
            try
            {
                walkAudio.clip = defaultWalk;
                walkAudio.timeSamples = pos;
                walkAudio.Play();
            }
            catch
            {
                walkAudio.time = 0;
                walkAudio.Play();
            }
        }
    }

	private void FixedUpdate()
    {
        slowed = false;
    }

	private void OnDisable()
    {
        phys.velocity = Vector2.zero;
        phys.velocityGoal = Vector2.zero;
        walkAudio.mute = true;
    }
}
