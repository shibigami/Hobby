using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : MonoBehaviour
{
    public enum AgentStates { Idle, Speaking, Joining, Dissapear, Death }
    public AgentStates agentState { get; private set; }
    public float speed;
    private Vector2 startingPos;
    private bool walkRight, spoken;

    // Start is called before the first frame update
    void Start()
    {
        if (GameData.priestJoined) gameObject.SetActive(false);

        startingPos = transform.position;
        walkRight = true;
        agentState = AgentStates.Idle;
        spoken = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (agentState)
        {
            case AgentStates.Idle:
                {
                    //travels 0.5 units to the left and 0.5 to the right of its original position
                    if (walkRight) transform.position = Vector2.MoveTowards(transform.position, startingPos +
                            new Vector2(0.5f, 0), speed * Time.deltaTime);
                    else transform.position = Vector2.MoveTowards(transform.position, startingPos -
                            new Vector2(0.5f, 0), speed * Time.deltaTime);

                    if (transform.position.x >= startingPos.x + 0.5f) walkRight = false;
                    else if (transform.position.x <= startingPos.x - 0.5f) walkRight = true;

                    break;
                }
            case AgentStates.Speaking:
                {
                    if (!spoken)
                    {
                        spoken = true;
                        StartCoroutine("Speak");
                    }
                    break;
                }
            case AgentStates.Joining:
                {
                    GameData.PriestJoins();
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().priestParticles.SetActive(true);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().priestMask.SetActive(true);
                    agentState = AgentStates.Dissapear;
                    break;
                }
            case AgentStates.Dissapear:
                {
                    if (transform.localScale.x > 0.1f) transform.localScale -= Vector3.one * Time.deltaTime * 0.1f;
                    else gameObject.SetActive(false);
                    break;
                }
            case AgentStates.Death:
                {
                    GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("No! Not the dark one!!!");
                    GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("He blinds my eye... Save...urs...f...");
                    agentState = AgentStates.Dissapear;
                    break;
                }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameData.mageJoined && agentState != AgentStates.Dissapear) agentState = AgentStates.Death;
        else if (agentState == AgentStates.Idle) agentState = AgentStates.Speaking;
    }

    private IEnumerator Speak()
    {
        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("...");
        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Light... Is this light I see, with my own eye!?");
        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("...");
        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("I will travel with you. If what travels with you is indeed Light...");
        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Our blessing shall grow together.");

        yield return new WaitForSeconds(3);

        agentState = AgentStates.Joining;
    }
}