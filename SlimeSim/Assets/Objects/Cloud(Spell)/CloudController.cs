using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    public float speed;

    private GameObject player;

    private float deathTime;

    private bool die;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<PlayerController>().Mount(gameObject, speed);

        die = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!die)
        {
            if (Time.time > deathTime) StartCoroutine("Die");
        }
    }

    private IEnumerator Die()
    {
        die = true;

        player.GetComponent<PlayerController>().Dismount();

        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }

    public void SetLifeTimer(int timeInSeconds)
    {
        deathTime = Time.time + timeInSeconds;
    }
}