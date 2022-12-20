using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyInstaller : MonoInstaller<EnemyInstaller>
    {
        [SerializeField] private EnemyView enemyView;
        [SerializeField] private EnemyConfig config;
        public override void InstallBindings()
        {
            Container
                .Bind<IEnemyController>()
                .To<EnemyController>()
                .AsSingle();
            Container
                .Bind<EnemyConfig>()
                .FromScriptableObject(config)
                .AsSingle();
            Container
                .Bind<IEnemyWaveController>()
                .To<EnemyWaveController>()
                .AsSingle()
                .NonLazy();
            Container
                .BindMemoryPool<EnemyView, EnemyView.Pool>()
                .WithInitialSize(5)
                .FromComponentInNewPrefab(enemyView)
                .UnderTransformGroup("Enemies");
        }
    }
}