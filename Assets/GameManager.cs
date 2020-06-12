using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    [SerializeField] PlayerController player;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] EnemyManager enemy;
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    public void StartGame()
    {
        player.StartGame();
        enemy.NextLevel();
    }

    public void GameOver()
    {
        Invoke("ShowEndScreen", 1);
    }

    void ShowEndScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public void LevelCompleted()
    {
        StopAllCoroutines();
        levelManager.MoveToNextLevel();
        player.StartMovingToNextLocation();
        enemy.NextLevel();
    }

    IEnumerator WaitForEnemyTurn()
    {
        yield return new WaitForSeconds(1);
        if (!enemy.IsDead())
            enemy.ShootAtPlayer();
        else
            LevelCompleted();
    }

    internal void DonePlayerTurn()
    {
        StartCoroutine(WaitForEnemyTurn());
    }
}
