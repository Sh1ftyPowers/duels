using Cysharp.Threading.Tasks;
using Duels.Audio;
using Duels.Effects;
using Duels.Presentation;
using Duels.UI;
using Duels.Units;
using UnityEngine;
using Zenject;

namespace Duels.Core
{
    public class BattleInstaller : MonoInstaller
    {
        [SerializeField] private BattleView _battleViewPrefab;
        [SerializeField] private RestartView _restartViewPrefab;

        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioConfig _audioConfig;

        [SerializeField] private UnitSpawnConfig _spawnConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(this.GetCancellationTokenOnDestroy());

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
            Container.Bind<BattleEvents>().AsSingle();

            Container.Bind<EffectsManager>().AsSingle();

            Container.Bind<VictoryHandler>().AsSingle();

            Container.Bind<TurnHandler>().AsSingle();

            Container.Bind<BattleSystem>().AsSingle();
        }

        private void BindUI()
        {
            Container.BindInstance(_battleViewPrefab).WhenInjectedInto<UIFactory>();

            Container.BindInstance(_restartViewPrefab).WhenInjectedInto<UIFactory>();

            Container.Bind<UIFactory>().AsSingle();

            Container.BindInterfacesTo<UIInitializer>().AsSingle().NonLazy();

            Container.Bind<HealthbarPresenter>().AsSingle();

            Container.Bind<MessageSystem>().AsSingle();
        }

        private void BindUnits()
        {
            Container.BindInstance(_spawnConfig);
            Container.Bind<SpawnPoints>().FromComponentInHierarchy().AsSingle();

            Container.Bind<UnitSpawner>().AsSingle();

            Container.Bind<UnitFactory>().AsSingle();
        }
    }
}