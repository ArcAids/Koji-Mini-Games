using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Transform muzzle;
    [SerializeField] Transform aimSprite;
    PivotSpinner spinner;
    [SerializeField] float fireRate=1;
    [SerializeField] int startingAmmo;
    [SerializeField] Cinemachine.CinemachineImpulseSource impulseSource;

    float nextFireTime=0;
    bool canShoot=false;

    private void Awake()
    {
        nextFireTime = Time.time + 1;
    }
    public void StartShooting()
    {
        canShoot = true;
        if(spinner==null)
            TryGetComponent(out spinner);
        if(aimSprite!=null)
            aimSprite.gameObject.SetActive(true);
        if (spinner!=null)
        {
            spinner?.StartSpinning();
            spinner.Speed = Random.Range(1,5);
        }
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
            if(aimSprite!=null)
                aimSprite.gameObject.SetActive(false);
            Instantiate(bulletPrefab, muzzle.position, muzzle.rotation, null).Shoot(transform.localScale.x>0);
            return true;
        }
        return false;
    }
}
