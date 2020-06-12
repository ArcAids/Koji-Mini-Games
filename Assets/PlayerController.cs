using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDie
{
    ShootingController shooting;
    MovementController movement;
    [SerializeField] ParticleSystem deathParticles;
    private void Awake()
    {
        TryGetComponent(out shooting);
        TryGetComponent(out movement);
        movement.Init();
    }
    public void StartGame()
    {
        shooting.StartShooting();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (shooting.Shoot())
            {
                GameManager.Instance.DonePlayerTurn();
            }
        }
    }

    public void Dead(Vector2 hitPosition)
    {
        deathParticles.transform.parent = null;
        deathParticles.transform.position = hitPosition;

        deathParticles.Play();

        StopAllCoroutines();
        shooting.StopShooting();
        movement.BlastAway();
        GameManager.Instance.GameOver();
    }

    public void StartMovingToNextLocation()
    {
        shooting.StopShooting();
        StartCoroutine(StartMoving(new Vector2(-transform.position.x,transform.position.y+7)));
    }

    IEnumerator StartMoving(Vector2 position)
    {
        while (transform.position.x != position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position,position,Time.deltaTime *20);
            yield return null;

        }
        movement.Flip();
        shooting.StartShooting();
    }
}
