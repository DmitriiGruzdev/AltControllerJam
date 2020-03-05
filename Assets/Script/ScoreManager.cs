using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static ScoreManager mInstance;
    float score;
    public GameObject scoreDisplay;
    public GameObject highScoreInstance;
    public float highScoreValue;

    private GameObject highScore;
    // Start is called before the first frame update
    void Awake()
    {
        mInstance = this;
        if(!GameObject.FindGameObjectWithTag("HighScore"))
        {
            GameObject newHighScore = Instantiate(highScoreInstance);
            highScore = newHighScore;
            highScore.GetComponent<HighScore>().highScore=highScoreValue;
        }
        else
        {
            highScore = GameObject.FindGameObjectWithTag("HighScore");
        }
        highScore.GetComponent<HighScore>().StartHighScore();
        highScore.GetComponent<HighScore>().UpdateDisplay();
        
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
            score = value;
        }
    }

    public void UpdateHighScore()
    {
        if(score> highScore.GetComponent<HighScore>().highScore)
        {
            highScore.GetComponent<HighScore>().NewScore(score);
        }
        
    }
}
