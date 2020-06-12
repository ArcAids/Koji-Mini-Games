using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDie
{
    ShootingController shooting;
    MovementController movement;
    [SerializeField] ParticleSystem deathParticles;
    bool isDead = false;
    public void Init()
    {
        TryGetComponent(out shooting);
        TryGetComponent(out movement);
        movement.Init();
        shooting.StartShooting();
    }

    public void ResetEnemy(bool right,float height)
    {
        movement.ResetRigid();
        isDead = false;
        gameObject.SetActive(true);
        int direction = right ? 1 : -1;
        Vector2 originalPosition= new Vector2(7.5f*direction, height);
        MoveToPosition(originalPosition);
        movement.Flip();
        
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

    public void Dead(Vector2 hitPosition)
    {
        isDead = true; 

        deathParticles.transform.parent = null;
        deathParticles.transform.position = hitPosition;
        deathParticles.Play();
        movement.BlastAway();
    }

    public bool IsDead()
    {
        return isDead;
    }
    

}

internal interface IDie
{
    void Dead(Vector2 hitPosition);
}