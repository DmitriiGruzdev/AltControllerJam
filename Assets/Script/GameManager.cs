using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Transform> lanes;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxMoveSpeed;
    [SerializeField] float laneWidth;
    static GameManager mInstance;

    // Start is called before the first frame update
    void Awake()
    {
        mInstance = this;
        for (int i = -1; i <=1; ++i)
        {
            Vector3 newPos = new Vector3(laneWidth * i, lanes[i + 1].position.y, lanes[i + 1].position.z);
            lanes[i + 1].position = newPos;
        }
    }

    public void GameOver()
    {

    }

    public static GameManager Get
    {
        get
        {
            return mInstance;
        }
    }

    public List<Transform> Lanes
    {
        get
        {
            return lanes;
        }
    }

    public float MoveSpeed
    {
        set
        {
            moveSpeed = Mathf.Clamp(value, 0, maxMoveSpeed);
            ScoreManager.Get.Score += Mathf.RoundToInt(moveSpeed / 10);
        }
        get
        {
            return moveSpeed;
        }
    }

    
    public float LaneWidth
    {
        get
        {
            return laneWidth;
        }
    }

}
