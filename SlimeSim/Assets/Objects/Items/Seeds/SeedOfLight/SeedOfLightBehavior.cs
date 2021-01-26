using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(GenericSeedBehavior))]

public class SeedOfLightBehavior : MonoBehaviour
{
    public Light2D light2D;
    public float maxLightRadius;

    private GenericSeedBehavior genericSeedScript;

    // Start is called before the first frame update
    void Start()
    {
        genericSeedScript = GetComponent<GenericSeedBehavior>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (genericSeedScript.isFullyGrown())
        {
            if (light2D.pointLightOuterRadius < maxLightRadius) 
            {
                light2D.pointLightOuterRadius += Time.deltaTime;
            }
        }
    }
}
