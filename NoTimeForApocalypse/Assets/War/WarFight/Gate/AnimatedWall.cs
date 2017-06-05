using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedWall : MonoBehaviour
{

    private Animator wallAnim;
    public Collider2D blockColl;
    public string breakerTag;

    // Use this for initialization
    void Start()
    {
        wallAnim = GetComponent<Animator>();
        TagTracker.current.tagsChanged.AddListener(WallStatus);
        WallStatus();
    }

    // Update is called once per frame
    void WallStatus()
    {
        wallAnim.SetBool("broken", TagTracker.current.isTag(breakerTag));
        blockColl.enabled = !TagTracker.current.isTag(breakerTag);
    }

    public void Reset()
    {
        breakerTag = "!bossblock";
        WallStatus();
    }
}
