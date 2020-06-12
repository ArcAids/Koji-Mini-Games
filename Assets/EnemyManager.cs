using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] List<Enemy> enemies;
    float height;
    bool right;
    Enemy currentEnemy;

    private void Awake()
    {
        height = 0.5f;
        right = true;
        Debug.Log("EnemyManager awake");
        foreach (var enemy in enemies)
        {
            enemy.Init();
        }
    }

    public void NextLevel()
    {
        Enemy enemy;
        int timer=0;
        do
        {
            timer++;
            enemy = enemies[Random.Range(0, enemies.Count)];
        }
        while (timer < 10 && enemy.gameObject.activeSelf );
        currentEnemy = enemy;
        enemy.ResetEnemy(right,height);
        right = !right;
        height += 7;
    }

    public bool IsDead()
    {
        if (!currentEnemy)
            return true;
        return currentEnemy.IsDead();
    }

    public void ShootAtPlayer()
    {
        if (!currentEnemy)
            GameManager.Instance.GameOver();
        currentEnemy.ShootAtPlayer();
    }
}
