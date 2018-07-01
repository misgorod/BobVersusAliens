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
        public List<Weapon.Settings> Settings;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(GameInstaller);
        BindWeaponSettings();
    }

    private void BindWeaponSettings()
    {
        foreach (var weapon in Weapon.Settings)
        {
            if (weapon.weaponName == GameInstaller.firstSlotWeapons.ToString())
            {
                Container.Bind<Weapon>().WithId("first slot").WithArguments<Weapon.Settings>(weapon).WhenInjectedInto<PlayerShootHandler>();
            }

            if (weapon.weaponName == GameInstaller.secondSlotWeapons.ToString())
            {
                Container.Bind<Weapon>().WithId("second slot").WithArguments<Weapon.Settings>(weapon).WhenInjectedInto<PlayerShootHandler>();
            }

            if (weapon.weaponName == GameInstaller.thirdSlotWeapons.ToString())
            {
                Container.Bind<Weapon>().WithId("third slot").WithArguments<Weapon.Settings>(weapon).WhenInjectedInto<PlayerShootHandler>();
            }
        }

    }
}
