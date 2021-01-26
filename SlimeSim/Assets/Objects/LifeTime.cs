using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    public float lifeTime;
    private float lifeTick;

    // Start is called before the first frame update
    void Start()
    {
        lifeTick = Time.time + lifeTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lifeTick < Time.time) Destroy(gameObject);
    }
}
