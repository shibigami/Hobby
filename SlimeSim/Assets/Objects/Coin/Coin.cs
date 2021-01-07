using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Coin : MonoBehaviour
{
    public GameObject animationObject;
    private Animator animator;
    public bool picked { get; private set; }
    public float disapearTime;
    private Rigidbody2D rb2d;

    private GameObject player;
    public bool canPickUp { get; private set; }
    public float thrownedTime { get; private set; }
    public float pickUpRange;

    // Start is called before the first frame update
    void Start()
    {
        animator = animationObject.GetComponent<Animator>();
        picked = false;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector2(Random.Range(-75,75),125));

        player = GameObject.FindGameObjectWithTag("Player");

        thrownedTime = 1.5f;
        canPickUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (picked)
        {
            disapearTime -= Time.deltaTime;
            if (disapearTime < 0) Destroy(gameObject);
        }

        if (!canPickUp)
        {
            thrownedTime -= Time.deltaTime;
            if (thrownedTime < 0) canPickUp = true;
        }
        else if (player != null)
        {
            if ((transform.position - player.transform.position).magnitude < pickUpRange)
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player" || collision.tag == "Mount") && !picked && canPickUp)
        {
            GameData.AddGold(10);
            PickUp();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.tag == "Player" || collision.tag == "Mount") && !picked && canPickUp)
        {
            GameData.AddGold(10);
            PickUp();
        }
    }

    public void SetThrowned(float time) 
    {
        thrownedTime = time;
        canPickUp = true;
    }

    public void PickUp() 
    {
        animator.SetTrigger("PickUp");
        picked = true;
    }
}
