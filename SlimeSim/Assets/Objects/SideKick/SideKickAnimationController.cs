using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideKickAnimationController : MonoBehaviour
{
    public GameObject sideKickControllerObject;
    private SideKickController controller;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = sideKickControllerObject.GetComponent<SideKickController>();
        animator = GetComponent<Animator>();

        InvokeRepeating("UpdateAnimation",0,0.5f);
    }

    // Update is called once per frame
    public void UpdateAnimation()
    {
        if (controller.agentState == SideKickController.agentStates.Awake) animator.SetBool("Awake", true);
    }
}
