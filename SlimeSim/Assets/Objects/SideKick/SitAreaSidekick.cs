using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitAreaSidekick : MonoBehaviour
{
    public bool playerSat { get; private set; }
    public bool coinHere { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        playerSat = false;
        coinHere = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<PlayerController>().agentState == PlayerController.agentStates.Sit)
            {
                playerSat = true;
            }
            else playerSat = false;
        }

        if (collision.tag == "Coin")
        {
            coinHere = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") playerSat = false;
        if (collision.tag == "Coin") coinHere = false;
    }
}
