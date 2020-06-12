using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<GameLevel> levels;
    [SerializeField] CameraTarget cameraTarget;
    [SerializeField] TMPro.TMP_Text currentLevelText;
    [SerializeField] TMPro.TMP_Text highestLevelText;
    [SerializeField] GameObject highestScoreText;
    [SerializeField] GameObject gameOverText;
    
    float currentHeight=-28;
    int currentLevelIndex = 0;
    int recordKills;
    private void Start()
    {
        foreach (var level in levels)
        {
            level.transform.position = Vector2.up * currentHeight ;
            //level.SetIndex(currentLevelIndex);
            currentHeight+=7;
        }
        UpdateLevels();
        recordKills = PlayerPrefs.GetInt("RecordKills", 0);
        currentLevelText.text = "Kills: 0";
        highestLevelText.text = "Record Kills: " + recordKills;
    }

    public void MoveToNextLevel()
    {
        cameraTarget.MoveHigher(7);
        GameLevel level=levels[0];
        levels.RemoveAt(0);
        level.transform.position = Vector2.up * currentHeight;
        currentHeight += 7;
        levels.Add(level);
        currentLevelIndex++;
        currentLevelText.text = "Kills: "+currentLevelIndex;

        if (currentLevelIndex > recordKills)
        {
            highestScoreText.SetActive(true);
            gameOverText.SetActive(false);
            highestLevelText.text = "Record Kills: " + currentLevelIndex;
            PlayerPrefs.SetInt("RecordKills", currentLevelIndex);
        }

        UpdateLevels();
    }

    void UpdateLevels()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].SetIndex(i);
        }
    }
}
