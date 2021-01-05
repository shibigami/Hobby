using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISpellBehaviors : MonoBehaviour
{
    private GameObject player;
    private int backToStartUseCounter;

    public GameObject backToStartButton, createPlatformButton,lightPlatform,darkPlatform;

    // Start is called before the first frame update
    void Start()
    {
        ResetBackToStartCounter();
        UpdateMagicUnlockedButtons();

        player = GameObject.FindGameObjectWithTag("Player");

        InvokeRepeating("UpdateCooldowns", 0, 0.05f);
    }

    private void UpdateCooldowns()
    {
        if (Magic.magicTypeChosen == Magic.MagicType.Mage)
        {
            backToStartButton.GetComponent<Image>().fillAmount = 1 -
                (Magic.mageSpells[3].GetCurrentCooldown() / Magic.mageSpells[3].coolDown);

            createPlatformButton.GetComponent<Image>().fillAmount = 1 -
                (Magic.mageSpells[4].GetCurrentCooldown() / Magic.mageSpells[4].coolDown);
        }
        else if (Magic.magicTypeChosen == Magic.MagicType.Priest)
        {
            backToStartButton.GetComponent<Image>().fillAmount = 1 -
                (Magic.priestSpells[3].GetCurrentCooldown() / Magic.priestSpells[3].coolDown);

            createPlatformButton.GetComponent<Image>().fillAmount = 1 -
                (Magic.priestSpells[4].GetCurrentCooldown() / Magic.priestSpells[4].coolDown);
        }
    }

    public void OnSceneChanged()
    {
        ResetBackToStartCounter();
    }

    private void ResetBackToStartCounter()
    {
        if (GameData.mageJoined) backToStartUseCounter = (int)Magic.mageSpells[3].GetPower();
        else if (GameData.priestJoined) backToStartUseCounter = (int)Magic.priestSpells[3].GetPower();
        else backToStartUseCounter = 0;
    }

    public void UpdateMagicUnlockedButtons()
    {
        if (Magic.magicTypeChosen == Magic.MagicType.None)
        {
            backToStartButton.SetActive(false);
            createPlatformButton.SetActive(false);
        }
        else
        {
            if (Magic.magicTypeChosen == Magic.MagicType.Mage)
            {
                if (Magic.mageSpells[3].level > 0) backToStartButton.SetActive(true);
                if (Magic.mageSpells[4].level > 0) createPlatformButton.SetActive(true);
            }
            else if (Magic.magicTypeChosen == Magic.MagicType.Priest)
            {
                if (Magic.priestSpells[3].level > 0) backToStartButton.SetActive(true);
                if (Magic.priestSpells[4].level > 0) createPlatformButton.SetActive(true);
            }
        }
    }

    public void SendPlayerToStartPoint()
    {
        if (backToStartUseCounter > 0)
        {
            if (Magic.magicTypeChosen == Magic.MagicType.Mage)
            {
                if (Magic.mageSpells[3].GetCurrentCooldown() <= 0)
                {
                    Magic.mageSpells[3].UseActiveSpell();
                    TeleportPlayerToStartPoint();
                }
            }
            else if (Magic.magicTypeChosen == Magic.MagicType.Priest)
            {
                if (Magic.priestSpells[3].GetCurrentCooldown() <= 0)
                {
                    Magic.priestSpells[3].UseActiveSpell();
                    TeleportPlayerToStartPoint();
                }
            }
        }
    }

    private void TeleportPlayerToStartPoint()
    {
        player.transform.position = GameObject.FindGameObjectWithTag("StartPoint").transform.position;
        backToStartUseCounter--;
    }

    public void CreatePlatform()
    {
        if (Magic.magicTypeChosen == Magic.MagicType.Mage)
        {
            if (Magic.mageSpells[4].GetCurrentCooldown() <= 0)
            {
                Magic.mageSpells[4].UseActiveSpell();
                CreateDarkPlatform();
            }
        }
        else if (Magic.magicTypeChosen == Magic.MagicType.Priest)
        {
            if (Magic.priestSpells[4].GetCurrentCooldown() <= 0)
            {
                Magic.priestSpells[4].UseActiveSpell();
                CreateLightPlatform();
            }
        }
    }

    private void CreateDarkPlatform()
    {
        Instantiate(darkPlatform, player.transform.position + new Vector3(1, 0, 0), darkPlatform.transform.rotation);
    }

    private void CreateLightPlatform()
    {
        Instantiate(darkPlatform, player.transform.position + new Vector3(1, 0, 0), darkPlatform.transform.rotation);
    }
}
