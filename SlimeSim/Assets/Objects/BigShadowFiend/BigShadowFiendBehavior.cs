using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class BigShadowFiendBehavior : MonoBehaviour
{
    private enum AgentStates
    {
        Idle,
        Speak,
        AwaitingReply,
        Disappear
    }
    private AgentStates agentState;

    public GameObject aura;
    private Collider2D collider2d;

    public float chatDelay;
    private float chatDelayTick;

    public float pushForce;

    public enum Encounters { FirstEncounter, SecondEncounter }
    public Encounters selectEncounter;

    // Start is called before the first frame update
    void Start()
    {
        collider2d = GetComponent<Collider2D>();
        collider2d.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (agentState)
        {
            case AgentStates.Idle:
                {
                    if (aura.activeSelf) aura.SetActive(false);
                    break;
                }
            case AgentStates.Speak:
                {
                    if (!aura.activeSelf) aura.SetActive(true);

                    //first encounter dialogue
                    if (selectEncounter == Encounters.FirstEncounter)
                    {
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("...");
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("I see you've made it. It's been a while...");
                        if (GameData.mageJoined)
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("And with you... How delightful.\n" +
                                                                                                                "Hahahahaha!!!");
                        else if (GameData.priestJoined)
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("And a most annoying pest joined. I see...\n" +
                                                                                                                "But no matter.");
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("*Cough cough*");
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Pardon me, it seems I've been here for too long...");
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("I'll go, for now. But remember, I'll be watching you.\n" +
                                                                                                            "Just as most here, I'm sure.");
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Hahahahaha.");
                    }
                    //second encounter dialogue
                    else if (selectEncounter == Encounters.SecondEncounter)
                    {
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("You've made this this far.\n" +
                                                                                                           "I'm not impressed though. It was expected.");
                        if(GameData.mageJoined)
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Although, not quite yourself...");
                        else if(GameData.priestJoined)
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("And that thing still follows you...");
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Do you remember this place?\n" +
                                                                                                           "Maybe not after seeing it, but the feel of the white dust\n" +
                                                                                                           "you step on?");
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Hahaha. I wonder where it comes from.");
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("*Cough cough*");
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Already... It seems this place remembers me better\n" +
                                                                                                            "than you it. We shall meet again, just as we always have.");
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("*Cough cough*");
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("...");
                    }

                    //chat delay in case chat dissappears for a frame
                    //so that vanish animation doesn't start before chat ends
                    chatDelayTick = Time.time + chatDelay;

                    agentState = AgentStates.AwaitingReply;
                    break;
                }
            case AgentStates.AwaitingReply:
                {
                    if (!GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().IsChatShowing() && Time.time > chatDelayTick)
                        agentState = AgentStates.Disappear;
                    break;
                }
            case AgentStates.Disappear:
                {
                    transform.position = Vector3.MoveTowards(transform.position,
                        new Vector3(transform.position.x, 200, 0), 1 * Time.deltaTime);
                    break;
                }
        }
    }

    private void PushForce(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            collision.gameObject.GetComponent<PlayerController>().Dismount();
        }
        Rigidbody2D colliderRB2D = collision.gameObject.GetComponent<Rigidbody2D>();

        if (colliderRB2D != null)
        {
            colliderRB2D.velocity += (Vector2)((collision.transform.position - transform.position).normalized * pushForce * Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PushForce(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PushForce(collision);
        if (collision.tag == "Player" && agentState == AgentStates.Idle)
        {
            agentState = AgentStates.Speak;
        }
    }
}