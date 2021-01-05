using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestMaker : MonoBehaviour
{
    public enum AgentState { Idle, Absorb, Transform, Transformed }
    public AgentState agentState { get; private set; }

    public GameObject uiCanvas;
    public Text amountText;
    public GameObject animatorObject;
    private Animator animator;
    public GameObject chest;
    public int maxAbsorbAmount, amountGiven;
    private int absorbedAmount;

    public float absorbSpeed;
    public float absorbRadius;

    private float absorbTimer;

    public bool mimic;
    //mimic indicator must have an animator with a "Reset" trigger for resetting animation
    public GameObject mimicIndicator;

    // Start is called before the first frame update
    void Start()
    {
        agentState = AgentState.Idle;

        animator = animatorObject.GetComponent<Animator>();

        uiCanvas.SetActive(false);

        if (!mimic)
        {
            mimicIndicator.SetActive(false);
            tag = "BigChest";
        }
        else mimicIndicator.SetActive(true);

        InvokeRepeating("UpdateAmountText", 0, 0.3f);
    }

    private void UpdateAmountText()
    {
        if (absorbedAmount > 0 && !uiCanvas.activeSelf) uiCanvas.SetActive(true);
        
        if(uiCanvas.activeSelf) amountText.text = string.Format("{0}", maxAbsorbAmount - absorbedAmount);
    }

    // Update is called once per frame
    void Update()
    {
        switch (agentState)
        {
            case AgentState.Idle:
                {
                    if (absorbedAmount >= maxAbsorbAmount)
                    {
                        StartCoroutine("Transform");
                        agentState = AgentState.Transform;
                    }

                    break;
                }
            case AgentState.Absorb:
                {
                    if (!animator.GetBool("Absorb"))
                    {
                        animator.SetBool("Absorb", true);
                        //reset indicator animator
                        mimicIndicator.GetComponent<Animator>().SetTrigger("Reset");
                    }
                    absorbTimer -= Time.deltaTime;

                    if (absorbTimer <= 0)
                    {
                        animator.SetBool("Absorb", false);
                        //reset indicator animator
                        mimicIndicator.GetComponent<Animator>().SetTrigger("Reset");
                        agentState = AgentState.Idle;
                    }

                    break;
                }
            case AgentState.Transform:
                {
                    animator.SetBool("Full", true);
                    break;
                }
            case AgentState.Transformed:
                {
                    Destroy(gameObject);
                    break;
                }
        }
    }
    private IEnumerator Transform()
    {
        yield return new WaitForSeconds(1);

        if (!mimic)
        {
            //remove tag to disable radar pin
            tag = "Untagged";

            GameObject temp = Instantiate(chest, transform.position, transform.rotation);
            temp.GetComponent<Chest>().gold = amountGiven;
        }

        agentState = AgentState.Transformed;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (absorbedAmount < maxAbsorbAmount && collision.tag == "Coin")
        {
            //if not picked
            if (!collision.GetComponent<Coin>().picked)
            {
                //set timer for animation handling
                absorbTimer = 1f;

                //set animationstate
                if (agentState != AgentState.Absorb) agentState = AgentState.Absorb;

                //pull coin towards center of transform
                collision.transform.position = Vector2.MoveTowards(collision.transform.position,
                    transform.position, absorbSpeed * Time.deltaTime);

                //if close to center pick up
                if ((collision.transform.position - transform.position).magnitude < absorbRadius)
                {
                    collision.GetComponent<Coin>().PickUp();
                    absorbedAmount++;
                }
            }
        }
    }
}