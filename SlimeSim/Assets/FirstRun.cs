using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstRun : MonoBehaviour
{
    public GameObject dontDestroyOnLoadObjects;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.FindGameObjectWithTag("Player")) Instantiate(dontDestroyOnLoadObjects);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
