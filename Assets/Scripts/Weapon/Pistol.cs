using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Pistol : IWeapon, ITickable, IInitializable
{
    private ReloadWeaponSignal onWeaponReload;
    private StopReloadWeaponSignal onStopReload;

    private BulletHandler.Pool bulletPool;
    private Settings settings;

    private float timeSinceShoot;
    private int bullets;

    private bool isReloading;

    private CancellationTokenSource reloadCancellatonToken;

    public Pistol(ReloadWeaponSignal onWeaponReload, StopReloadWeaponSignal onStopReload, BulletHandler.Pool bulletPool, Settings settings)
    {
        this.onWeaponReload = onWeaponReload;
        this.onStopReload = onStopReload;
        this.bulletPool = bulletPool;
        this.settings = settings;
        
    }

    public void Initialize()
    {
        timeSinceShoot = 0;
        isReloading = false;
        bullets = settings.bulletsInCollar;
        reloadCancellatonToken = new CancellationTokenSource();
    }

    public void Tick()
    {
        timeSinceShoot += Time.deltaTime;

        if ((timeSinceShoot > 4 && settings.bulletsInCollar > bullets && !isReloading) || ((bullets == 0) && !isReloading))
        {
            Reload();
        }
    }

    public void Shoot(Vector3 position, Quaternion rotation)
    {
        if ((bullets > 0) && (timeSinceShoot > settings.shootCooldown))
        {
            CancelReload();

            bullets--;
            Debug.Log(bullets);
            timeSinceShoot = 0;

            var bullet = bulletPool.Spawn(settings.bulletDamage, settings.bulletSpeed, settings.bulletLifeTime);
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
        }
    }

    public async void Reload()
    {
        isReloading = true;

        onWeaponReload.Fire(settings.weaponType, settings.reloadCooldown);

        try
        {
            await Task.Delay((int)(settings.reloadCooldown * 1000), reloadCancellatonToken.Token);
            bullets = settings.bulletsInCollar;
        }
        catch (TaskCanceledException tce)
        {
            //Reload cancelled
        }
        finally
        {
            isReloading = false;
        }
    }

    private void CancelReload()
    {
        onStopReload.Fire(settings.weaponType);

        reloadCancellatonToken.Cancel();
        reloadCancellatonToken.Dispose();
        reloadCancellatonToken = new CancellationTokenSource();
    }

    [System.Serializable]
    public class Settings
    {
        public Weapons weaponType;

        public float bulletDamage;
        public float bulletLifeTime;
        public float bulletSpeed;

        public int bulletsInCollar;

        public float shootCooldown;
        public float reloadCooldown;

    }
}
