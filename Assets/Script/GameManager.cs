using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //AUDIO
    AudioSource audioSource;
    [SerializeField] AudioClip gameOverSound;

    [SerializeField] List<Transform> lanes;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxMoveSpeed;
    [SerializeField] float laneWidth;
    bool gameOver;
    static GameManager mInstance;

    //UI
    [SerializeField] GameObject gameOverText;

    // Start is called before the first frame update
    void Awake()
    {
        mInstance = this;
        audioSource = GetComponent<AudioSource>();
        gameOverText.SetActive(false);
        for (int i = -1; i <=1; ++i)
        {
            Vector3 newPos = new Vector3(laneWidth * i, lanes[i + 1].position.y, lanes[i + 1].position.z);
            lanes[i + 1].position = newPos;
        }
    }

    private void Update()
    {
        ScoreManager.Get.Score += (moveSpeed / 10) * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            audioSource.Stop();
            audioSource.volume = 1;
            audioSource.PlayOneShot(gameOverSound);

            gameOverText.SetActive(true);

            StartCoroutine(WaitForResetButtonPress());
        }
    }

    IEnumerator WaitForResetButtonPress()
    {
        while(true)
        {
            if (Input.GetButtonDown("Duck"))
            {
                SceneManager.LoadSceneAsync(1);
                StopAllCoroutines();
                break;
            }

            yield return null;
        }
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
