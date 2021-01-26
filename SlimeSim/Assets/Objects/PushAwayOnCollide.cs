using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PushAwayOnCollide : MonoBehaviour
{
    public Vector2 pushForce;
    private Vector2 directionForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && collision.gameObject.GetComponent<Rigidbody2D>())
        {
            if (GetComponent<Rigidbody2D>().velocity.magnitude > 1.5f)
            {
                directionForce = (transform.position - collision.transform.position).normalized;
                directionForce.y = 1;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity += pushForce * directionForce;
            }
        }
    }
}
