using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public float speed;
    public Vector3 offset;
    private GameObject player;
    private Vector3 moveVector;
    private float zPosition;
    private float distanceModifier;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        moveVector = new Vector3();
        zPosition = transform.position.z;
        distanceModifier = 0;
    }

    public void Update()
    {
        if (((Vector2)transform.position - (Vector2)player.transform.position).magnitude > 6) distanceModifier = 0.15f;
        else distanceModifier = 0;
        moveVector = Vector3.MoveTowards(transform.position, player.transform.position + offset, (speed*Time.deltaTime)+distanceModifier);
        moveVector.z = zPosition;
        transform.position = moveVector;
    }
}
