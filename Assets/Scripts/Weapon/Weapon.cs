using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public enum WeaponType
{
    FirstSlot,
    SecondSlot,
    ThirdSlot
}

public enum FirstSlotWeapons
{
    Pistol
}

public enum SecondSlotWeapons
{
    Shotgun
}

public enum ThirdSlotWeapons
{
    Auto
}

public enum ReloadType
{
    FullCollar,
    OneBullet
}

public class Weapon : ITickable//, IInitializable
{
    protected ReloadWeaponSignal onWeaponReload;
    protected StopReloadWeaponSignal onStopReload;

    protected BulletHandler.Pool bulletPool;
    protected Settings settings;

    protected float timeSinceShoot;
    protected int bullets;

    protected bool isReloading;

    protected CancellationTokenSource reloadCancellatonToken;

    public Weapon(ReloadWeaponSignal onWeaponReload, StopReloadWeaponSignal onStopReload, BulletHandler.Pool bulletPool, Settings settings)
    {
        this.onWeaponReload = onWeaponReload;
        this.onStopReload = onStopReload;
        this.bulletPool = bulletPool;
        this.settings = settings;

        timeSinceShoot = 0;
        isReloading = false;
        bullets = settings.bulletsInCollar;
        reloadCancellatonToken = new CancellationTokenSource();
    }

    public virtual void Initialize()
    {
        
    }

    public virtual void Tick()
    {
        timeSinceShoot += Time.deltaTime;

        if ((timeSinceShoot > 4 && settings.bulletsInCollar > bullets && !isReloading) || ((bullets == 0) && !isReloading))
        {
            Reload();
        }
    }

    public virtual void Shoot(Vector3 position, Quaternion rotation)
    {
        if ((bullets > 0) && (timeSinceShoot > settings.shootCooldown))
        {
            CancelReload();

            bullets--;
            timeSinceShoot = 0;

            for (int i = 0; i < settings.bulletsPerShot; i++)
            {
                var bullet = bulletPool.Spawn(settings.bulletDamage, settings.bulletSpeed, settings.bulletLifeTime);
                bullet.transform.position = position;
                float spread = Random.Range(-settings.spreadDegree, settings.spreadDegree);
                bullet.transform.rotation = Quaternion.Euler(rotation.eulerAngles + new Vector3(0, 0, spread));
            }
        }
    }

    public virtual async void Reload()
    {
        isReloading = true;

        onWeaponReload.Fire(settings.weaponType, settings.reloadCooldown);

        try
        {
            await Task.Delay((int)(settings.reloadCooldown * 1000), reloadCancellatonToken.Token);
            switch (settings.reloadType)
            {
                case ReloadType.FullCollar:
                    bullets = settings.bulletsInCollar;
                    break;
                case ReloadType.OneBullet:
                    bullets++;
                    break;
            }
            
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

    protected virtual void CancelReload()
    {
        onStopReload.Fire(settings.weaponType);

        reloadCancellatonToken.Cancel();
        reloadCancellatonToken.Dispose();
        reloadCancellatonToken = new CancellationTokenSource();
    }

    [System.Serializable]
    public class Settings
    {
        public string weaponName;
        public WeaponType weaponType;
        public ReloadType reloadType;

        public float bulletDamage;
        public float bulletLifeTime;
        public float bulletSpeed;
        public int bulletsPerShot;

        public int bulletsInCollar;

        public float shootCooldown;
        public float reloadCooldown;

        public float spreadDegree;

    }

}


