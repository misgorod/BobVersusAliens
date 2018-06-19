using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerShootHandler : IDisposable, IInitializable
{
    IWeapon currentWeapon;
    PlayerShootSignal onPlayerShoot;

    IWeapon firstSlot;
    IWeapon secondSlot;
    IWeapon thirdSlot;

    public PlayerShootHandler(PlayerShootSignal onPlayerShoot, 
        [Inject(Id = "first slot")]
        IWeapon firstSlot,
        [Inject(Id = "second slot")]
        IWeapon secondSlot,
        [Inject(Id = "third slot")]
        IWeapon thirdSlot)
    {
        this.onPlayerShoot = onPlayerShoot;
        this.firstSlot = firstSlot;
        this.secondSlot = secondSlot;
        this.thirdSlot = thirdSlot;

        currentWeapon = firstSlot;
    }

    public void Initialize()
    {
        onPlayerShoot.Listen(Shoot);
    }
	
    public void Shoot(Vector3 mousePosition)
    {
        currentWeapon.Shoot(mousePosition);
    }

    public void Dispose()
    {
        onPlayerShoot.Unlisten(Shoot);
    }
}
