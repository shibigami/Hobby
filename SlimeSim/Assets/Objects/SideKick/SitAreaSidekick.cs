using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitAreaSidekick : MonoBehaviour
{
    public bool playerSat { get; private set; }
    public bool coinHere { get; private set; }
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        playerSat = false;
        coinHere = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            if (player.GetComponent<PlayerController>().agentState == PlayerController.agentStates.Sit)
            {
                playerSat = true;
            }
            else playerSat = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") player = collision.gameObject;
        if (collision.tag == "Coin") coinHere = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") player = null;
        if (collision.tag == "Coin") coinHere = false;
    }
}
