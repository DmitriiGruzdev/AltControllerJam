using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    enum CurrentLane { LEFT, MID, RIGHT };
    CurrentLane currentLane = CurrentLane.MID;

    CapsuleCollider capsuleCollider;
    [SerializeField] float duckHeight;
    [SerializeField] float standingHeight;

    //RUNNING
    [SerializeField] float stoppingSpeed;
    [SerializeField] float speedDecrement;
    [SerializeField] float speedIncrement;
    [SerializeField] float timeBeforeStop = 1f;

    bool stepped = false;

    //DUCK
    bool ducking = false;

    //JUMP
    bool jumping = false;
    [SerializeField] float timeToApex;
    [SerializeField] float hangTime;
    [SerializeField] float timeToGround;
    [SerializeField] float jumpHeight;

    static GameManager gm;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Get;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("LeftFootLeft") || Input.GetButtonDown("RightFootLeft") || Input.GetButtonDown("LeftFootMid")
            || Input.GetButtonDown("RightFootMid") || Input.GetButtonDown("LeftFootRight") || Input.GetButtonDown("RightFootRight")) && !jumping)
        {
            stepped = true;
            StopAllCoroutines();
            StartCoroutine(ResetStepped());
            Vector3 newPos = transform.position;
            if (Input.GetButtonDown("LeftFootLeft"))
            {
                gm.MoveSpeed += speedIncrement * Time.deltaTime;
                newPos.x = -gm.LaneWidth;
                currentLane = CurrentLane.LEFT;
            }
            if (Input.GetButtonDown("RightFootLeft"))
            {
                gm.MoveSpeed += speedIncrement * Time.deltaTime;
                newPos.x = -gm.LaneWidth;
                currentLane = CurrentLane.LEFT;
            }

            if (Input.GetButtonDown("LeftFootMid"))
            {
                gm.MoveSpeed += speedIncrement * Time.deltaTime;
                newPos.x = 0;
                currentLane = CurrentLane.MID;
            }
            if (Input.GetButtonDown("RightFootMid"))
            {
                gm.MoveSpeed += speedIncrement * Time.deltaTime;
                newPos.x = 0;
                currentLane = CurrentLane.MID;
            }

            if (Input.GetButtonDown("LeftFootRight"))
            {
                gm.MoveSpeed += speedIncrement * Time.deltaTime;
                newPos.x = gm.LaneWidth;
                currentLane = CurrentLane.RIGHT;
            }
            if (Input.GetButtonDown("RightFootRight"))
            {
                gm.MoveSpeed += speedIncrement * Time.deltaTime;
                newPos.x = gm.LaneWidth;
                currentLane = CurrentLane.RIGHT;
            }

            transform.position = newPos;
        }
        else if (!jumping)
        {
            if (!stepped)
            {
                gm.MoveSpeed -= stoppingSpeed * Time.deltaTime;
            }
            else
            {
                gm.MoveSpeed -= (gm.MoveSpeed)  * Time.deltaTime;
            }
        }

        if (Input.GetButtonDown("Duck"))
        {
            capsuleCollider.height = duckHeight;
            Vector3 newPos = new Vector3(capsuleCollider.center.x, (standingHeight /2f) - (duckHeight / 2f), capsuleCollider.center.z);
            capsuleCollider.center = newPos;
        }
        else if (Input.GetButtonUp("Duck"))
        {
            capsuleCollider.height = standingHeight;
            Vector3 newPos = new Vector3(capsuleCollider.center.x, (standingHeight / 2f), capsuleCollider.center.z);
            capsuleCollider.center = newPos;
        }

        if (Input.GetButtonDown("Jump") && !jumping)
        {
            jumping = true;
            StartCoroutine(Jump());
        }
    }
   
    IEnumerator ResetStepped()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            if (time >= timeBeforeStop)
            {
                stepped = false;
                break;
            }
            yield return null;
        }
    }

    IEnumerator Jump()
    {
        float time = 0;
        Vector3 jumpVector = new Vector3(transform.position.x, jumpHeight, transform.position.z);
        Vector3 groundVector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        while (time <= timeToApex)
        {
            time += Time.deltaTime;
            float perComp = time / timeToApex;
            if (perComp >= 1)
                break;
            
            transform.position = Vector3.Slerp(transform.position, jumpVector, perComp);
            yield return null;
        }
        time = 0;

        while (time <= hangTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        while (time <= timeToGround)
        {
            time += Time.deltaTime;
            float perComp = time / timeToApex;
            if (perComp >= 1)
                break;
            
            transform.position = Vector3.Slerp(transform.position, groundVector, perComp);
            yield return null;
        }
        jumping = false;
    }
}
