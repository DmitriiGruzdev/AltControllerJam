using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        //UpdateTxtFile();

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
        //highScore = ReadTxtFile();
        highScoreDisplay = GameObject.FindGameObjectWithTag("HSDisplay");
       

    }

    void UpdateTxtFile()
    {
        string path = Application.persistentDataPath + "/HighScore.txt";

        //add some to text to it

        File.WriteAllText(path, Mathf.RoundToInt(highScore).ToString());
      
        
    }

    float ReadTxtFile()
    {
        string path = Application.persistentDataPath + "/HighScore.txt";
        StreamReader reader = new StreamReader(path);
        return float.Parse(reader.ReadToEnd());
    }

}
