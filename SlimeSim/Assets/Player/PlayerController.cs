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
        Landing
    }

    public agentStates agentState { get; private set; }

    private Rigidbody2D rb2d;
    public float speed, maxVel, jumpForce,rotateSpeed;
    private Vector2 moveVector;
    private bool onGround;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        rb2d = GetComponent<Rigidbody2D>();
        moveVector = new Vector2();
        onGround = false;

        InvokeRepeating("CheckDeath", 1, 1);
    }

    private void CheckDeath()
    {
        if (transform.position.y < -10)
        {
            transform.position = new Vector3(GameObject.FindGameObjectWithTag("StartPoint").transform.position.x, 7, 0);
            rb2d.velocity = new Vector2(0, 0);
        }
    }


    private void CheckJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            agentState = agentStates.Jump;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            moveVector.x = 0;
            onGround = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") onGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (agentState)
        {
            case agentStates.Iddle:
                {
                    if (Input.GetAxis("Horizontal") != 0) agentState = agentStates.Running;
                    CheckJump();
                    break;
                }
            case agentStates.Running:
                {
                    if (Input.GetAxis("Horizontal") != 0) moveVector.x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
                    else agentState = agentStates.Iddle;
                    CheckJump();
                    break;
                }
            case agentStates.Jump:
                {
                    rb2d.velocity = (new Vector2(0, jumpForce));
                    break;
                }
            case agentStates.Jumping:
                {
                    if (onGround) agentState = agentStates.Landing;
                    break;
                }
            case agentStates.Landing:
                {
                    if (transform.rotation.z < -0.2f)
                    {
                        transform.Rotate(0, 0, rotateSpeed, Space.Self);
                    }
                    else if (transform.rotation.z > 0.2f)
                    {
                        transform.Rotate(0, 0, -rotateSpeed, Space.Self);
                    }
                    else agentState = agentStates.Iddle;
                    break;
                }

        }

        if (moveVector.x != 0)
        {
            rb2d.AddForce(moveVector);
            rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -maxVel, maxVel), rb2d.velocity.y);
        }

        if (!onGround) agentState = agentStates.Jumping;
    }

}