using UnityEngine;
using Zenject;

public class PlayerDiedSignal : Signal<PlayerDiedSignal> { }
public class PlayerShootSignal : Signal<PlayerShootSignal, Vector3> { }
public class ChangeWeaponSignal : Signal<ChangeWeaponSignal, WeaponType> { }
public class ReloadWeaponSignal : Signal<ReloadWeaponSignal, WeaponType, float> { }
public class StopReloadWeaponSignal : Signal<StopReloadWeaponSignal, WeaponType> { }
public class BulletChangeSignal : Signal<BulletChangeSignal, WeaponType, int> { }
