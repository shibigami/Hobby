using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FakeWallBehavior : MonoBehaviour
{
    private enum WallState { Hiding, Speak, Absorbing, Dissapearing, Dissapear ,Idle}
    private WallState wallState;

    public GameObject animator;
    public GameObject bag;
    public GameObject fiendIndicator;
    public GameObject eye;
    public GameObject wall;

    private GameObject player;
    private int coinCount;
    public float absorbSpeed;
    public float dissapearRate;

    private Color tempColor;

    public bool isFiend;

    // Start is called before the first frame update
    void Start()
    {
        wallState = WallState.Hiding;

        coinCount = 0;

        if (isFiend) bag.SetActive(false);
        else fiendIndicator.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != "Hub")
        {
            switch (wallState)
            {
                case WallState.Hiding:
                    {
                        if(PlayerSat()) wallState = WallState.Speak;
                        Debug.Log("Is player sat?"+PlayerSat());
                        break;
                    }
                case WallState.Speak:
                    {
                        animator.GetComponent<Animator>().SetTrigger("PlayerSat");
                        if (!isFiend)
                        {
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Ah! A traveller, just like myself. But with legs and feet!");
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("So glad to see you. You see, I'm a bit\n" +
                                                                                                            "in between a rock and a hard place here.");
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("I was cursed by some fiend after accepting to give him some\n" +
                                                                                                            "sort of shinny, round, metal like objects.");
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("He took over my physical appearance, and I his!\n" +
                                                                                                            "You can imagine how much of a blockhead I felt afterwards!");
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("But no matter. I'll strike a deal with you. You help me\n" +
                                                                                                            "out and I'll help you if you ever find more like the fiend\n" +
                                                                                                            "who gave me a cold shoulder. Or none whatsoever. Ha!");
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("* cough * Hmm, I must apologize for my concrete humour.\n" +
                                                                                                            "Help me out and I'll leave no stone unturned.\n" +
                                                                                                            "Bahaha!");
                        }
                        wallState = WallState.Absorbing;
                        break;
                    }
                case WallState.Absorbing:
                    {
                        if (coinCount >= 10) wallState = WallState.Dissapearing;
                        break;
                    }
                case WallState.Dissapearing:
                    {
                        if (!isFiend)
                        {
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Yes! Those are the ones!");
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Thank you! And now... I shall curse you instead!!");
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Bwahaha!!");
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("...");
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("My jokes crack me up.\n" +
                                                                                                                "Almost felt a bit of a draft there. All good though!");
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("I'll help you out.\n" +
                                                                                                                "If you ever see a strange Wall acting suspicous,\n" +
                                                                                                                "throw a few of those near it. About 10 should be enough.");
                        }
                        wallState = WallState.Dissapear;
                        break;
                    }
                case WallState.Dissapear:
                    {
                        if (!GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().IsChatShowing())
                        {
                            //change this
                            if (wall.GetComponent<SpriteRenderer>().color.a > 0)
                            {
                                //assuming all objects color is white with full alpha
                                tempColor = wall.GetComponent<SpriteRenderer>().color;
                                tempColor.a -= dissapearRate * Time.deltaTime;

                                wall.GetComponent<SpriteRenderer>().color = tempColor;
                                eye.GetComponent<SpriteRenderer>().color = tempColor;
                                fiendIndicator.GetComponent<SpriteRenderer>().color = tempColor;
                                bag.GetComponent<SpriteRenderer>().color = tempColor;
                            }
                            else
                            {
                                if (!isFiend) GameData.FakeWallJoins();

                                gameObject.SetActive(false);
                            }
                        }
                        break;
                    }
            }
        }
        else
        {
            switch (wallState)
            {
                case WallState.Hiding:
                    {
                        if (PlayerSat()) wallState = WallState.Speak;
                        break;
                    }
                case WallState.Speak:
                    {
                        //open eye
                        animator.GetComponent<Animator>().SetTrigger("PlayerSat");

                        //dialogue
                        if (!HubData.wallHouseBuilt)
                        {
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("I'm not sure what this place is...");
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("But since you're here, why not contribute\n" +
                                                                                                                "for some renovations?");

                            coinCount = HubData.GetHouseBricksBuilt();
                            //get coins
                            wallState = WallState.Absorbing;
                        }
                        else
                        {
                            GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>().ShowDialogue("Insert joke here");

                            wallState = WallState.Idle;
                        }

                        break;
                    }
                case WallState.Absorbing:
                    {
                        if (coinCount != HubData.GetHouseBricksBuilt()) 
                        {
                            GameObject.FindGameObjectWithTag("House").GetComponent<HouseBehavior>().UpdateBuild();
                            GameData.Save();
                            coinCount = HubData.GetHouseBricksBuilt();
                        }
                        if (HubData.wallHouseBuilt) wallState = WallState.Idle;
                        break;
                    }
                case WallState.Idle:
                    {

                        break;
                    }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") player = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") player = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Coin"&& wallState == WallState.Absorbing)
        {
            //go towards wall
            collision.transform.position += (transform.position - collision.transform.position).normalized * absorbSpeed * Time.deltaTime;

            //if wall is absorbing and it has absorbed less than 10 coins
            if (coinCount < 10||SceneManager.GetActiveScene().name=="Hub")
            {
                //if coin isnt picked and the distance to the gameobject is less than absorb range
                if (!collision.GetComponent<Coin>().picked)
                {
                    collision.GetComponent<Coin>().PickUp();
                    coinCount++;
                    if (SceneManager.GetActiveScene().name == "Hub") HubData.AddWallCoins();
                }
            }
        }
    }

    private bool PlayerSat() 
    {
        if (player != null)
        {
            if (player.GetComponent<PlayerController>().agentState == PlayerController.agentStates.Sit)
            {
                return true;
            }
            return false;
        }
        return false;
    }
}