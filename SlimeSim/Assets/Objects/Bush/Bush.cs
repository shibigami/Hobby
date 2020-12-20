using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    public GameObject connectedPoint;
    public GameObject indicator;
    public float inputDelay;
    private float inputDelayTimer;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        indicator.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        inputDelayTimer = inputDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputDelayTimer > 0) inputDelayTimer -= Time.deltaTime;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (inputDelayTimer <= 0)
        {
            if (!indicator.activeSelf)
            {
                indicator.SetActive(true);
            }
            if (Input.GetAxis("Vertical") > 0.1f) Teleport();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (indicator.activeSelf) indicator.SetActive(false);
    }

    public void Teleport()
    {
        if (connectedPoint != null && inputDelayTimer <= 0)
        {
            if (connectedPoint.GetComponent<Bush>()) connectedPoint.GetComponent<Bush>().ResetDelayTimer();
            player.transform.position = connectedPoint.transform.position;
            ResetDelayTimer();
        }
    }

    public void ResetDelayTimer() 
    {
        inputDelayTimer = inputDelay;
    }
}