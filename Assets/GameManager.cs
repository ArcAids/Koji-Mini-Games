using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    [SerializeField] PlayerController player;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Enemy enemy;
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void LevelCompleted()
    {
        StopAllCoroutines();
        levelManager.MoveToNextLevel();
        player.StartMovingToNextLocation();
        enemy.ResetEnemy();
    }

    IEnumerator WaitForEnemyTurn()
    {
        yield return new WaitForSeconds(1);
        if (!enemy.IsDead())
            enemy.ShootAtPlayer();
    }

    internal void DonePlayerTurn()
    {
        StartCoroutine(WaitForEnemyTurn());
    }
}
