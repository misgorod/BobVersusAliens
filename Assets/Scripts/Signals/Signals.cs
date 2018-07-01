using UnityEngine;
using Zenject;

public class PlayerDiedSignal : Signal<PlayerDiedSignal> { }
public class PlayerShootSignal : Signal<PlayerShootSignal, Vector3> { }
public class ChangeWeaponSignal : Signal<ChangeWeaponSignal, Weapons> { }
public class ReloadWeaponSignal : Signal<ReloadWeaponSignal, Weapons, float> { }
public class StopReloadWeaponSignal : Signal<StopReloadWeaponSignal, Weapons> { }
