using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDie
{
    ShootingController shooting;
    [SerializeField] ParticleSystem deathParticles;
    private void Awake()
    {
        TryGetComponent<ShootingController>(out shooting);
        
    }
    private void Start()
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

    public void Dead()
    {
        deathParticles.transform.parent = null;
        deathParticles.transform.position = transform.position;
        deathParticles.transform.localScale = transform.localScale;
        //Quaternion rotation= deathParticles.transform.localRotation;
        //if(transform.localScale.x<0)
        //    rotation.eulerAngles.Set(rotation.eulerAngles.x,180,rotation.eulerAngles.z);
        //else
        //    rotation.eulerAngles.Set(rotation.eulerAngles.x,0,rotation.eulerAngles.z);
        //deathParticles.transform.localRotation = rotation;
        deathParticles.Play();

        StopAllCoroutines();
        shooting.StopShooting();
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
            transform.position = Vector2.MoveTowards(transform.position,position,Time.deltaTime *10);
            yield return null;

        }
        transform.localScale = new Vector3(-transform.localScale.x,1,1);
        shooting.StartShooting();
    }
}
