using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Pistol : IWeapon, ITickable
{
    private BulletHandler.Pool bulletPool;
    private Settings settings;

    private float timeSinceShoot;

    public Pistol(BulletHandler.Pool bulletPool, Settings settings)
    {
        this.bulletPool = bulletPool;
        this.settings = settings;

        timeSinceShoot = 0;
    }

    public void Tick()
    {
        timeSinceShoot += Time.deltaTime;
    }

    public void Shoot(Vector3 position, Quaternion rotation)
    {
        if (timeSinceShoot > settings.shootCooldown)
        {
            timeSinceShoot = 0;

            var bullet = bulletPool.Spawn(settings.damage, settings.speed, settings.lifeTime);
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
        }
    }

    [System.Serializable]
    public class Settings
    {
        public float damage;
        public float lifeTime;
        public float speed;

        public float shootCooldown;

    }
}
