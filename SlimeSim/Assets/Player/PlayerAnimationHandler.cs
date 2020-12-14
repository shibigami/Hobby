using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator animator;
    public GameObject playerController;
    private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = playerController.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.moveVector.x > 0 && transform.localScale.x == -1) transform.localScale = Vector3.one;
        else if (controller.moveVector.x < 0 && transform.localScale.x == 1) transform.localScale = new Vector3(-1, 1, 1);

        animator.SetFloat("x", Mathf.Sqrt(Mathf.Pow(controller.moveVector.x, 2)));

        if (controller.agentState == PlayerController.agentStates.Jumping) animator.SetBool("Jump", true);
        else animator.SetBool("Jump", false);
        if (controller.agentState == PlayerController.agentStates.Landing || 
            controller.agentState == PlayerController.agentStates.Iddle || 
            controller.agentState == PlayerController.agentStates.Running) 
            animator.SetBool("Land",true);
        else animator.SetBool("Land", false);
    }
}
