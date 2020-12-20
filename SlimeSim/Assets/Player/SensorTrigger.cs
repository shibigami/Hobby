using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class SensorTrigger : MonoBehaviour
{
    public bool triggered { get; private set; }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground") triggered = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground") triggered = false;
    }
}
