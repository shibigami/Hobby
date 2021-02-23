using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPackCharBehavior : MonoBehaviour
{
    private enum AgentStates 
    {
        Speaking,
        AwaitingReply,
        Joining,
        Joined
    }
    private AgentStates agentState;

    private PlayerController playerController;
    private bool playerColliding;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerColliding = false;
        agentState = AgentStates.Speaking;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (agentState)
        {
            case AgentStates.Speaking:
                {
                    GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("You! Over here!");
                    GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Don't think I can't see you there, see!");
                    GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("I'll make you a proposition you can't refuse, kid.\n" +
                                                                                                        "Take me with you and I'll make it worth your while.");
                    GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("But in turn, you gotta help me out, see!\n" +
                                                                                                        "I can't tell you the details, but you scratch my backpack\n" +
                                                                                                        "and I'll scratch your back, pal.");

                    agentState = AgentStates.AwaitingReply;

                    break;
                }
            case AgentStates.AwaitingReply:
                {
                    if (playerColliding && playerController.agentState == PlayerController.agentStates.Sit)
                        agentState = AgentStates.Joining;
                    break;
                }
            case AgentStates.Joining:
                {
                    GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("That's it, nice and steady.");
                    GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("This is gonna be good, kid. We both know it.");
                    GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("I'll just keep your things with me, yeah see?\n" +
                                                                                                        "And I'll just throw them on the floor when you need them!");
                    GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Now come on, kid. Time to get the pack out'a here!");

                    agentState = AgentStates.Joined;

                    break;
                }
            case AgentStates.Joined:
                {
                    GameData.UnlockInventory();
                    playerController.backPack.SetActive(true);

                    Destroy(gameObject);

                    break;
                }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") playerColliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") playerColliding = false;
    }
}
