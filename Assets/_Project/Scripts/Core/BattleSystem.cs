using UnityEngine;
using UnityEngine.SceneManagement;
using Duels.Units;
using Duels.UI;
using Duels.Effects;
using Duels.Audio;
using Duels.Attacks;
using Cysharp.Threading.Tasks;

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

        public BattleState State;

        private Unit _teamOneHero;
        private Unit _teamTwoHero;

        private VictoryHandler _victoryHandler;

        private int _startDelay = 500;
        private int _atackDelay = 3000;

        private void Start()
        {
            _audio.PlayBattleMusic();

            _victoryHandler = new VictoryHandler(_battleUI, _gameOverCanvas);

            State = BattleState.Start;
            
            SetUpBattle().Forget();
        }

        private async UniTask SetUpBattle()
        {
            _teamOneHero = _spawner.SpawnTeamOne(this, _message);
            _teamTwoHero = _spawner.SpawnTeamTwo(this, _message);

            _battleUI.SetTurnText("The Battle Begins!");

            await UniTask.Delay(_startDelay);

            State = BattleState.TeamOneTurn;

            StartBattleLoop().Forget();
        }

        private async UniTask StartBattleLoop()
        {
            while (!IsBattleOver())
            {
                if (State == BattleState.TeamOneTurn)
                {
                    await PerformTurn(_teamOneHero, _teamTwoHero, BattleState.TeamTwoTurn);
                }
                else if (State == BattleState.TeamTwoTurn)
                {
                    await PerformTurn(_teamTwoHero, _teamOneHero, BattleState.TeamOneTurn);
                }
            }
        }

        private async UniTask PerformTurn(Unit attacker, Unit defender, BattleState nextState)
        {
            _battleUI.SetTurnText(attacker.UnitName + " attacks!");
            await _message.WaitForMessages();

            Debug.Log("Ход: " + attacker.UnitName);

            _effects.ProcessEffects(attacker);
            _effects.ProcessEffects(defender);
            await _message.WaitForMessages();

            if (_victoryHandler.CheckVictory(attacker, defender))
            {
                State = attacker == _teamOneHero
                    ? BattleState.TeamOneVictory
                    : BattleState.TeamTwoVictory;

                EndBattle();
                return;
            }

            if (attacker.IsStunned)
            {
                attacker.IsStunned = false;
                State = nextState;
                return;
            }

            await UniTask.Delay(_atackDelay);

            if (!IsBattleOver())
            {
                AttackResult result = attacker.PerformAttack(defender);
                await _message.WaitForMessages();

                if (result.Effect != null)
                {
                    _effects.ApplyEffect(defender, result.Effect);
                }

                await _message.WaitForMessages();
            }

            if (_victoryHandler.CheckVictory(attacker, defender))
            {
                State = attacker == _teamOneHero
                    ? BattleState.TeamOneVictory
                    : BattleState.TeamTwoVictory;

                EndBattle();
                return;
            }

            State = nextState;
        }

        private bool IsBattleOver()
        {
            return State == BattleState.TeamOneVictory || State == BattleState.TeamTwoVictory;
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
