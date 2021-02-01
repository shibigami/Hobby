using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    private GameObject player;
    public GameObject body;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(player==null) player = GameObject.FindGameObjectWithTag("Player");
        else if (player.transform.position.x < body.transform.position.x) transform.localScale=new Vector3(1,1,1);
        else if (player.transform.position.x > body.transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
    }
}