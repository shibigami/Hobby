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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        moveVector = new Vector3();
        zPosition = transform.position.z;
    }

    public void Update()
    {
        moveVector = Vector3.MoveTowards(transform.position, player.transform.position + offset, speed*Time.deltaTime);
        moveVector.z = zPosition;
        transform.position = moveVector;
    }
}
