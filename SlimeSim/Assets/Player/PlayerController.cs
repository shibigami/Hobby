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

    public agentStates agentState;

    private Rigidbody2D rb2d;
    public float speed, maxVel, jumpForce, rotateSpeed,rotateJumpForce;
    public Vector2 moveVector { get; private set; }
    private bool onGround;

    //android controls
    public GameObject analog;
    private AnalogTouchControls analogControls;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        rb2d = GetComponent<Rigidbody2D>();
        moveVector = new Vector2();
        onGround = false;

        analogControls = analog.GetComponent<AnalogTouchControls>();

        InvokeRepeating("CheckDeath", 1, 1);
    }

    private void CheckDeath()
    {
        if (transform.position.y < -10)
        {
            transform.position = new Vector3(GameObject.FindGameObjectWithTag("StartPoint").transform.position.x, 7, 0);
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            moveVector = new Vector2(0,moveVector.y);
            GameData.Die();
        }
    }


    private void CheckJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            agentState = agentStates.Jump;
        }
        if (!onGround) agentState = agentStates.Jumping;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            moveVector = new Vector2(0, 0);
            onGround = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground") onGround = false;
    }

    private IEnumerator RotatingToStand()
    {
        if (rb2d.velocity.y < 0.001f&& rb2d.velocity.y > -0.001f)
        {
            rb2d.AddForce(new Vector2(0, rotateJumpForce));
            if (transform.rotation.z < Mathf.Deg2Rad * -10)
            {
                transform.Rotate(0, 0, rotateSpeed, Space.Self);
            }
            else if (transform.rotation.z > 10 * Mathf.Deg2Rad)
            {
                transform.Rotate(0, 0, -rotateSpeed, Space.Self);
            }
        }
        yield return new WaitForSecondsRealtime(0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (agentState)
        {
            case agentStates.Iddle:
                {
                    if (Input.GetAxis("Horizontal") != 0||analogControls.AnalogPosition().x!=0) agentState = agentStates.Running;
                    CheckJump();
                    break;
                }
            case agentStates.Running:
                {
                    if(Application.platform==RuntimePlatform.Android) moveVector = new Vector2(analogControls.AnalogPosition().x * Time.deltaTime * speed,0);
                    else moveVector = new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * speed,0);
                    if (rb2d.velocity.x == 0) agentState = agentStates.Iddle;
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
                    else StartCoroutine(RotatingToStand());
                    break;
                }
            case agentStates.Landing:
                {
                    agentState = agentStates.Iddle;
                    break;
                }
        }

        rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -maxVel, maxVel), rb2d.velocity.y);
    }

    public void Jump()
    {
        if (agentState == agentStates.Running || agentState == agentStates.Iddle)
        {
            agentState = agentStates.Jump;
        }
    }
}