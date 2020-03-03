using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static ScoreManager mInstance;
    float score;
    public GameObject scoreDisplay;

    // Start is called before the first frame update
    void Awake()
    {
        mInstance = this;
    }


    public void Update()
    {
        scoreDisplay.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(score).ToString();
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
