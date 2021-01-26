using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSeedBehavior : MonoBehaviour
{
    public int itemID;
    public bool pickable;
    private bool picked;
    public float absorbRange;
    public float pickUpRange;
    private Collider2D[] colliders;
    private Rigidbody2D rb2d;

    public float fullGrowthTime;
    private float[] timePerStage;
    private int currentStage;
    private bool playerNearby;
    private PlayerController playerController;

    public Sprite[] stagesSprites;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        if (!pickable)
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            rb2d = GetComponent<Rigidbody2D>();

            spriteRenderer = GetComponent<SpriteRenderer>();

            //initialise time per stage
            timePerStage = new float[stagesSprites.Length];
            for (int i = 1; i <= stagesSprites.Length; i++)
            {
                timePerStage[i - 1] = Time.time + (fullGrowthTime / stagesSprites.Length) * i;
            }

            currentStage = 0;
            spriteRenderer.sprite = stagesSprites[currentStage];
        }
        else picked = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!pickable)
        {
            //if player is sitting nearby
            if (playerNearby && playerController.agentState == PlayerController.agentStates.Sit)
            {
                //reduce current growth time
                for (int i = 0; i < stagesSprites.Length; i++) timePerStage[i] -= Time.deltaTime / 10;
            }

            if (currentStage < timePerStage.Length)
            {
                if (Time.time > timePerStage[currentStage])
                {
                    spriteRenderer.GetComponent<SpriteRenderer>().sprite = stagesSprites[currentStage];
                    
                    currentStage++;

                    rb2d.constraints = RigidbodyConstraints2D.FreezePositionX;
                }
            }
        }
        else 
        {
            colliders = Physics2D.OverlapCircleAll(transform.position,absorbRange);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == "Player")
                {
                    transform.position = Vector2.MoveTowards(transform.position, colliders[i].transform.position,1*Time.deltaTime);
                    if ((transform.position - colliders[i].transform.position).magnitude < pickUpRange&&!picked)
                    {
                        picked = true;
                        InventorySystem.AddItem(itemID);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Player") playerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.tag == "Player") playerNearby = false;
    }

    public bool isFullyGrown() 
    {
        if (currentStage >= stagesSprites.Length) return true;
        else return false;
    }
}