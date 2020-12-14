using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public float force;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,force));
        }
    }
}
