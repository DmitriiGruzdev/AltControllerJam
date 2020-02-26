using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{

    static CrowdManager mInstance;

    float crowdScore = 100f;

    // Start is called before the first frame update
    void Awake()
    {
        mInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseCrowdScore(float amount)
    {
        crowdScore -= amount;
    }

    public void IncreaseCrowdScore(float amount)
    {
        crowdScore += amount;
    }

    public static CrowdManager Get
    {
        get
        {
            return mInstance;
        }        
    }
}
