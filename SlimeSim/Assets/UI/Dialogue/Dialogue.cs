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

        InvokeRepeating("UpdateDialogue", 0, 0.3f);
    }

    // Update is called once per frame
    void UpdateDialogue()
    {
        if (textQueue.Count > 0)
        {
            textBox.text = textQueue[0];
            if (!balloon.activeSelf) balloon.SetActive(true);
        }
    }

    public void ShowDialogue(string text)
    {
        if (!textQueue.Contains(text)) textQueue.Add(text);
    }

    public void HideDialogue()
    {
        textQueue.RemoveAt(0);
        if (textQueue.Count <= 0) balloon.SetActive(false);
    }

    public bool IsChatShowing()
    {
        if (textQueue.Count > 0) return true;
        else return false;
    }

    public void ClearDialogue()
    {
        if (textQueue!=null && textQueue.Count > 0) textQueue.Clear();
        if (balloon.activeSelf) balloon.SetActive(false);
    }
}
