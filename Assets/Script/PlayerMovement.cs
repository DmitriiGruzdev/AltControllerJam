using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    enum CurrentLane { LEFT, MID, RIGHT };
    CurrentLane currentLane = CurrentLane.MID;
    [SerializeField] float speedDecrement;
    [SerializeField] float speedIncrement;
    [SerializeField] float stepDistance;

    IEnumerator leftCoroutine;
    IEnumerator rightCoroutine;
    Coroutine leftFoot;
    Coroutine rightFoot;

    bool stepLeft, stepRight;
    
    static GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        leftCoroutine = WaitForStepLeft(1f, null);
        rightCoroutine = WaitForStepRight(1f, null);
        gm = GameManager.Get;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("LeftFootLeft") || Input.GetButtonDown("RightFootLeft") || Input.GetButtonDown("LeftFootMid")
            || Input.GetButtonDown("RightFootMid") || Input.GetButtonDown("LeftFootRight") || Input.GetButtonDown("RightFootRight"))
        {
            Vector3 newPos = transform.position;
            if (Input.GetButtonDown("LeftFootLeft"))
            {
                gm.LeftMoveSpeed += speedIncrement * Time.deltaTime;
                newPos.x = -gm.LaneWidth;
                if (leftFoot != null)
                    StopCoroutine(leftFoot);
                leftFoot = StartCoroutine(leftCoroutine);
                currentLane = CurrentLane.LEFT;
            }
            if (Input.GetButtonDown("RightFootLeft"))
            {
                gm.RightMoveSpeed += speedIncrement * Time.deltaTime;
                newPos.x = -gm.LaneWidth;
                if (rightFoot != null)
                    StopCoroutine(rightFoot);
                rightFoot = StartCoroutine(rightCoroutine);
                currentLane = CurrentLane.LEFT;
            }

            if (Input.GetButtonDown("LeftFootMid"))
            {
                gm.LeftMoveSpeed += speedIncrement * Time.deltaTime;
                newPos.x = 0;
                if (leftFoot != null)
                    StopCoroutine(leftFoot);
                leftFoot = StartCoroutine(leftCoroutine);
                currentLane = CurrentLane.MID;
            }
            if (Input.GetButtonDown("RightFootMid"))
            {
                gm.RightMoveSpeed += speedIncrement * Time.deltaTime;
                newPos.x = 0;
                if (rightFoot != null)
                    StopCoroutine(rightFoot);
                rightFoot = StartCoroutine(rightCoroutine);
                currentLane = CurrentLane.MID;
            }

            if (Input.GetButtonDown("LeftFootRight"))
            {
                gm.LeftMoveSpeed += speedIncrement * Time.deltaTime;
                newPos.x = gm.LaneWidth;
                if (leftFoot != null)
                    StopCoroutine(leftFoot);
                leftFoot = StartCoroutine(leftCoroutine);
                currentLane = CurrentLane.RIGHT;
            }
            if (Input.GetButtonDown("RightFootRight"))
            {
                gm.RightMoveSpeed += speedIncrement * Time.deltaTime;
                newPos.x = gm.LaneWidth;
                if (rightFoot != null)
                    StopCoroutine(rightFoot);
                rightFoot = StartCoroutine(rightCoroutine);
                currentLane = CurrentLane.RIGHT;
            }

            transform.position = newPos;
        }
    }
    
   void ResetStepLeft()
    { 
        gm.LeftMoveSpeed = 0;
    }

     void ResetStepRight()
    {
        gm.RightMoveSpeed = 0;
    }

    IEnumerator WaitForStepLeft(float timeToWait, Callback callback)
    {
        
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;

            if (time >= timeToWait)
            {
                ResetStepLeft();
                break;
            }
            yield return null;
        }
    }

    IEnumerator WaitForStepRight(float timeToWait, Callback callback)
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;

            if (time >= timeToWait)
            {
                ResetStepRight();
                break;
            }
            yield return null;
        }
    }
}
