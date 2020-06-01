using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDie
{
    ShootingController shooting;
    [SerializeField] ParticleSystem deathParticles;

    bool isDead = false;
    private void Awake()
    {
        TryGetComponent<ShootingController>(out shooting);
    }

    private void Start()
    {

        MoveToPosition(transform.position);
    }

    public void ResetEnemy()
    {
        isDead = false;
        Vector2 originalPosition= new Vector2(-transform.position.x, transform.position.y + 7);
        MoveToPosition(originalPosition);
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        
    }

    void MoveToPosition(Vector2 position)
    {
        transform.position = new Vector2(position.x * 2, position.y);
        transform.DOLocalMove(position, 10).SetSpeedBased().SetEase(Ease.OutSine);
    }

    public void ShootAtPlayer()
    {
        shooting.Shoot();
    }

    public void Dead()
    {
        isDead = true;

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

        GameManager.Instance.LevelCompleted();
    }

    public bool IsDead()
    {
        return isDead;
    }
    

}

internal interface IDie
{
    void Dead();
}