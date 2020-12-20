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
        Awake
    }

    public agentStates agentState { get; private set; }
    private Rigidbody2D rb2d;
    private GameObject player;
    public float AwakeTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.bodyType = RigidbodyType2D.Static;
        agentState = agentStates.Asleep;
        player = GameObject.FindGameObjectWithTag("Player");
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
                    break;
                }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") collision.gameObject.GetComponent<PlayerController>().Die();
    }
}
