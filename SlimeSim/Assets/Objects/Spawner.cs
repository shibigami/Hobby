using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Tooltip("Check for always spawning")]
    public bool loop;
    [Tooltip("If not looping will spawn this amount of times")]
    public int spawnAmount;
    private int amountSpawned;
    public float spawnTime;
    public GameObject spawnObject;
    private float spawnTick;

    private bool done;

    // Start is called before the first frame update
    void Start()
    {
        spawnTick = Time.time + spawnTime;
        amountSpawned = 0;
        done = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!done)
        {
            if (Time.time > spawnTick)
            {
                if (loop)
                {
                    Spawn();
                }
                else
                {
                    if (amountSpawned < spawnAmount)
                    {
                        Spawn();
                        amountSpawned++;
                    }
                    else done = true;
                }
            }
        }
    }

    private void Spawn()
    {
        Instantiate(spawnObject, transform.position, spawnObject.transform.rotation);
        spawnTick = Time.time + spawnTime;
    }
}
