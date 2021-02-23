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
        InAir,
        TurnToStand,
        Landing,
        Sit,
        Mounting,
        Mounted
    }

    public agentStates agentState { get; private set; }

    //movement
    private Rigidbody2D rb2d;
    public float speed, maxVel, jumpForce;
    public float standTorque;
    private float forceTorque;
    public float acceptableStandRotation;
    public Vector2 moveVector { get; private set; }
    private Collider2D feetTrigger; //this is the only box collider attached
    private bool onGround;
    public float landingDelay;
    private float landingTimer;

    //mage/priest particle effects
    public GameObject mageParticles, priestParticles;
    //mage/priest masks
    public GameObject mageMask, priestMask;
    //backpack
    public GameObject backPack;

    //android controls
    public GameObject analog;
    private AnalogTouchControls analogControls;

    //mounts
    public GameObject mount { get; private set; }
    private float mountDelay;
    private float mountSpeed;
    private Rigidbody2D mountRb2d;

    //death particles
    public GameObject deathParticles;

    //plant growth particles
    public GameObject plantGrowthParticles;
    private Collider2D[] plantColliders;

    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        moveVector = new Vector2(0,0);
        feetTrigger = GetComponent<BoxCollider2D>();
        onGround = false;

        analogControls = analog.GetComponent<AnalogTouchControls>();

        mageParticles.SetActive(false);
        priestParticles.SetActive(false);
        mageMask.SetActive(false);
        priestMask.SetActive(false);

        backPack.SetActive(false);

        plantGrowthParticles.GetComponent<ParticleSystem>().Stop();

        Invoke("UpdateWornObjects",1f);

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
        //death particles
        Instantiate(deathParticles, transform.position, transform.rotation);
        //move player to start of stage
        transform.position = GameObject.FindGameObjectWithTag("StartPoint").transform.position;
        //set velocity
        rb2d.velocity = new Vector2(0, 0);
        //set movement cast vector
        moveVector = new Vector2(0, 0);
        //set rotation
        transform.rotation = new Quaternion(0, 0, 0, 0);
        //and torque
        rb2d.SetRotation(0);
        //reset looking direction
        transform.localScale = Vector3.one;
        //gamedata logic when death occurs
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
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            moveVector = Vector2.zero;
            feetTrigger.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (agentState)
        {
            case agentStates.Iddle:
                {
                    //freeze rotation while walking
                    if (rb2d.constraints != RigidbodyConstraints2D.FreezeRotation) rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
                    //set correct rotation for standing up
                    if (transform.rotation.z != 0) transform.rotation = new Quaternion(0, 0, 0, 0);

                    //only move after landing delay
                    if (Time.time > landingTimer)
                    {
                        if (MovementIntent()) agentState = agentStates.Running;
                        CheckJump();
                    }

                    //if dropping
                    if (!onGround) agentState = agentStates.InAir;
                    break;
                }
            case agentStates.Running:
                {
                    //use controls appropriate to platform
                    if (Application.platform == RuntimePlatform.Android)
                        moveVector = new Vector2(analogControls.AnalogPosition().x * Time.deltaTime * speed, 0);
                    else moveVector = new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0);
                    //change to idle if no more movement intent
                    if (!MovementIntent()) agentState = agentStates.Iddle;
                    //check for jump
                    CheckJump();
                    //falling/jumping
                    if (!onGround) agentState = agentStates.InAir;

                    //adjust velocity according to upgrades unlocked
                    if (moveVector.x != 0)
                    {
                        //on mage unlock
                        if (Magic.magicTypeChosen == Magic.MagicType.Mage)
                            rb2d.velocity = new Vector2(Mathf.Clamp(moveVector.x, -maxVel - Magic.mageSpells[1].GetPower(), maxVel + Magic.mageSpells[1].GetPower()), rb2d.velocity.y);
                        //on priest unlock
                        else if (Magic.magicTypeChosen == Magic.MagicType.Priest)
                            rb2d.velocity = new Vector2(Mathf.Clamp(moveVector.x, -maxVel - Magic.priestSpells[1].GetPower(), maxVel + Magic.priestSpells[1].GetPower()), rb2d.velocity.y);
                        //standard
                        else rb2d.velocity = new Vector2(Mathf.Clamp(moveVector.x, -maxVel, maxVel), rb2d.velocity.y);
                    }
                    break;
                }
            case agentStates.Jump:
                {
                    if (rb2d.constraints == RigidbodyConstraints2D.FreezeRotation) rb2d.constraints = RigidbodyConstraints2D.None;

                    //on mage unlock
                    if (Magic.magicTypeChosen == Magic.MagicType.Mage) rb2d.AddForce(new Vector2(0, jumpForce + Magic.mageSpells[0].GetPower()));
                    //on priest unlock
                    else if (Magic.magicTypeChosen == Magic.MagicType.Priest) rb2d.AddForce(new Vector2(0, jumpForce + Magic.priestSpells[0].GetPower()));
                    //standard
                    else rb2d.AddForce(new Vector2(0, jumpForce));
                    agentState = agentStates.InAir;
                    break;
                }
            case agentStates.InAir:
                {
                    if (rb2d.constraints == RigidbodyConstraints2D.FreezeRotation) rb2d.constraints = RigidbodyConstraints2D.None;

                    //if y velocity is between these points
                    if (rb2d.velocity.y > -0.02f && rb2d.velocity.y < 0.02f) 
                    {
                        //enable feet
                        if(!feetTrigger.enabled) feetTrigger.enabled = true;

                        //if the |rotation| is lesser than the acceptable rotation
                        if (Mathf.Sqrt(Mathf.Pow(transform.rotation.z, 2)) < acceptableStandRotation)
                        {
                            //stand
                            transform.rotation = new Quaternion(0, 0, 0, 0);
                            agentState = agentStates.Landing;
                        }
                    }
                    else feetTrigger.enabled = false;

                    //enable a slight increase in torque using the direction values
                    //kind of fixes getting stuck with an awkward rotation and being able to get up
                    if (MovementIntent())
                    {
                        if (Application.platform == RuntimePlatform.Android)
                            forceTorque = analogControls.AnalogPosition().normalized.x*standTorque;
                        else forceTorque = Input.GetAxis("Horizontal")*standTorque;
                        rb2d.AddTorque(-forceTorque);
                        rb2d.velocity += new Vector2(forceTorque*Time.deltaTime,0);
                    }

                    if (onGround)
                    {
                        landingTimer = Time.time + landingDelay;
                        agentState = agentStates.Landing;
                    }

                    break;
                }
            case agentStates.Landing:
                {
                    agentState = agentStates.Iddle;
                    break;
                }
            case agentStates.Sit:
                {
                    //use mage third spell to show door
                    if (Magic.magicTypeChosen == Magic.MagicType.Mage && Magic.mageSpells[2].level > 0 && Magic.mageSpells[2].GetCurrentCooldown() <= 0)
                        GameObject.FindGameObjectWithTag("Door").GetComponent<DoorHandler>().ShowDoor(Magic.mageSpells[2].UseActiveSpell());

                    //use priest third spell to show door
                    else if (Magic.magicTypeChosen == Magic.MagicType.Priest && Magic.priestSpells[2].level > 0 && Magic.priestSpells[2].GetCurrentCooldown() <= 0)
                        GameObject.FindGameObjectWithTag("Door").GetComponent<DoorHandler>().ShowDoor(Magic.priestSpells[2].UseActiveSpell());

                    if (MovementIntent())
                    {
                        agentState = agentStates.Iddle;
                    }

                    //sing if plants nearby
                    plantColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
                    bool check = false;
                    for (int i = 0; i < plantColliders.Length; i++)
                    {
                        if (plantColliders[i].tag == "Seed")
                        {
                            if (!plantColliders[i].GetComponent<GenericSeedBehavior>().pickable)
                            {
                                check = true;
                                break;
                            }
                        }
                    }
                    if (check && plantGrowthParticles.GetComponent<ParticleSystem>().isStopped)
                        plantGrowthParticles.GetComponent<ParticleSystem>().Play();

                    break;
                }
            case agentStates.Mounting:
                {
                    //disable plant growth particles
                    if (plantGrowthParticles.GetComponent<ParticleSystem>().isPlaying)
                        plantGrowthParticles.GetComponent<ParticleSystem>().Stop();

                    if(Time.time>mountDelay)
                    //mount
                    agentState = agentStates.Mounted;
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
    }

    public bool MovementIntent()
    {
        if (Input.GetAxis("Horizontal") != 0 || analogControls.AnalogPosition().x != 0)
        {
            //stop growth particles on movement intent
            if (plantGrowthParticles.GetComponent<ParticleSystem>().isPlaying)
                plantGrowthParticles.GetComponent<ParticleSystem>().Stop();

            return true;
        }
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
        mount = mountObject;
        mountSpeed = mount_Speed;
        mountRb2d = mount.GetComponent<Rigidbody2D>();

        rb2d.bodyType = RigidbodyType2D.Static;
        rb2d.freezeRotation = true;

        mountDelay = Time.time + 0.5f;

        agentState = agentStates.Mounting;

        //disable interaction by setting the tag to default
        tag = "Untagged";
    }

    public void Dismount()
    {
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        agentState = agentStates.Iddle;
        rb2d.velocity = new Vector2(0, 0);
        rb2d.freezeRotation = false;

        //reenable interaction by re-setting the tag
        tag = "Player";
    }

    public void UpdateWornObjects() 
    {
        if (GameData.mageJoined)
        {
            mageMask.SetActive(true);
            mageParticles.SetActive(true);
        }
        else if (GameData.priestJoined) 
        {
            priestMask.SetActive(true);
            priestParticles.SetActive(true);
        }
        if (GameData.inventoryUnlocked) backPack.SetActive(true);
    }
}