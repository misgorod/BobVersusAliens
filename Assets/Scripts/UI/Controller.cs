using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Controller : IInitializable, IDisposable
{

    private MainView view;

    private ChangeWeaponSignal changeWeaponSignal;
    private ReloadWeaponSignal onWeaponReload;
    private StopReloadWeaponSignal onWeaponStopReload;
    private BulletChangeSignal onBulletChange;

    public Controller(MainView view, ChangeWeaponSignal changeWeaponSignal, ReloadWeaponSignal onWeaponReload, StopReloadWeaponSignal onWeaponStopReload, BulletChangeSignal onBulletChange)
    {
        this.view = view;
        this.changeWeaponSignal = changeWeaponSignal;
        this.onWeaponReload = onWeaponReload;
        this.onWeaponStopReload = onWeaponStopReload;
        this.onBulletChange = onBulletChange;

        changeWeaponSignal.Listen(view.ChangeWeaponImages);
        onWeaponReload.Listen(view.StartReload);
        onWeaponStopReload.Listen(view.StopReload);
        onBulletChange.Listen(view.ChangeAmmo);
    }

    public void Initialize()
    {
        /*changeWeaponSignal.Listen(view.ChangeWeaponImages);
        onWeaponReload.Listen(view.StartReload);
        onWeaponStopReload.Listen(view.StopReload);
        onBulletChange.Listen(view.ChangeAmmo);*/
    }

    public void Dispose()
    {
        changeWeaponSignal.Unlisten(view.ChangeWeaponImages);
        onWeaponReload.Unlisten(view.StartReload);
        onWeaponStopReload.Unlisten(view.StopReload);
        onBulletChange.Unlisten(view.ChangeAmmo);
    }
}
