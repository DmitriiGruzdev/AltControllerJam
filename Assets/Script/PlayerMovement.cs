using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    enum CurrentLane { LEFT, MID, RIGHT };
    CurrentLane currentLane = CurrentLane.MID;
    [SerializeField] float speedDecrement;
    [SerializeField] float speedIncrement;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Get;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;
        if (Input.GetButtonDown("LeftFootLeft") )
        {
            gm.MoveSpeed += speedIncrement * Time.deltaTime;
            newPos.x = -1;
            currentLane = CurrentLane.LEFT;
        }
        if (Input.GetButtonDown("RightFootLeft"))
        {
            gm.MoveSpeed += speedIncrement * Time.deltaTime;
            newPos.x = -1;
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
            newPos.x = 1;
            currentLane = CurrentLane.RIGHT;
        }
        if (Input.GetButtonDown("RightFootRight"))
        {
            gm.MoveSpeed += speedIncrement * Time.deltaTime;
            newPos.x = 1;
            currentLane = CurrentLane.RIGHT;
        }
        transform.position = newPos;
        gm.MoveSpeed -= speedDecrement * Time.deltaTime;
    }
}
