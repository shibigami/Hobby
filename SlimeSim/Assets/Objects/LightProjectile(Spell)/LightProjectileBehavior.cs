using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightProjectileBehavior : MonoBehaviour
{
    public float radius { get; private set; }
    public GameObject lightObject;
    private GameObject player;

    private float angle;

    public float lifetime;
    private float lifetimeTick;

    // Start is called before the first frame update
    void Start()
    {
        CheckControllingObject();
        angle = 0;
        lifetimeTick = Time.time + lifetime;
    }

    private void CheckControllingObject()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else if (GameObject.FindGameObjectWithTag("Mount"))
        {
            player = GameObject.FindGameObjectWithTag("Mount");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null) CheckControllingObject();
        else
        {
            angle += Time.deltaTime;

            transform.position = new Vector3(player.transform.position.x + Mathf.Cos(angle) * radius,
                player.transform.position.y + Mathf.Sin(angle) * radius, 0);
        }
        if (Time.time >= lifetimeTick) Destroy(gameObject);
    }

    public void SetRadius(float radiusValue)
    {
        radius = radiusValue;
        lightObject.GetComponent<Light2D>().pointLightOuterRadius = radius;
    }
}
