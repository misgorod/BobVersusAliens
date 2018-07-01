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

    public Controller(MainView view, ChangeWeaponSignal changeWeaponSignal, ReloadWeaponSignal onWeaponReload, StopReloadWeaponSignal onWeaponStopReload)
    {
        this.view = view;
        this.changeWeaponSignal = changeWeaponSignal;
        this.onWeaponReload = onWeaponReload;
        this.onWeaponStopReload = onWeaponStopReload;
    }

    public void Initialize()
    {
        changeWeaponSignal.Listen(view.ChangeWeaponImages);
        onWeaponReload.Listen(view.StartReload);
        onWeaponStopReload.Listen(view.StopReload);
    }

    public void Dispose()
    {
        changeWeaponSignal.Unlisten(view.ChangeWeaponImages);
        onWeaponReload.Unlisten(view.StartReload);
        onWeaponStopReload.Unlisten(view.StopReload);
    }
}
