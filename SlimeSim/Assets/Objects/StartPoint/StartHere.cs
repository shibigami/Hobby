using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHere : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
            GameObject.FindGameObjectWithTag("Player").transform.rotation = new Quaternion(0, 0, 0, 0);

            GameObject.FindGameObjectWithTag("UIActiveSpells").GetComponent<UISpellBehaviors>().OnSceneChanged();
            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ClearDialogue();
        }
        catch 
        {
            Debug.Log("Player not present in Scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
