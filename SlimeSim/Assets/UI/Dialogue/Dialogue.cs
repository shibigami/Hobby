using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject balloon;
    public Text textBox;
    public float refreshTime;
    private List<string> textQueue;
    private int stringSubSetIndex;

    // Start is called before the first frame update
    void Start()
    {
        balloon.SetActive(false);
        textBox.text = "";
        textQueue = new List<string>();

        stringSubSetIndex = -1;

        InvokeRepeating("UpdateDialogue", 0, refreshTime);
    }

    // Update is called once per frame
    void UpdateDialogue()
    {
        if (textQueue.Count > 0)
        {
            //check if not reached end of current line
            if (stringSubSetIndex < textQueue[0].Length)
            {
                //increase index to show next char
                stringSubSetIndex++;
                //set substring to text box
                textBox.text = textQueue[0].Substring(0,stringSubSetIndex);
            }

            //show dialogue balloon if its not showing yet
            if (!balloon.activeSelf) balloon.SetActive(true);
        }
    }

    public void ShowDialogue(string text)
    {
        if (!textQueue.Contains(text)) textQueue.Add(text);
    }

    public void HideDialogue()
    {
        //if its still writing
        if (stringSubSetIndex < textQueue[0].Length)
        //show everything
        {
            stringSubSetIndex = textQueue[0].Length - 1;
            textBox.text = textQueue[0];
        }
        //if its already fully written skip to next dialogue
        else
        {
            //skip to next dialogue
            textQueue.RemoveAt(0);
            //clear dialogue box
            textBox.text = "";
            //reset substring counter
            stringSubSetIndex = 0;
        }
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
