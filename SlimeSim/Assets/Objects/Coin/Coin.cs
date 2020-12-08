using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject animationObject;
    private Animator animator;
    private bool picked;
    public float disapearTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = animationObject.GetComponent<Animator>();
        picked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (picked)
        {
            disapearTime -= Time.deltaTime;
            if (disapearTime < 0) Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"&&!picked)
        {
            GameData.AddGold(1);
            animator.SetTrigger("PickUp");
            picked=true;
        }
    }
}
