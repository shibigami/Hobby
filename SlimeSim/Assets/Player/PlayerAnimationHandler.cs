using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator animator;
    public GameObject playerController;
    private PlayerController controller;
    public float idleTime;
    private float idleTimer;
    private float mountDelay;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = playerController.GetComponent<PlayerController>();
        idleTimer = idleTime;
        mountDelay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //flip character according to direction of movement
        if (controller.moveVector.x > 0 && transform.localScale.x == -1) transform.localScale = new Vector3(1, 1, 1);
        else if (controller.moveVector.x < 0 && transform.localScale.x == 1) transform.localScale = new Vector3(-1, 1, 1);

        animator.SetFloat("x", Mathf.Sqrt(Mathf.Pow(controller.moveVector.x, 2)));

        //set animator bools according to player controller agent states
        animator.SetBool("Jump", (controller.agentState == PlayerController.agentStates.InAir));
        animator.SetBool("Land", (controller.agentState == PlayerController.agentStates.Landing ||
        controller.agentState == PlayerController.agentStates.Iddle ||
        controller.agentState == PlayerController.agentStates.Running));
        animator.SetBool("Sit", (controller.agentState == PlayerController.agentStates.Sit));
        if (controller.agentState == PlayerController.agentStates.Mounting) mountDelay = Time.time + 0.5f;
        if(Time.time<mountDelay) animator.SetBool("Mounting", true);
        else animator.SetBool("Mounting", false);
        animator.SetBool("Mounted", controller.agentState == PlayerController.agentStates.Mounted);

        //set iddle animation timer
        if (controller.agentState == PlayerController.agentStates.Iddle) idleTimer -= Time.deltaTime;
        else
        {
            animator.SetBool("LoopIddle", false);
            idleTimer = idleTime;
        }
        if (idleTimer <= 0) animator.SetBool("LoopIddle", true);
    }
}
