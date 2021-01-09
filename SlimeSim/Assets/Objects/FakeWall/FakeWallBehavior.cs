using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeWallBehavior : MonoBehaviour
{
    private enum WallState { Hiding, Speak, Absorbing, Dissapearing, Dissapear }
    private WallState wallState;
    private bool playerSat;

    // Start is called before the first frame update
    void Start()
    {
        wallState = WallState.Hiding;
    }

    // Update is called once per frame
    void Update()
    {
        switch (wallState)
        {
            case WallState.Hiding:
                {
                    if (playerSat) wallState = WallState.Speak;
                    break;
                }
            case WallState.Speak:
                {
                    break;
                }
            case WallState.Absorbing:
                {
                    break;
                }
            case WallState.Dissapearing:
                {
                    break;
                }
            case WallState.Dissapear:
                {
                    break;
                }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<PlayerController>().agentState == PlayerController.agentStates.Sit)
            {
                playerSat = true;
            }
            else playerSat = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerSat = false;
    }
}