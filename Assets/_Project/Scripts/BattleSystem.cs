using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public enum BattleState { Start, TeamOneTurn, TeamTwoTurn, TeamOneVictory, TeamTwoVictory }

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject[] teamOnePrefabs;
    [SerializeField] private GameObject[] teamTwoPrefabs;

    [SerializeField] private BattleUI _battleUI;
    [SerializeField] private MessageSystem _message;
    [SerializeField] private UnitSpawner _spawner;
    [SerializeField] private AudioManager _audio;

    public BattleState State;

    private GameObject _teamOneHeroPrefab;
    private GameObject _teamTwoHeroPrefab;

    private Unit _teamOneHero;
    private Unit _teamTwoHero;

    private bool _isTurnInProgress = false;

    private int _teamOneHeroID;
    private int _teamTwoHeroID;

    void Start()
    {
        _teamOneHeroID = UnityEngine.Random.Range(0, teamOnePrefabs.Length);
        _teamOneHeroPrefab = teamOnePrefabs[_teamOneHeroID];

        _teamTwoHeroID = UnityEngine.Random.Range(0, teamTwoPrefabs.Length);
        _teamTwoHeroPrefab = teamTwoPrefabs[_teamTwoHeroID];

        _audio.PlayBattleMusic();

        State = BattleState.Start;
        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle()
    {
        _teamOneHero = _spawner.SpawnTeamOne(_teamOneHeroPrefab, this, _message);
        _teamTwoHero = _spawner.SpawnTeamTwo(_teamTwoHeroPrefab, this, _message);

        _battleUI.SetTurnText("The Battle Begins!");

        //yield return new WaitForSeconds(2f);
        yield return new WaitForSeconds(0.5f);

        State = BattleState.TeamOneTurn;
        StartCoroutine(BattleLoop());
    }

    IEnumerator BattleLoop()
    {
        while (State != BattleState.TeamOneVictory && State != BattleState.TeamTwoVictory)
        {
            if (_isTurnInProgress) yield return null;

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

    IEnumerator PerformTurn(Unit attacker, Unit defender, BattleState nextState)
    {
        if (_isTurnInProgress) yield break;
        _isTurnInProgress = true;

        _battleUI.SetTurnText(attacker.unitName + " attacks!");
        yield return StartCoroutine(_message.WaitForMessages());
        Debug.Log("Ход: " + attacker.unitName);

        attacker.ProcessEffects();
        defender.ProcessEffects();
        yield return StartCoroutine(_message.WaitForMessages());

        if (defender.currentHealthPoints <= 0)
        {
            State = attacker == _teamOneHero ? BattleState.TeamOneVictory : BattleState.TeamTwoVictory;
            EndBattle();
            _isTurnInProgress = false;
            yield break;
        }

        if (attacker.isStunned)
        {
            //Debug.Log(attacker.unitName + " is stunned. His turn is skipped");

            attacker.isStunned = false;
            State = nextState;

            _isTurnInProgress = false;
            yield break;
        }

        yield return new WaitForSeconds(5f);

        if (State != BattleState.TeamOneVictory && State != BattleState.TeamTwoVictory)
        {
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(_message.WaitForMessages());

            attacker.PerformAttack(defender);
            yield return StartCoroutine(_message.WaitForMessages());
            //yield return StartCoroutine(WaitForReturnToIdle(attacker.animator));
        }

        if (defender.currentHealthPoints <= 0)
        {
            State = attacker == _teamOneHero ? BattleState.TeamOneVictory : BattleState.TeamTwoVictory;

            attacker.animator.SetTrigger("isWinner");
            defender.animator.SetBool("isDead", true);

            _battleUI.SetTurnText(attacker.unitName + " killed " + defender.unitName + "!");
            _battleUI.SetStatusText("Gloty to the Winner!");

            EndBattle();
        }
        else
        {
            State = nextState;
        }

        _isTurnInProgress = false;
    }

    // Что с ней, что без нее анимации атаки все равно работают криво.
    /*IEnumerator WaitForReturnToIdle(Animator animator)
    {
        yield return null;

        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).IsName("CombatIdle")
        );
    }*/

    private void EndBattle()
    {
        StartCoroutine(_audio.PlayEndBattleMusic());
        _gameOverCanvas.SetActive(true);
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
