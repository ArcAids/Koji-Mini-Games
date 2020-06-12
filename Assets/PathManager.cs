using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField] ObjectPool platforms;
    [SerializeField] TMPro.TMP_Text scoreText;
    [SerializeField] TMPro.TMP_Text highscoreText;
    [SerializeField] GameObject newHighScoreMessage;
    int score;
    Vector3 currentPosition;
    int highScore;
    float difficulty=0;
    private void Awake()
    {
        platforms.Init();
        highScore = PlayerPrefs.GetInt("HighScoreZigZag",0);
        highscoreText.text = "Highscore: " + highScore;
    }
    private void Start()
    {
        for (int i = -4; i < 5; i+=4)
        {
            for (int j = -4; j < 5; j+=4)
            {
                GameObject platform=platforms.InstantiatePooled();
                platform.transform.parent = transform;
                platform.transform.localPosition = new Vector3(i,0,j);
            }
        }
        currentPosition.x = -4;
        currentPosition.y = 0;
        currentPosition.z = 4;

        StartCoroutine(StartLayingPlatforms());
        score = 0;
        scoreText.text = "score:" + score;
    }

    IEnumerator StartLayingPlatforms()
    {
        for (int i = 0; i < 5; i++)
        {
            PlaceNextPlatform();
            yield return new WaitForSeconds(0.1f);
        }
    }

    Vector2 lastScale=new Vector2(4,4);
    public void PlaceNextPlatform()
    {
        UpdateScores();

        Platform platform = platforms.InstantiatePooled<Platform>();
        platform.transform.parent = transform;


        if (Random.value < 0.5f)
        {
            currentPosition.x -= lastScale.x*0.5f;

            Vector3 scale=platform.transform.localScale;
            scale.x = Random.Range(4 , 10);
            platform.transform.localScale = scale;
            currentPosition.x -= scale.x * 0.5f;

            currentPosition.z += (lastScale.y - platform.RandomizeZScale(difficulty)) * 0.5f;
        }
        else
        {
            currentPosition.z += lastScale.y * 0.5f;

            Vector3 scale = platform.transform.localScale;
            scale.z =  Random.Range(4, 10);
            platform.transform.localScale = scale;
            currentPosition.z += scale.z * 0.5f;

            currentPosition.x -= (lastScale.x - platform.RandomizeXScale(difficulty)) * 0.5f;
        }


        lastScale.x= platform.transform.localScale.x;
        lastScale.y= platform.transform.localScale.z;
        platform.transform.localPosition = currentPosition;
        platform.SpawnAnimation();
    }

    void UpdateScores()
    {
        score++;
        if (score%25==0) difficulty = Mathf.Clamp(difficulty+0.2f,0,1.5f);
        scoreText.text = "score:" + score;
        if (highScore < score)
        {
            newHighScoreMessage.SetActive(true);
            PlayerPrefs.SetInt("HighScoreZigZag", score);
            highscoreText.text = "Highscore: " + score;
        }
    }
}
