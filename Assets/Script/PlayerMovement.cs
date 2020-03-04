using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //AUDIO
    [SerializeField] AudioSource audioSource;
    bool stepPlayed = false;
    [SerializeField] List<AudioClip> audioClips;

    enum CurrentLane { LEFT, MID, RIGHT };
    CurrentLane currentLane = CurrentLane.MID;

    CapsuleCollider capsuleCollider;
    [SerializeField] float duckHeight;
    [SerializeField] float standingHeight;

    //RUNNING
    [SerializeField] float stoppingSpeed;
    [SerializeField] float speedDecrement;
    [SerializeField] float speedIncrement;
    [SerializeField] float speedPenalty;
    [SerializeField] float timeBeforeStop = 1f;

    [SerializeField] float timeCheckInterval = 1;
    float timeSinceCheck = 0;
    int stepCount = 0;

    bool stepped = false;

    //DUCK
    bool ducking = false;

    //JUMP
    bool jumping = false;
    bool canJump = false;
    [SerializeField] float timeToApex;
    [SerializeField] float timeBeforeJumpStart;
    [SerializeField] float timeToGround;
    [SerializeField] float jumpHeight;

    Vector3 groundVector;

    private Animator anim;
    static GameManager gm;
    static CrowdManager cm;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Get;
        cm = CrowdManager.Get;
        groundVector = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("LeftFootLeft") || Input.GetButtonDown("RightFootLeft") || Input.GetButtonDown("LeftFootMid")
            || Input.GetButtonDown("RightFootMid") || Input.GetButtonDown("LeftFootRight") || Input.GetButtonDown("RightFootRight")))
        {
            if (!jumping)
            {
                Vector3 newPos = transform.position;
                if (Input.GetButtonDown("LeftFootLeft"))
                {
                    newPos.x = -gm.LaneWidth;
                    currentLane = CurrentLane.LEFT;
                }
                if (Input.GetButtonDown("RightFootLeft"))
                {
                    newPos.x = -gm.LaneWidth;
                    currentLane = CurrentLane.LEFT;
                }

                if (Input.GetButtonDown("LeftFootMid"))
                {
                    newPos.x = 0;
                    currentLane = CurrentLane.MID;
                }
                if (Input.GetButtonDown("RightFootMid"))
                {
                    newPos.x = 0;
                    currentLane = CurrentLane.MID;
                }

                if (Input.GetButtonDown("LeftFootRight"))
                {
                    newPos.x = gm.LaneWidth;
                    currentLane = CurrentLane.RIGHT;
                }
                if (Input.GetButtonDown("RightFootRight"))
                {
                    newPos.x = gm.LaneWidth;
                    currentLane = CurrentLane.RIGHT;
                }
                transform.position = newPos;
            }

            if (stepPlayed)
            {
                stepPlayed = false;
                audioSource.PlayOneShot(audioClips[0]);
            }
            else
            {
                stepPlayed = true;
                audioSource.PlayOneShot(audioClips[1]);
            }
            if (jumping)
            {
                StartCoroutine(JumpDown());
            }
            if (Input.GetButtonDown("Duck"))
            {
                capsuleCollider.height = duckHeight;
                ducking = true;
                Vector3 newPos = new Vector3(capsuleCollider.center.x, (standingHeight / 2f) - (duckHeight / 2f), capsuleCollider.center.z);
                capsuleCollider.center = newPos;
            }
            else if (Input.GetButtonUp("Duck"))
            {
                capsuleCollider.height = standingHeight;
                ducking = false;
                Vector3 newPos = new Vector3(capsuleCollider.center.x, (standingHeight / 2f), capsuleCollider.center.z);
                capsuleCollider.center = newPos;
            }
        }

        if (((Input.GetButton("LeftFootLeft") && Input.GetButton("RightFootLeft")) || (Input.GetButton("LeftFootMid")
        && Input.GetButton("RightFootMid")) || (Input.GetButton("LeftFootRight") && Input.GetButton("RightFootRight"))) && !jumping)
        {
            StartCoroutine(CheckForJump());
        }

        if (timeSinceCheck > timeCheckInterval)
        {
            if (stepCount == 0 && gm.MoveSpeed != 0)
            {
                gm.MoveSpeed = gm.MoveSpeed / 2f;
            }

            stepped = true;

            gm.MoveSpeed += (speedIncrement * (stepCount / timeCheckInterval)) * Time.deltaTime;            

            if (!jumping)
            {
                if (!stepped)
                {
                    gm.MoveSpeed -= stoppingSpeed * Time.deltaTime;
                }
                else
                {
                    gm.MoveSpeed -= (gm.MoveSpeed) * Time.deltaTime;
                }
            }
            //Debug.Log(stepCount);

            stepCount = 0;
            timeSinceCheck = 0;
        }
        else
        {
            timeSinceCheck += Time.deltaTime;
            if ((Input.GetButtonDown("LeftFootLeft") || Input.GetButtonDown("RightFootLeft") || Input.GetButtonDown("LeftFootMid")
            || Input.GetButtonDown("RightFootMid") || Input.GetButtonDown("LeftFootRight") || Input.GetButtonDown("RightFootRight")))
            {
                stepCount++;
            }
        }
        AnimationCheck();
    }

    IEnumerator CheckForJump()
    {
        float time = 0;
        while (true)
        {
            if (!(Input.GetButtonDown("LeftFootLeft") && Input.GetButtonDown("RightFootLeft") && Input.GetButtonDown("LeftFootMid")
            && Input.GetButtonDown("RightFootMid") && Input.GetButtonDown("LeftFootRight") && Input.GetButtonDown("RightFootRight")))
            {
                time += Time.deltaTime;
                //
            }
            else
            {
                time = 0;
                break;
            }

            if (time >= timeBeforeJumpStart)
            {
                jumping = true;
                StartCoroutine(JumpUp());
                break;
            }
            yield return null;
        }
    }

    IEnumerator JumpUp()
    {
        float time = 0;
        Vector3 jumpVector = new Vector3(transform.position.x, jumpHeight, transform.position.z);
        groundVector = new Vector3(transform.position.x, groundVector.y, groundVector.z);
        while (true)
        {
            time += Time.deltaTime;
            float perComp = time / timeToApex;
            if (perComp >= 1)
                break;

            transform.position = Vector3.Lerp(transform.position, jumpVector, perComp);
            yield return null;
        }        
    }
    IEnumerator JumpDown()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            float perComp = time / timeToApex;
            if (perComp >= 1)
                break;

            transform.position = Vector3.Lerp(transform.position, groundVector, perComp);
            yield return null;
        }
        jumping = false;
    }

    /// <summary>
    /// Checks the player state, running , jumping etc and triggers animations
    /// </summary>
    void AnimationCheck()
    {
        if(gm.MoveSpeed!=0)
        {
            anim.SetBool("isRunning", true);
            anim.SetFloat("runSpeed", gm.MoveSpeed/10);
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetFloat("runSpeed", 1);
        }

        if(jumping)
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }

        if(ducking)
        {
            anim.SetBool("isDucking", true);
        }
        else
        {
            anim.SetBool("isDucking", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        gm.MoveSpeed -= speedPenalty;
        cm.DecreaseCrowdScore();
    }

}
