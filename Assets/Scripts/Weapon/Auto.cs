using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Auto : IWeapon, ITickable
{
    private BulletHandler.Pool bulletPool;
    private Settings settings;

    private float timeSinceShoot;
    private int bullets;

    private bool isReloading;

    public Auto(BulletHandler.Pool bulletPool, Settings settings)
    {
        this.bulletPool = bulletPool;
        this.settings = settings;

        timeSinceShoot = 0;
        bullets = settings.bulletsInCollar;
    }

    public void Tick()
    {
        timeSinceShoot += Time.deltaTime;
    }

    public void Shoot(Vector3 position, Quaternion rotation)
    {
        if ((settings.bulletsInCollar > 0) && (timeSinceShoot > settings.shootCooldown))
        {
            bullets--;
            timeSinceShoot = 0;

                var bullet = bulletPool.Spawn(settings.bulletDamage, settings.bulletSpeed, settings.bulletLifeTime);
                bullet.transform.position = position;
                bullet.transform.rotation = rotation;
        }
    }

    public async void Reload()
    {
        isReloading = true;
        await Task.Delay((int)(settings.reloadCooldown * 1000));
        bullets = settings.bulletsInCollar;
        isReloading = false;
    }

    [System.Serializable]
    public class Settings
    {
        public float bulletDamage;
        public float bulletLifeTime;
        public float bulletSpeed;

        public int bulletsInCollar;

        public float shootCooldown;
        public float reloadCooldown;

    }
}
