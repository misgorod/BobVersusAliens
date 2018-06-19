using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "BobVersusAliens/GameSettings", fileName = "GameSettings")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public GameInstaller.Settings GameInstaller;
    public WeaponSettings Weapon;

    [System.Serializable]
    public class WeaponSettings
    {
        public Pistol.Settings Pistol;
        public Rifle.Settings Rifle;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(GameInstaller);

        Container.BindInstance(Weapon.Pistol);
        Container.BindInstance(Weapon.Rifle);
    }
}
