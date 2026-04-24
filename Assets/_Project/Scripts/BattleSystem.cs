using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleState { Start, TeamOneTurn, TeamTwoTurn, TeamOneVictory, TeamTwoVictory }

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject[] teamOnePrefabs;
    [SerializeField] private GameObject[] teamTwoPrefabs;

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

    void Start()
    {
        _teamOneHeroPrefab = teamOnePrefabs[Random.Range(0, teamOnePrefabs.Length)];
        _teamTwoHeroPrefab = teamTwoPrefabs[Random.Range(0, teamTwoPrefabs.Length)];

        _audio.PlayBattleMusic();

        State = BattleState.Start;
        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle()
    {
        _teamOneHero = _spawner.SpawnTeamOne(_teamOneHeroPrefab, this, _message);
        _teamTwoHero = _spawner.SpawnTeamTwo(_teamTwoHeroPrefab, this, _message);

        _battleUI.SetTurnText("The Battle Begins!");

        yield return new WaitForSeconds(0.5f);

        State = BattleState.TeamOneTurn;
        StartCoroutine(BattleLoop());
    }

    IEnumerator BattleLoop()
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

    IEnumerator PerformTurn(Unit attacker, Unit defender, BattleState nextState)
    {
        _battleUI.SetTurnText(attacker.unitName + " attacks!");
        yield return StartCoroutine(_message.WaitForMessages());

        Debug.Log("Ход: " + attacker.unitName);

        _effects.ProcessEffects(attacker);
        _effects.ProcessEffects(defender);
        yield return StartCoroutine(_message.WaitForMessages());

        if (CheckVictory(attacker, defender))
            yield break;

        if (attacker.isStunned)
        {
            attacker.isStunned = false;
            State = nextState;
            yield break;
        }

        yield return new WaitForSeconds(3f);

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
        if (defender.currentHealthPoints > 0)
            return false;

        State = attacker == _teamOneHero
            ? BattleState.TeamOneVictory
            : BattleState.TeamTwoVictory;

        attacker.PlayVictoryAnimation();
        defender.PlayDeathAnimation();

        _battleUI.SetTurnText(attacker.unitName + " killed " + defender.unitName + "!");
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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
