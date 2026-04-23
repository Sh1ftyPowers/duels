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
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip _battleTheme;
    [SerializeField] private AudioClip _victorySound;
    [SerializeField] private AudioClip _restartMenuTheme;
    [SerializeField] private BattleUI _battleUI;


    public Transform TeamOneSpawnPoint;
    public Transform TeamTwoSpawnPoint;

    public BattleState State;

    private GameObject _teamOneHeroPrefab;
    private GameObject _teamTwoHeroPrefab;

    private Unit _teamOneHero;
    private Unit _teamTwoHero;

    private bool _isTurnInProgress = false;

    private int _teamOneHeroID;
    private int _teamTwoHeroID;

    private Queue<string> _messages = new Queue<string>();
    private bool _isShowingMessage = false;

    void Start()
    {
        _teamOneHeroID = UnityEngine.Random.Range(0, teamOnePrefabs.Length);
        _teamOneHeroPrefab = teamOnePrefabs[_teamOneHeroID];

        _teamTwoHeroID = UnityEngine.Random.Range(0, teamTwoPrefabs.Length);
        _teamTwoHeroPrefab = teamTwoPrefabs[_teamTwoHeroID];

        _musicSource.clip = _battleTheme;
        _musicSource.loop = true;
        _musicSource.Play();

        State = BattleState.Start;
        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle()
    {
        GameObject TeamOneGameObject = Instantiate(_teamOneHeroPrefab, TeamOneSpawnPoint);
        _teamOneHero = TeamOneGameObject.GetComponent<Unit>();
        _teamOneHero.Init(this);

        GameObject TeamTwoGameObject = Instantiate(_teamTwoHeroPrefab, TeamTwoSpawnPoint);
        _teamTwoHero = TeamTwoGameObject.GetComponent<Unit>();
        _teamTwoHero.Init(this);

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
        yield return StartCoroutine(WaitForMessages());
        Debug.Log("Ход: " + attacker.unitName);

        attacker.ProcessEffects();
        defender.ProcessEffects();
        yield return StartCoroutine(WaitForMessages());

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
            yield return StartCoroutine(WaitForMessages());

            attacker.PerformAttack(defender);
            yield return StartCoroutine(WaitForMessages());
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

    public IEnumerator WaitForMessages()
    {
        while (_isShowingMessage || _messages.Count > 0)
        {
            yield return null;
        }
    }

    IEnumerator ShowMessages()
    {
        _isShowingMessage = true;

        while (_messages.Count > 0)
        {
            _battleUI.SetStatusText(_messages.Dequeue());
            yield return new WaitForSeconds(2f);
        }

        _isShowingMessage = false;
    }

    public void ShowBattleInfo(string message)
    {
        _messages.Enqueue(message);

        if (!_isShowingMessage)
            StartCoroutine(ShowMessages());
    }

    private void EndBattle()
    {
        StartCoroutine(PlayEndBattleMusic());
        _gameOverCanvas.SetActive(true);
    }

    IEnumerator PlayEndBattleMusic()
    {
        _musicSource.loop = false;
        _musicSource.clip = _victorySound;
        _musicSource.Play();

        yield return new WaitForSeconds(_victorySound.length - 0.5f);

        _musicSource.clip = _restartMenuTheme;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
