using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    enum CurrentLane { LEFT, MID, RIGHT };
    CurrentLane currentLane = CurrentLane.MID;
    [SerializeField] float stoppingSpeed;
    [SerializeField] float speedDecrement;
    [SerializeField] float speedIncrement;
    [SerializeField] float timeBeforeStop = 1f;

    bool stepped = false;
    
    static GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Get;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("LeftFootLeft") || Input.GetButtonDown("RightFootLeft") || Input.GetButtonDown("LeftFootMid")
            || Input.GetButtonDown("RightFootMid") || Input.GetButtonDown("LeftFootRight") || Input.GetButtonDown("RightFootRight"))
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
        else
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
}
