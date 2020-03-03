using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CrowdManager : MonoBehaviour
{
    //ACTUAL CODE STUFF
    static CrowdManager mInstance;
    static GameManager gm;

    //AUDIO
    [SerializeField]AudioSource audioCheer;
    [SerializeField]AudioSource audioBoo;
    [SerializeField] AudioClip cheer;
    [SerializeField] AudioClip boo;

    float crowdScore = 100f;
    [SerializeField] float targetSpeed = 10f;
    [SerializeField] float scoreDecrease = 5f;
    [SerializeField] float scoreIncrease = 2f;
    [SerializeField] float timeIncrease = 0.1f;
    [SerializeField] float decreaseOnHit = 1f;

    //UI
    [SerializeField] Slider crowdScoreSlider;
    [SerializeField] float minAngle;
    [SerializeField] float maxAngle;
    float currentAngle;

    // Start is called before the first frame update
    void Awake()
    {
        mInstance = this;
        
    }

    private void Start()
    {
        gm = GameManager.Get;
        crowdScoreSlider.value = crowdScore;
        crowdScoreSlider.maxValue = 100;

        audioBoo.clip = boo;
        audioCheer.clip = cheer;
        audioBoo.Play();
        audioCheer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        audioBoo.volume = 1 - (crowdScore / 100);
        audioCheer.volume = (crowdScore / 100);
        if (crowdScore <= 0)
        {
            gm.GameOver();
        }

        if (gm.MoveSpeed < targetSpeed)
        {
            DecreaseCrowdScore(scoreDecrease * Time.deltaTime);
        }
        else
        {
            IncreaseCrowdScore(scoreIncrease * Time.deltaTime); 
        }
        targetSpeed += timeIncrease * Time.deltaTime;

        currentAngle = Mathf.Lerp(minAngle, maxAngle, crowdScore / 100f);
        crowdScoreSlider.value = crowdScore;
        
    }

    public void DecreaseCrowdScore(float amount)
    {
        crowdScore = Mathf.Clamp( crowdScore - amount, 0, 100);
    }

    public void DecreaseCrowdScore()
    {
        crowdScore = Mathf.Clamp(crowdScore - decreaseOnHit, 0, 100);
    }

    public void IncreaseCrowdScore(float amount)
    {
        crowdScore = Mathf.Clamp(crowdScore + amount, 0, 100);
    }

    public static CrowdManager Get
    {
        get
        {
            return mInstance;
        }        
    }
}
