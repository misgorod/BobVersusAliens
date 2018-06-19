using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    [Inject]
    Settings settings;

    

    public override void InstallBindings()
    {
        switch (settings.firstSlotWeapons)
        {
            case (FirstSlotWeapons.Pistol):             
                Container.Bind<IWeapon>().WithId("first slot").To<Pistol>().AsSingle();
                break;
            default:
                Container.Bind<IWeapon>().WithId("first slot").To<Pistol>().AsSingle();
                break;

        }

        switch (settings.secondSlotWeapons)
        {
            case (SecondSlotWeapons.Rifle):
                Container.Bind<IWeapon>().WithId("second slot").To<Rifle>().AsSingle();
                break;
            default:
                Container.Bind<IWeapon>().WithId("second slot").To<Rifle>().AsSingle();
                break;

        }

        Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
        Container.BindInterfacesTo<PlayerShootHandler>().AsSingle();

        Container.DeclareSignal<PlayerShootSignal>();

        Container.BindInterfacesTo<Pistol>().AsSingle();
        Container.BindInterfacesTo<Rifle>().AsSingle();

        Container.BindMemoryPool<BulletHandler, BulletHandler.Pool>().WithInitialSize(10).FromNewComponentOnNewPrefab(settings.bulletPrefab).UnderTransformGroup("Bullets");
    }

    [System.Serializable]
    public class Settings
    {
        public FirstSlotWeapons firstSlotWeapons;
        public SecondSlotWeapons secondSlotWeapons;
        public ThirdSlotWeapons thirdSlotWeapons;

        public GameObject bulletPrefab;
    }
}
