using System.Threading;
using UnityEngine;
using Zenject;
using Duels.Audio;
using Duels.Effects;
using Duels.Presentation;
using Duels.UI;
using Duels.Units;

namespace Duels.Core
{
    public class BattleInstaller : MonoInstaller
    {
        [SerializeField] private BattleView _battleViewPrefab;

        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioConfig _audioConfig;

        [SerializeField] private UnitSpawnConfig _spawnConfig;

        public override void InstallBindings()
        {
            BindAudio();
            BindCore();
            BindUI();
            BindUnits();
        }

        private void BindAudio()
        {
            Container.BindInstance(_audioConfig);
            Container.BindInstance(_musicSource);

            Container.BindInterfacesAndSelfTo<AudioManager>().AsSingle();
        }

        private void BindCore()
        {
            Container.BindInterfacesAndSelfTo<RewardService>().AsSingle();

            Container.BindInterfacesAndSelfTo<Wallet>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameLifetime>().AsSingle();

            Container.Bind<CancellationToken>().FromResolveGetter<GameLifetime>(x => x.Token).AsSingle();

            Container.Bind<BattleEvents>().AsSingle();

            Container.Bind<EffectsManager>().AsSingle();

            Container.Bind<VictoryHandler>().AsSingle();

            Container.Bind<TurnHandler>().AsSingle();

            Container.Bind<BattleSystem>().AsSingle();
        }

        private void BindUI()
        {
            Container.BindInstance(_battleViewPrefab).WhenInjectedInto<UIFactory>();

            Container.Bind<UIFactory>().AsSingle();

            Container.Bind<BattleView>().FromMethod(ctx => ctx.Container.Resolve<UIFactory>().CreateBattleCanvas()).AsCached();

            Container.BindInterfacesAndSelfTo<HealthbarPresenter>().AsSingle();

            Container.Bind<MessageSystem>().AsSingle();

            Container.BindInterfacesAndSelfTo<BattlePresenter>().AsSingle();
        }

        private void BindUnits()
        {
            Container.BindInstance(_spawnConfig);

            Container.Bind<SpawnPoints>().FromComponentInHierarchy().AsSingle();

            Container.Bind<UnitSpawner>().AsSingle();

            Container.Bind<UnitFactory>().AsSingle();

            Container.Bind<DamageCalculator>().AsSingle();
        }
    }
}