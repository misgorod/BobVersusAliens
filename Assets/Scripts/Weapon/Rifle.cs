using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Zenject;

public class Rifle : IWeapon, ITickable
{
    private BulletHandler.Pool bulletPool;
    private Settings settings;

    private float timeSinceShoot;

    public Rifle(BulletHandler.Pool bulletPool, Settings settings)
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
            for (int i = 0; i < settings.bulletCount; i++)
            {
                var bullet = bulletPool.Spawn(settings.damage, settings.speed, settings.lifeTime);
                bullet.transform.position = position;
                bullet.transform.rotation = rotation;
            }
        }
    }

    [System.Serializable]
    public class Settings
    {
        public float damage;
        public int bulletCount;
        public float lifeTime;
        public float speed;

        public float shootCooldown;
        public float spreadDegree;
    }
}
