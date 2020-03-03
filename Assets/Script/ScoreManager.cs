using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static ScoreManager mInstance;
    float score;

    // Start is called before the first frame update
    void Awake()
    {
        mInstance = this;
    }

    public static ScoreManager Get
    {
        get
        {
            return mInstance;
        }
    }
    public float Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;Debug.Log(score);
        }
    }
}
