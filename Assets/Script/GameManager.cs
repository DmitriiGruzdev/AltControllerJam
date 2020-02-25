using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Transform> lanes;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxMoveSpeed;
    static GameManager mInstance;

    // Start is called before the first frame update
    void Awake()
    {
        mInstance = this;
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
    public float MoveSpeed
    {
        set
        {
            moveSpeed = Mathf.Clamp( value, 0, maxMoveSpeed);
        }
        get
        {
            return moveSpeed;
        }
    }

}
