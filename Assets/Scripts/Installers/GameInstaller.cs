using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    [Inject]
    private Settings settings;

    public override void InstallBindings()
    {

        Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerShootHandler>().AsSingle();

        Container.DeclareSignal<PlayerShootSignal>();
        Container.DeclareSignal<ChangeWeaponSignal>();
        Container.DeclareSignal<ReloadWeaponSignal>();
        Container.DeclareSignal<StopReloadWeaponSignal>();
        Container.DeclareSignal<BulletChangeSignal>();

        Container.BindInterfacesAndSelfTo<Controller>().AsSingle();

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
