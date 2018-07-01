using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerShootHandler : IDisposable, IInitializable, ITickable
{
    private Weapon currentWeapon;

    private PlayerShootSignal onPlayerShoot;
    private ChangeWeaponSignal onWeaponChange;

    private Weapon firstSlot;
    private Weapon secondSlot;
    private Weapon thirdSlot;

    private ShootPosition shootPosition;

    public PlayerShootHandler(PlayerShootSignal onPlayerShoot, ChangeWeaponSignal onWeaponChange,
        ShootPosition shootPosition,
        [Inject(Id = "first slot")]
        Weapon firstSlot,
        [Inject(Id = "second slot")]
        Weapon secondSlot,
        [Inject(Id = "third slot")]
        Weapon thirdSlot)
    {
        this.onPlayerShoot = onPlayerShoot;
        this.onWeaponChange = onWeaponChange;
        this.shootPosition = shootPosition;

        this.firstSlot = firstSlot;
        this.secondSlot = secondSlot;
        this.thirdSlot = thirdSlot;


    }

    public void Initialize()
    {
        onPlayerShoot.Listen(Shoot);
        onWeaponChange.Listen(ChangeWeapon);

        currentWeapon = firstSlot;
    }

    public void Tick()
    {
        firstSlot.Tick();
        secondSlot.Tick();
        thirdSlot.Tick();
    }
	
    public void Shoot(Vector3 mousePosition)
    {
        currentWeapon.Shoot(
            shootPosition.Position,
            shootPosition.CalculateShootRotation(mousePosition));
    }

    public void ChangeWeapon(WeaponType weapon)
    {
        switch (weapon)
        {
            case WeaponType.FirstSlot:
                currentWeapon = firstSlot;
                break;
            case WeaponType.SecondSlot:
                currentWeapon = secondSlot;
                break;
            case WeaponType.ThirdSlot:
                currentWeapon = thirdSlot;
                break;
        }
    }

    public void Dispose()
    {
        onPlayerShoot.Unlisten(Shoot);
        onWeaponChange.Unlisten(ChangeWeapon);
    }
}
