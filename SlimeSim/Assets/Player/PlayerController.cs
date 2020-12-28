using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public enum agentStates
    {
        Iddle,
        Running,
        Jump,
        Jumping,
        TurnToStand,
        Landing,
        Sit
    }

    public agentStates agentState { get; private set; }

    //movement
    private Rigidbody2D rb2d;
    public float speed, maxVel, jumpForce;
    public float getUpRotationIncrement, getUpJumpForce;
    //timer of x seconds to ensure character needs to stand up after timeout
    public float getUpWaitTime;
    private float getUpTimer;

    public Vector2 moveVector { get; private set; }
    private bool onGround;

    //android controls
    public GameObject analog;
    private AnalogTouchControls analogControls;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        moveVector = new Vector2();
        onGround = false;

        getUpTimer = getUpWaitTime;

        analogControls = analog.GetComponent<AnalogTouchControls>();

        Journal.init();
        GameData.init();

        InvokeRepeating("CheckOutOfStage", 1, 1);
    }

    private void CheckOutOfStage()
    {
        if (transform.position.y < -10)
        {
            Die();
        }
    }

    public void Die() 
    {
        transform.position = GameObject.FindGameObjectWithTag("StartPoint").transform.position;
        rb2d.velocity = new Vector2(0, 0);
        moveVector = new Vector2(0, 0);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.localScale = Vector3.one;
        getUpTimer = getUpWaitTime;
        GameData.Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Ground") onGround = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground") onGround = false;
    }

    private void CheckJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            agentState = agentStates.Jump;
        }
        if (!onGround) agentState = agentStates.Jumping;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground") moveVector = Vector2.zero;
    }

    private IEnumerator RotateToStand() 
    {
        if (getUpTimer <= 0)
        {
            rb2d.AddForce(new Vector2(0, getUpJumpForce));
            rb2d.AddTorque(getUpRotationIncrement);
        }
        else
        {
            getUpTimer -= Time.deltaTime;
        }

        yield return new WaitForSeconds(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (agentState)
        {
            case agentStates.Iddle:
                {
                    if(MovementIntent()) agentState = agentStates.Running;
                    CheckJump();
                    if (Input.GetButton("Fire1")) agentState = agentStates.Sit;
                    break;
                }
            case agentStates.Running:
                {
                    if (Application.platform == RuntimePlatform.Android) 
                        moveVector = new Vector2(analogControls.AnalogPosition().x * Time.deltaTime * speed, 0);
                    else moveVector = new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0);
                    if (!MovementIntent()) agentState = agentStates.Iddle;
                    CheckJump();

                    if (moveVector.x != 0)
                    {
                        rb2d.AddForce(moveVector);
                    }
                    break;
                }
            case agentStates.Jump:
                {
                    rb2d.AddForce(new Vector2(0, jumpForce));
                    agentState = agentStates.Jumping;
                    break;
                }
            case agentStates.Jumping:
                {
                    if (onGround) agentState = agentStates.Landing;
                    if(rb2d.velocity.magnitude<0.01f) StartCoroutine("RotateToStand");
                    break;
                }
            case agentStates.Landing:
                {
                    StopCoroutine("RotateToStand");
                    getUpTimer = getUpWaitTime;
                    agentState = agentStates.Iddle;
                    break;
                }
            case agentStates.Sit:
                {
                    if (MovementIntent()) agentState = agentStates.Iddle;
                    break;
                }
        }

        rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -maxVel, maxVel), rb2d.velocity.y);
    }

    private bool MovementIntent() 
    {
        if (Input.GetAxis("Horizontal") != 0 || analogControls.AnalogPosition().x != 0) return true;
        else return false;
    }

    public void Jump()
    {
        if (agentState == agentStates.Running || agentState == agentStates.Iddle)
        {
            agentState = agentStates.Jump;
        }
    }

    public void Sit() 
    {
        if (agentState == agentStates.Iddle) agentState = agentStates.Sit;
    }
}