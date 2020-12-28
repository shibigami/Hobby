using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject balloon;
    public Text textBox;
    private List<string> textQueue;

    // Start is called before the first frame update
    void Start()
    {
        balloon.SetActive(false);
        textBox.text = "";
        textQueue = new List<string>();

        InvokeRepeating("UpdateDialogue",0,0.25f);
    }

    // Update is called once per frame
    void UpdateDialogue()
    {
        if (textQueue.Count > 0)
        {
            if (!balloon.activeSelf) balloon.SetActive(true);
            textBox.text = textQueue[0];
        }
    }

    public void ShowDialogue(string text)
    {
        if (!textQueue.Contains(text)) textQueue.Add(text);
    }

    public void HideDialogue() 
    {
        if(textQueue.Count<=0) balloon.SetActive(false);
        else textQueue.RemoveAt(0);
    }
}
