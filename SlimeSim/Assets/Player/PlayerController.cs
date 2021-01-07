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
        Sit,
        Mounted
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

    //mage/priest particle effects
    public GameObject mageParticles, priestParticles;

    //android controls
    public GameObject analog;
    private AnalogTouchControls analogControls;

    //mounts
    public GameObject mount { get; private set; }
    private float mountSpeed;
    private Rigidbody2D mountRb2d;

    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        moveVector = new Vector2();
        onGround = false;

        getUpTimer = getUpWaitTime;

        analogControls = analog.GetComponent<AnalogTouchControls>();

        mageParticles.SetActive(false);
        priestParticles.SetActive(false);

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
        rb2d.SetRotation(0);
        transform.localScale = Vector3.one;
        getUpTimer = getUpWaitTime;
        GameData.Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground") onGround = true;
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
            rb2d.velocity += new Vector2(0, getUpJumpForce);
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
                    if (MovementIntent()) agentState = agentStates.Running;
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
                    //on mage unlock
                    if (Magic.magicTypeChosen == Magic.MagicType.Mage) rb2d.AddForce(new Vector2(0, jumpForce + Magic.mageSpells[0].GetPower()));
                    //on priest unlock
                    else if (Magic.magicTypeChosen == Magic.MagicType.Priest) rb2d.AddForce(new Vector2(0, jumpForce + Magic.priestSpells[0].GetPower()));
                    //standard
                    else rb2d.AddForce(new Vector2(0, jumpForce));
                    agentState = agentStates.Jumping;
                    break;
                }
            case agentStates.Jumping:
                {
                    if (onGround) agentState = agentStates.Landing;
                    if (rb2d.velocity.magnitude < 0.01f) StartCoroutine("RotateToStand");
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
                    //use mage third spell to show door
                    if (Magic.magicTypeChosen == Magic.MagicType.Mage && Magic.mageSpells[2].GetCurrentCooldown() <= 0)
                        GameObject.FindGameObjectWithTag("Door").GetComponent<DoorHandler>().ShowDoor(Magic.mageSpells[2].UseActiveSpell());

                    //use priest third spell to show door
                    else if (Magic.magicTypeChosen == Magic.MagicType.Priest && Magic.priestSpells[2].GetCurrentCooldown() <= 0)
                        GameObject.FindGameObjectWithTag("Door").GetComponent<DoorHandler>().ShowDoor(Magic.priestSpells[2].UseActiveSpell());

                    if (MovementIntent()) agentState = agentStates.Iddle;
                    break;
                }
            case agentStates.Mounted:
                {
                    if (mount != null)
                    {
                        if (Application.platform == RuntimePlatform.Android)
                            mountRb2d.velocity = new Vector3(analogControls.AnalogPosition().normalized.x,
                                analogControls.AnalogPosition().normalized.y, 0) * Time.deltaTime * mountSpeed;
                        else
                            mountRb2d.velocity = new Vector3(Input.GetAxis("Horizontal"),
                                Input.GetAxis("Vertical"), 0) * Time.deltaTime * mountSpeed;

                        transform.position = mount.transform.position + new Vector3(0, 0.35f, 0);
                    }
                    else Dismount();

                    break;
                }
        }

        //on mage unlock
        if (Magic.magicTypeChosen == Magic.MagicType.Mage)
            rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -maxVel - Magic.mageSpells[1].GetPower(), maxVel + Magic.mageSpells[1].GetPower()), rb2d.velocity.y);
        //on priest unlock
        else if (Magic.magicTypeChosen == Magic.MagicType.Priest)
            rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -maxVel - Magic.priestSpells[1].GetPower(), maxVel + Magic.priestSpells[1].GetPower()), rb2d.velocity.y);
        //standard
        else rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -maxVel, maxVel), rb2d.velocity.y);
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

    public void Mount(GameObject mountObject,float mount_Speed) 
    {
        agentState = agentStates.Mounted;
        mount = mountObject;
        mountSpeed = mount_Speed;
        mountRb2d = mount.GetComponent<Rigidbody2D>();

        rb2d.freezeRotation = true;

        //disable interaction by setting the tag to default
        tag = "Untagged";
    }

    public void Dismount() 
    {
        agentState = agentStates.Iddle;

        rb2d.freezeRotation = false;

        //reenable interaction by re-setting the tag
        tag = "Player";
    }
}