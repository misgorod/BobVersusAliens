using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Controller : IInitializable, IDisposable
{

    MainView view;

    PlayerShootHandler playerShootHandler;

    ChangeWeaponSignal changeWeaponSignal;

    public Controller(MainView view, PlayerShootHandler playerShootHandler, ChangeWeaponSignal changeWeaponSignal)
    {
        this.view = view;
        this.playerShootHandler = playerShootHandler;
        this.changeWeaponSignal = changeWeaponSignal;
    }

    public void Initialize()
    {
        changeWeaponSignal.Listen(ChangeWeapon);
    }

    public void ChangeWeapon(Weapons weapon)
    {
        playerShootHandler.ChangeWeapon(weapon);
        view.ChangeWeaponImages(weapon);
    }

    public void Foo()
    {

    }

    public void Dispose()
    {
        changeWeaponSignal.Unlisten(ChangeWeapon);
    }
}
