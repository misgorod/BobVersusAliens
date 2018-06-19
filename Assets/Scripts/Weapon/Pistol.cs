using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Pistol : IWeapon, ITickable
{
    private BulletHandler.Pool bulletPool;
    private Player player;
    private Settings settings;

    private float timeSinceShoot;

    public Pistol(BulletHandler.Pool bulletPool, Player player, Settings settings)
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

            var bullet = bulletPool.Spawn(settings.damage, settings.speed, settings.lifeTime);
            bullet.transform.position = player.Position;
            bullet.transform.rotation = CalculateShootRotation(mousePosition);
        }
	}

    private Quaternion CalculateShootRotation(Vector3 mousePosition)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition = player.transform.InverseTransformPoint(mousePosition);
        mousePosition.z = 0;
        mousePosition = mousePosition.normalized;
        float tmp = Vector3.SignedAngle(Vector3.right, mousePosition, Vector3.forward);
        return Quaternion.Euler(0, 0, tmp);
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
