using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHere : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            try
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
                GameObject.FindGameObjectWithTag("Player").transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            catch
            {
                Debug.Log("Player not present in Scene.");
            }
            try
            {
                GameObject.FindGameObjectWithTag("UIActiveSpells").GetComponent<UISpellBehaviors>().OnSceneChanged();
            }
            catch
            {
                Debug.Log("UIActiveSpells not present in Scene");
            }
            try
            {
                GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ClearDialogue();
            }
            catch
            {
                Debug.Log("Dialogue UI error.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
