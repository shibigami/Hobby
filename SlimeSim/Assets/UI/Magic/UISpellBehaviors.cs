using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISpellBehaviors : MonoBehaviour
{
    public GameObject player;
    private int backToStartUseCounter;

    public GameObject backToStartButton;
    public GameObject createPlatformButton;
    public GameObject lightPlatform;
    public GameObject darkPlatform;
    public GameObject lightProjectileButton;
    public GameObject lightProjectile;
    public GameObject darkPortalPointButton;
    public GameObject darkPortalPoint;
    public GameObject darkPortalTeleportButton;
    public Text darkPortalUsesText;

    private GameObject darkPortalPointInstance;
    private int darkPortalUses;

    // Start is called before the first frame update
    void Start()
    {
        ResetBackToStartCounter();
        UpdateMagicUnlockedButtons();

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

            darkPortalPointButton.GetComponent<Image>().fillAmount = 1 -
                (Magic.mageSpells[5].GetCurrentCooldown() / Magic.mageSpells[5].coolDown);

            //if there is a portal show the teleport button
            if (darkPortalPointInstance != null && darkPortalUses > 0 && player.GetComponent<PlayerController>().agentState != PlayerController.agentStates.Mounted)
            {
                darkPortalTeleportButton.SetActive(true);
                darkPortalUsesText.text = darkPortalUses.ToString();
            }
            else darkPortalTeleportButton.SetActive(false);
        }
        else if (Magic.magicTypeChosen == Magic.MagicType.Priest)
        {
            backToStartButton.GetComponent<Image>().fillAmount = 1 -
                (Magic.priestSpells[3].GetCurrentCooldown() / Magic.priestSpells[3].coolDown);

            createPlatformButton.GetComponent<Image>().fillAmount = 1 -
                (Magic.priestSpells[4].GetCurrentCooldown() / Magic.priestSpells[4].coolDown);

            lightProjectileButton.GetComponent<Image>().fillAmount = 1 -
                (Magic.priestSpells[5].GetCurrentCooldown() / Magic.priestSpells[5].coolDown);
        }
    }

    public void OnSceneChanged()
    {
        ResetBackToStartCounter();
    }

    private void ResetBackToStartCounter()
    {
        if (GameData.mageJoined)
        {
            backToStartUseCounter = (int)Magic.mageSpells[3].GetPower();
            darkPortalUses = (int)Magic.mageSpells[5].GetPower();
        }
        else if (GameData.priestJoined) backToStartUseCounter = (int)Magic.priestSpells[3].GetPower();
        else backToStartUseCounter = 0;
    }

    public void UpdateMagicUnlockedButtons()
    {
        if (Magic.magicTypeChosen == Magic.MagicType.None)
        {
            backToStartButton.SetActive(false);
            createPlatformButton.SetActive(false);
            darkPortalPointButton.SetActive(false);
            darkPortalTeleportButton.SetActive(false);
            lightProjectileButton.SetActive(false);
        }
        else
        {
            if (Magic.magicTypeChosen == Magic.MagicType.Mage)
            {
                if (Magic.mageSpells[3].level > 0) backToStartButton.SetActive(true);
                if (Magic.mageSpells[4].level > 0) createPlatformButton.SetActive(true);
                if (Magic.mageSpells[5].level > 0) darkPortalPointButton.SetActive(true);
            }
            else if (Magic.magicTypeChosen == Magic.MagicType.Priest)
            {
                if (Magic.priestSpells[3].level > 0) backToStartButton.SetActive(true);
                if (Magic.priestSpells[4].level > 0) createPlatformButton.SetActive(true);
                if (Magic.priestSpells[5].level > 0) lightProjectileButton.SetActive(true);
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
        if (player.GetComponent<PlayerController>().agentState == PlayerController.agentStates.Mounted) 
        {
            player.GetComponent<PlayerController>().Dismount();
            player.GetComponent<PlayerController>().mount.GetComponent<CloudController>().SetLifeTimer(2);
        }
        else
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
    }

    private void CreateDarkPlatform()
    {
        GameObject temp = Instantiate(darkPlatform, player.transform.position + new Vector3(0, 0, 0), darkPlatform.transform.rotation);
        temp.GetComponent<CloudController>().SetLifeTimer((int)Magic.mageSpells[4].GetPower());
    }

    private void CreateLightPlatform()
    {
        GameObject temp = Instantiate(lightPlatform, player.transform.position + new Vector3(0, 0, 0), lightPlatform.transform.rotation);
        temp.GetComponent<CloudController>().SetLifeTimer((int)Magic.priestSpells[4].GetPower());
    }

    public void CreateLightProjectile() 
    {
        if (Magic.priestSpells[5].GetCurrentCooldown() <= 0) 
        {
            Magic.priestSpells[5].UseActiveSpell();
            GameObject light = Instantiate(lightProjectile, player.transform.position, lightProjectile.transform.rotation);
            light.GetComponent<LightProjectileBehavior>().SetRadius(Magic.priestSpells[5].GetPower());
        }
    }

    public void CreateDarkPortalPoint() 
    {
        if (Magic.mageSpells[5].GetCurrentCooldown() <= 0) 
        {
            if (darkPortalPointInstance != null) Destroy(darkPortalPointInstance);
            Magic.mageSpells[5].UseActiveSpell();
            darkPortalPointInstance = Instantiate(darkPortalPoint, player.transform.position, darkPortalPoint.transform.rotation);
        }
    }

    public void TeleportToDarkPortalPoint()
    {
        if (darkPortalPointInstance != null && darkPortalUses > 0)
        {
            player.transform.position = darkPortalPointInstance.transform.position;
            darkPortalUses--;
        }
    }
}
