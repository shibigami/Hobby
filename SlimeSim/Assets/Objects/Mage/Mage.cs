using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Mage : MonoBehaviour
{
    private Vector2 initialPos;
    private bool ascend;
    private float maxY, minY;
    private float speed;

    private enum AgentStates
    {
        Iddle,
        Talk,
        WaitForResponse,
        Join
    }
    private AgentStates agentState;

    public float absorbSpeed;
    private int coinsAbsorbed;

    // Start is called before the first frame update
    void Start()
    {
        if (GameData.mageJoined) gameObject.SetActive(false);

        initialPos = (Vector2)transform.position;
        ascend = true;
        maxY = initialPos.y + 0.1f;
        minY = initialPos.y - 0.1f;
        speed = 0.1f;

        GetComponent<Collider2D>().isTrigger = true;

        agentState = AgentStates.Iddle;
        coinsAbsorbed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameData.mageJoined)
        {
            if (ascend) transform.position = new Vector3(initialPos.x,
                  transform.position.y + speed * Time.deltaTime,
                  0);
            else transform.position = new Vector3(initialPos.x,
                transform.position.y - speed * Time.deltaTime,
                0);

            if (transform.position.y >= maxY) ascend = false;
            else if (transform.position.y <= minY) ascend = true;
        }

        switch (agentState)
        {
            case AgentStates.Iddle:
                {
                    break;
                }
            case AgentStates.Talk:
                {
                    GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("KeKeKe. Power. Give me Power!!");
                    GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("KeKeKe. Throw it on the floor and let me take it!");
                    GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("KeKeKeKwakekeke.");
                    agentState = AgentStates.WaitForResponse;
                    break;
                }
            case AgentStates.WaitForResponse:
                {
                    if (coinsAbsorbed >= 10)
                    {
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Yes!! So much POWER!!!");
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Kekekeke! I will grant you part of my power if you give me more!!");
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("I'll follow you. For now...");
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("KeKeKeKwakekeke.");

                        agentState = AgentStates.Join;
                    }
                    break;
                }
            case AgentStates.Join:
                {
                    if (transform.localScale.x > 0.001f) transform.localScale -= new Vector3(0.1f * Time.deltaTime, 0.1f * Time.deltaTime, 0.1f * Time.deltaTime);
                    if (!GameData.mageJoined)
                    {
                        GameData.MageJoins();
                        Journal.UpdateMagePriestBranch();
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().mageParticles.SetActive(true);
                    }
                    break;
                }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && agentState != AgentStates.Join)
        {
            agentState = AgentStates.Talk;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && agentState != AgentStates.Join)
        {
            agentState = AgentStates.Iddle;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Coin" && agentState == AgentStates.WaitForResponse)
        {
            collision.transform.position = Vector2.MoveTowards(collision.transform.position, transform.position, absorbSpeed * Time.deltaTime);
            if ((collision.transform.position - transform.position).magnitude < 0.5f)
            {
                if (!collision.GetComponent<Coin>().picked)
                {
                    coinsAbsorbed++;
                }
                collision.GetComponent<Coin>().PickUp();
            }
        }
    }
}