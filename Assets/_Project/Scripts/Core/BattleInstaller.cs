using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Cysharp.Threading.Tasks;
using Duels.Audio;
using Duels.Effects;
using Duels.Presentation;
using Duels.UI;
using Duels.Units;

namespace Duels.Core
{
    public class BattleInstaller : MonoInstaller
    {
        [SerializeField] private BattleView _battleView;

        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioConfig _audioConfig;

        [SerializeField] private Button _restartButton;

        [SerializeField] private UnitSpawnConfig _spawnConfig;
        [SerializeField] private SpawnPoints _spawnPoints;

        public override void InstallBindings()
        {
            Container.BindInstance(this.GetCancellationTokenOnDestroy());

            BindBattleView();
            BindAudio();
            BindCore();
            BindUI();
            BindUnits();
        }

        private void BindBattleView()
        {
            Container.BindInstance(_battleView);
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
            Container.Bind<MessageSystem>().AsSingle();

            Container.BindInterfacesAndSelfTo<HealthbarPresenter>().AsSingle();

            Container.BindInterfacesAndSelfTo<BattlePresenter>().AsSingle();

            Container.BindInstance(_restartButton);
            Container.BindInterfacesAndSelfTo<GameRestarter>().AsSingle();
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