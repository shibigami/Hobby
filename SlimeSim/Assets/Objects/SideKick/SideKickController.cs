using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class SideKickController : MonoBehaviour
{
    public enum agentStates 
    {
        Asleep,
        Awake,
        Helped,
        Joined,
        MoveBack
    }

    public agentStates agentState { get; private set; }
    private Rigidbody2D rb2d;
    private GameObject player;
    public float AwakeTimer;
    public GameObject sitAreaObject;
    private SitAreaSidekick sitArea;
    private bool pickedUpCoin;
    private float pickUpTimer;
    private int coinsPicked;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.bodyType = RigidbodyType2D.Static;
        agentState = agentStates.Asleep;
        player = GameObject.FindGameObjectWithTag("Player");
        sitArea = sitAreaObject.GetComponent<SitAreaSidekick>();

        pickedUpCoin = false;
        coinsPicked = 0;
        ResetPickUpTimer();
    }

    private void ResetPickUpTimer() 
    {
        pickUpTimer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (agentState) 
        {
            case agentStates.Asleep: 
                {
                    if (rb2d.bodyType == RigidbodyType2D.Static &&
                        (player.transform.position.x > transform.position.x - transform.localScale.x / 2 &&
                        player.transform.position.x < transform.position.x + transform.localScale.x / 2))
                        rb2d.bodyType = RigidbodyType2D.Dynamic;

                    if (rb2d.bodyType == RigidbodyType2D.Dynamic) AwakeTimer -= Time.deltaTime;
                    if (AwakeTimer <= 0) agentState = agentStates.Awake;
                    break;
                }
            case agentStates.Awake:
                {
                    if (sitArea.playerSat)
                    {
                        GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Give me Shiny and I will let you pass.");
                        if (!GameObject.FindGameObjectWithTag("UI").GetComponent<Outputs>().dropGoldUnlocked) GameObject.FindGameObjectWithTag("UI").GetComponent<Outputs>().UnlockDropGold();
                    }

                    if (sitArea.coinHere)
                    {
                        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2);
                        foreach (Collider2D col in colliders)
                        {
                            if (col.tag == "Coin")
                            {
                                col.gameObject.transform.position = Vector2.MoveTowards(col.transform.position,
                                    transform.position, 2 * Time.deltaTime);
                            }
                        }
                    }

                    //starts timer for picking up coins before disappearing
                    if (pickedUpCoin)
                    {
                        pickUpTimer -= Time.deltaTime;
                        if (pickUpTimer < 0)
                        {
                            if (coinsPicked > 5) agentState = agentStates.Joined;
                            else agentState = agentStates.Helped;
                        }
                    }
                    break;
                }
            case agentStates.Helped:
                {
                    rb2d.bodyType = RigidbodyType2D.Static;
                    GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Yes, this Shiny.\nI happy for now. Hm...");
                    agentState = agentStates.MoveBack;
                    break;
                }
            case agentStates.Joined:
                {
                    rb2d.bodyType = RigidbodyType2D.Static;
                    GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("That's many shiny... I go with you.\nFind more Shiny. Then, we Shiny together. Yes, hm...");
                    GameData.SideKickJoins();
                    agentState = agentStates.MoveBack;
                    break;
                }
            case agentStates.MoveBack: 
                {
                    transform.position = Vector2.MoveTowards((Vector2)transform.position, new Vector2(transform.position.x, 5), 1 * Time.deltaTime);
                    break;
                }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") collision.gameObject.GetComponent<PlayerController>().Die();

        if (collision.gameObject.tag == "Coin"&&!collision.gameObject.GetComponent<Coin>().picked)
        {
            collision.gameObject.GetComponent<Coin>().PickUp();
            coinsPicked++;
            ResetPickUpTimer();
            pickedUpCoin = true;
        }
    }
}
