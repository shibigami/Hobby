using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPackWornBehavior : MonoBehaviour
{
    public float maxDistanceFromInitial;
    public float step;
    public float waitingTime;
    private PlayerController playerController;
    public Vector2 initialPosition;
    private bool goUp;
    private bool smallReadjust;
    private float waitTick;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        goUp = false;
        smallReadjust = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (playerController.agentState == PlayerController.agentStates.Running)
        {
            if (goUp)
            {
                if (Time.time >= waitTick)
                {
                    transform.localPosition += new Vector3(0, step * Time.deltaTime, 0);
                    if (transform.localPosition.y > initialPosition.y + maxDistanceFromInitial) goUp = false;
                }
                else
                {
                    if (smallReadjust)
                    {
                        transform.localPosition += new Vector3(0, (step * 2) * Time.deltaTime, 0);
                        smallReadjust = false;
                    }
                }
            }
            else
            {
                transform.localPosition -= new Vector3(0, step*Time.deltaTime, 0);
                if (transform.localPosition.y < initialPosition.y - maxDistanceFromInitial)
                {
                    smallReadjust = true;
                    waitTick = Time.time + waitingTime;
                    goUp = true;
                }
            }
        }
        else
        {
            transform.localPosition = initialPosition;
        }
    }
}
