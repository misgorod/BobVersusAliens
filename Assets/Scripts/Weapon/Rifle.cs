using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Zenject;

public class Rifle : IWeapon, ITickable
{
    private BulletHandler.Pool bulletPool;
    private Player player;
    private Settings settings;

    private float timeSinceShoot;

    public Rifle(BulletHandler.Pool bulletPool, Player player, Settings settings)
    {
        this.bulletPool = bulletPool;
        this.player = player;
        this.settings = settings;
        timeSinceShoot = 0;
    }

    public void Tick()
    {
        timeSinceShoot += Time.deltaTime;
    }

    public void Shoot(Vector3 mousePosition)
    {
        if (timeSinceShoot > settings.shootCooldown)
        {
            timeSinceShoot = 0;
            for (int i = 0; i < settings.bulletCount; i++)
            {
                Debug.Log(settings.speed);
                var bullet = bulletPool.Spawn(settings.damage, settings.speed, settings.lifeTime);
                bullet.transform.position = player.Position;
                bullet.transform.rotation = CalculateShootRotation(mousePosition);
            }
        }
    }

    private Quaternion CalculateShootRotation(Vector3 mousePosition)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition = player.transform.InverseTransformPoint(mousePosition);
        mousePosition.z = 0;
        mousePosition = mousePosition.normalized;
        float zAngle = Vector3.SignedAngle(Vector3.right, mousePosition, Vector3.forward) + Random.Range(-settings.spreadDegree, settings.spreadDegree);
        return Quaternion.Euler(0, 0, zAngle);
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
