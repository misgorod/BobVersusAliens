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

        switch (settings.thirdSlotWeapons)
        {
            case (ThirdSlotWeapons.Auto):
                Container.Bind<IWeapon>().WithId("third slot").To<Auto>().AsSingle();
                break;
            default:
                Container.Bind<IWeapon>().WithId("third slot").To<Auto>().AsSingle();
                break;

        }

        Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerShootHandler>().AsSingle();

        Container.DeclareSignal<PlayerShootSignal>();


        Container.DeclareSignal<ChangeWeaponSignal>();
        Container.DeclareSignal<ReloadWeaponSignal>();
        Container.DeclareSignal<StopReloadWeaponSignal>();

        Container.BindInterfacesAndSelfTo<Controller>().AsSingle();


        Container.BindInterfacesTo<Pistol>().AsSingle();
        Container.BindInterfacesTo<Rifle>().AsSingle();
        Container.BindInterfacesTo<Auto>().AsSingle();

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
