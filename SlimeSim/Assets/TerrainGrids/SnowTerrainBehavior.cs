using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowTerrainBehavior : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    private bool playerOnStage;

    public GameObject snowParticlesFeetObject;
    private ParticleSystem snowParticles;
    public GameObject snowParticlesCameraObject;
    public Vector3 offSet;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerOnStage = false;

        snowParticles = snowParticlesFeetObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerOnStage && playerController.agentState == PlayerController.agentStates.Running)
        {
            player.GetComponent<Rigidbody2D>().velocity *= new Vector2(0.5f, 1);

            if (snowParticles.isStopped) snowParticles.Play();

            snowParticles.transform.position = player.transform.position + offSet;
        }
        else
        {
            if (snowParticles.isPlaying) snowParticles.Stop();
        }
        snowParticlesCameraObject.transform.position = Camera.main.transform.position + new Vector3(0,4.5f,-Camera.main.transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player) playerOnStage = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == player) playerOnStage = false;
    }
}
