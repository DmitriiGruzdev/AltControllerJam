using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Transform> lanes;
    [SerializeField] float leftMoveSpeed;
    [SerializeField] float rightMoveSpeed;
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

    // Update is called once per frame
    void Update()
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

    public float LeftMoveSpeed
    {
        set
        {
            leftMoveSpeed = Mathf.Clamp(value, 0, maxMoveSpeed);
        }
        get
        {
            return leftMoveSpeed;
        }
    }

    public float RightMoveSpeed
    {
        set
        {
            rightMoveSpeed = Mathf.Clamp( value, 0, maxMoveSpeed);
        }
        get
        {
            return rightMoveSpeed;
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
