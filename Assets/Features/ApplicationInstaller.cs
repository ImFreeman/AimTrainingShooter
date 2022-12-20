using Features.Input;
using Features.Player;
using Features.UIService;
using UnityEngine;
using Zenject;

public class ApplicationInstaller : MonoInstaller<ApplicationInstaller>
{
    [SerializeField] private PlayerConfig playerConfig;
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<WeaponFireMessage>();
        Container.DeclareSignal<WeaponFireHitMessage>();
        Container.DeclareSignal<MissMessage>();
        Container.DeclareSignal<StartGameMessage>();
        Container.DeclareSignal<GameOverMessage>();
        Container.DeclareSignal<PlayerScoreChangeMessage>();
        Container.DeclareSignal<SetTimeMessage>();
        Container.DeclareSignal<RestartMessage>();

        InputInstaller.Install(Container);

        Container
            .Bind<PlayerConfig>()
            .FromScriptableObject(playerConfig)
            .AsSingle();
        Container
            .Bind<GameTimeHandler>()
            .AsSingle();
        Container
            .Bind<IUIService>()
            .To<UIService>()
            .AsSingle();
        Container
            .Bind<PlayerDataHandler>()
            .AsSingle()
            .NonLazy();

        Container
            .Bind<ApplicationLauncher>()
            .AsSingle()
            .NonLazy();       
    }
}
