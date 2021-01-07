using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    public float speed;

    private GameObject player;

    private float lifeTimer;

    private bool die;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<PlayerController>().Mount(gameObject,speed);

        die = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!die)
        {
            lifeTimer -= Time.deltaTime;
            if (lifeTimer <= 0) StartCoroutine("Die");
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
        lifeTimer = timeInSeconds;
    }
}
