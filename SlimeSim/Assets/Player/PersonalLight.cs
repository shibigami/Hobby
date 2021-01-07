using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalLight : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float deadzoneRadius;
    private float distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(CalculateDistanceToPlayer()>deadzoneRadius)
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, (distanceToPlayer+deadzoneRadius+speed)*Time.deltaTime);
    }

    private float CalculateDistanceToPlayer() 
    {
        distanceToPlayer = (transform.position - player.transform.position).magnitude;
        return distanceToPlayer;
    }
}
