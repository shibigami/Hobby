using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Outputs : MonoBehaviour
{
    public Text goldText;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        InvokeRepeating("UpdateUI", 0, 0.5f);
    }

    void UpdateUI()
    {
        goldText.text = GameData.gold.ToString();
    }
}
