using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public float highScore;
    private GameObject highScoreDisplay;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void NewScore(float _newScore)
    {
        highScore = _newScore;
        highScoreDisplay.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(highScore).ToString();

    }

    public void UpdateDisplay()
    {
        if (highScore != 0)
        {
            highScoreDisplay.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(highScore).ToString();
        }
    }

    public void StartHighScore()
    {
        highScoreDisplay = GameObject.FindGameObjectWithTag("HSDisplay");
      
    }

}
