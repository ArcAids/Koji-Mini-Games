using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<GameLevel> levels;
    [SerializeField] CameraTarget cameraTarget;
    [SerializeField] TMPro.TMP_Text currentLevelText;

    float currentHeight=-14;
    int currentLevelIndex = 0;
    private void Start()
    {
        foreach (var level in levels)
        {
            level.transform.position = Vector2.up * currentHeight ;
            //level.SetIndex(currentLevelIndex);
            currentHeight+=7;
        }
        UpdateLevels();
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
        currentLevelText.text = "Level: "+currentLevelIndex;
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
