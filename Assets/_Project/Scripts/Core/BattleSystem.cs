using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using Duels.Units;
using Duels.UI;
using Duels.Effects;
using Duels.Audio;
using Duels.Attacks;

namespace Duels.Core
{
    public class BattleSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverCanvas;
        [SerializeField] private EffectsManager _effects;
        [SerializeField] private BattleUI _battleUI;
        [SerializeField] private MessageSystem _message;
        [SerializeField] private UnitSpawner _spawner;
        [SerializeField] private AudioManager _audio;

        private BattleState _state;

        private VictoryHandler _victoryHandler;

        private Unit _teamOneHero;
        private Unit _teamTwoHero;

        private Unit _firstTurnUnit;
        private Unit _secondTurnUnit;

        private const int StartDelay = 500;
        private const int AttackDelay = 3000;

        private async UniTaskVoid Start()
        {
            _audio.PlayBattleMusic();

            _victoryHandler = new VictoryHandler(_battleUI, _gameOverCanvas);

            _state = BattleState.Start;
            
            await SetUpBattle(this.GetCancellationTokenOnDestroy());
        }

        private async UniTask SetUpBattle(CancellationToken cancellationToken)
        {
            int turnDecider = Random.Range(0, 2);

            _teamOneHero = _spawner.SpawnTeamOne(this, _message);
            _teamTwoHero = _spawner.SpawnTeamTwo(this, _message);

            if (turnDecider == 0)
            {
                _firstTurnUnit = _teamOneHero;
                _secondTurnUnit = _teamTwoHero;
            }
            else
            {
                _firstTurnUnit = _teamTwoHero;
                _secondTurnUnit = _teamOneHero;
            }

            _battleUI.SetTurnText("The Battle Begins!");

            await UniTask.Delay(StartDelay, cancellationToken: cancellationToken);

            _state = BattleState.TeamOneTurn;

            await StartBattleLoop(cancellationToken);
        }

        private async UniTask StartBattleLoop(CancellationToken cancellationToken)
        {
            while (!IsBattleOver())
            {
                if (_state == BattleState.TeamOneTurn)
                {
                    await PerformTurn(_firstTurnUnit, _secondTurnUnit, BattleState.TeamTwoTurn, cancellationToken);
                }
                else if (_state == BattleState.TeamTwoTurn)
                {
                    await PerformTurn(_secondTurnUnit, _firstTurnUnit, BattleState.TeamOneTurn, cancellationToken);
                }
            }
        }

        private async UniTask PerformTurn(Unit attacker, Unit defender, BattleState nextState, CancellationToken cancellationToken)
        {
            _battleUI.SetTurnText(attacker.UnitName + " attacks!");
            await _message.WaitForMessages(cancellationToken);

            Debug.Log("Ход: " + attacker.UnitName);

            _effects.ProcessEffects(attacker);
            _effects.ProcessEffects(defender);
            await _message.WaitForMessages(cancellationToken);

            if (_victoryHandler.CheckVictory(attacker, defender))
            {
                _state = attacker == _teamOneHero
                    ? BattleState.TeamOneVictory
                    : BattleState.TeamTwoVictory;

                EndBattle();
                return;
            }

            if (attacker.Effects.HasEffect<StunningAttack>())
            {
                _state = nextState;

                return;
            }

            await UniTask.Delay(AttackDelay, cancellationToken: cancellationToken);

            if (!IsBattleOver())
            {
                AttackResult result = attacker.PerformAttack(defender);
                await _message.WaitForMessages(cancellationToken);

                if (result.Effect != null)
                {
                    _effects.ApplyEffect(defender, result.Effect);
                }

                await _message.WaitForMessages(cancellationToken);
            }

            if (_victoryHandler.CheckVictory(attacker, defender))
            {
                _state = attacker == _teamOneHero
                    ? BattleState.TeamOneVictory
                    : BattleState.TeamTwoVictory;

                EndBattle();
                return;
            }

            _state = nextState;
        }

        private bool IsBattleOver()
        {
            return _state == BattleState.TeamOneVictory || _state == BattleState.TeamTwoVictory;
        }

        private void EndBattle()
        {
            StartCoroutine(_audio.PlayEndBattleMusic());
            _gameOverCanvas.SetActive(true);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}