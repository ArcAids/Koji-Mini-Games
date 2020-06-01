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

        for (int i = 0; i < 15; i++)
        {
            PlaceNextPlatform();
        }
        score = 0;
        scoreText.text = "score:" + score;
    }

    public void PlaceNextPlatform()
    {
        score++;
        scoreText.text = "score:" + score;
        if(highScore<score)
        {
            newHighScoreMessage.SetActive(true);
            PlayerPrefs.SetInt("HighScoreZigZag", score);
            highscoreText.text = "Highscore: " + score;
        }
        if (Random.value > 0.5f)
        {
            currentPosition.x -= 4;
        }else
            currentPosition.z += 4;

        GameObject platform = platforms.InstantiatePooled();
        platform.transform.parent = transform;
        platform.transform.localPosition = currentPosition;
    }
}
