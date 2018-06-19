using UnityEngine;
using Zenject;

public class PlayerDiedSignal : Signal<PlayerDiedSignal> { }
public class PlayerShootSignal : Signal<PlayerShootSignal, Vector3> { }
