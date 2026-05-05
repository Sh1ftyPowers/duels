using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        public BattleState State;

        private GameObject _teamOneHeroPrefab;
        private GameObject _teamTwoHeroPrefab;

        private Unit _teamOneHero;
        private Unit _teamTwoHero;

        private readonly WaitForSeconds _startDelay = new WaitForSeconds(0.5f);
        private readonly WaitForSeconds _attackDelay = new WaitForSeconds(3f);

        private void Start()
        {
            _audio.PlayBattleMusic();

            State = BattleState.Start;
            StartCoroutine(SetUpBattle());
        }

        private IEnumerator SetUpBattle()
        {
            _teamOneHero = _spawner.SpawnTeamOne(this, _message);
            _teamTwoHero = _spawner.SpawnTeamTwo(this, _message);

            _battleUI.SetTurnText("The Battle Begins!");

            yield return _startDelay;

            State = BattleState.TeamOneTurn;
            StartCoroutine(BattleLoop());
        }

        private IEnumerator BattleLoop()
        {
            while (!IsBattleOver())
            {
                if (State == BattleState.TeamOneTurn)
                {
                    yield return StartCoroutine(PerformTurn(_teamOneHero, _teamTwoHero, BattleState.TeamTwoTurn));
                }
                else if (State == BattleState.TeamTwoTurn)
                {
                    yield return StartCoroutine(PerformTurn(_teamTwoHero, _teamOneHero, BattleState.TeamOneTurn));
                }
            }
        }

        private IEnumerator PerformTurn(Unit attacker, Unit defender, BattleState nextState)
        {
            _battleUI.SetTurnText(attacker.UnitName + " attacks!");
            yield return StartCoroutine(_message.WaitForMessages());

            Debug.Log("Ход: " + attacker.UnitName);

            _effects.ProcessEffects(attacker);
            _effects.ProcessEffects(defender);
            yield return StartCoroutine(_message.WaitForMessages());

            if (CheckVictory(attacker, defender))
                yield break;

            if (attacker.IsStunned)
            {
                attacker.IsStunned = false;
                State = nextState;
                yield break;
            }

            yield return _attackDelay;

            if (!IsBattleOver())
            {
                AttackResult result = attacker.PerformAttack(defender);
                yield return StartCoroutine(_message.WaitForMessages());

                if (result.Effect != null)
                {
                    _effects.ApplyEffect(defender, result.Effect);
                }

                yield return StartCoroutine(_message.WaitForMessages());
            }

            if (CheckVictory(attacker, defender))
                yield break;

            State = nextState;
        }

        private bool CheckVictory(Unit attacker, Unit defender)
        {
            if (defender.CurrentHealthPoints > 0)
                return false;

            State = attacker == _teamOneHero
                ? BattleState.TeamOneVictory
                : BattleState.TeamTwoVictory;

            attacker.PlayVictoryAnimation();
            defender.PlayDeathAnimation();

            _battleUI.SetTurnText(attacker.UnitName + " killed " + defender.UnitName + "!");
            _battleUI.SetStatusText("Glory to the Winner!");

            EndBattle();
            return true;
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
