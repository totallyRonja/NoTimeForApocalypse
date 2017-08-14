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
        Yarn.Unity.DialogueRunner.current.SwitchNode.AddListener(WallStatus);
        WallStatus();
    }

    // Update is called once per frame
    void WallStatus()
    {
        wallAnim.SetBool("broken", ExampleVariableStorage.current.IsTag(breakerTag));
        blockColl.enabled = !ExampleVariableStorage.current.IsTag(breakerTag);
    }

    public void Reset()
    {
        breakerTag = "!bossblock";
        WallStatus();
    }
}
