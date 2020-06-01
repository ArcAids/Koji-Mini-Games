using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] PivotSpinner spinner;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Transform muzzle;
    [SerializeField] Transform aimSprite;
    [SerializeField] float fireRate=1;
    [SerializeField] int startingAmmo;
    [SerializeField] Cinemachine.CinemachineImpulseSource impulseSource;

    float nextFireTime=0;
    bool canShoot=true;

    public void StartShooting()
    {
        canShoot = true;
        aimSprite?.gameObject.SetActive(true);
        spinner?.StartSpinning();
        spinner.Speed = Random.Range(1,5);
    }

    public void StopShooting()
    {
        aimSprite?.gameObject.SetActive(false);
        spinner?.Reset();
        spinner?.StopSpinning();
        canShoot = false;
    }

    public bool Shoot()
    {
        if (!canShoot) return false;
        if (nextFireTime < Time.time)
        {
            impulseSource.GenerateImpulse();
            spinner?.StopSpinning();
            nextFireTime = Time.time + (1/fireRate);
            Instantiate(bulletPrefab, muzzle.position, muzzle.rotation, null).Shoot(transform.localScale.x>0);
            return true;
        }
        return false;
    }
}
