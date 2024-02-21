using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum GameState
{
    Null,
    SetUp,
    Action,
    GameOver,
    Winer
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Action<GameState> stateChange;
    public Action<GameState> StateChange { get { return stateChange; } set { stateChange = value; } }

    [Header("Game State")]
    [SerializeField] private GameState state = GameState.Action;
    public GameState State { get { return state; } }


    [Space]
    [Header("Faction")]
    [SerializeField] private PlayerFaction playerFaction;
    public PlayerFaction PlayerFaction { get { if (playerFaction == null) Debug.Log("Set playerFanction"); return playerFaction; } }
    [SerializeField] private EmemyFaction ememyFaction;
    public EmemyFaction EmemyFaction { get { if (playerFaction == null) Debug.Log("Set ememyFaction"); return ememyFaction; } }

    // [SerializeField] private bool isRatInArea = false;
    // public bool IsRatInArea { get { return isRatInArea; } set { isRatInArea = value; } }

    void Awake()
    {
        if (instance != null && instance != this) Destroy(this.gameObject);
        else instance = this;
    }

    void Start()
    {
        StartState(GameState.SetUp);
        StartCoroutine(IEDelay(2f));
    }
    private IEnumerator IEDelay(float time)
    {
        yield return new WaitForSeconds(time);
        StartState(GameState.Action);
    }


    void Update()
    {
        UpdateState();
    }

    #region GameState
    public void StartState(GameState gameState)
    {
        EndState();

        state = gameState;
        stateChange?.Invoke(state);
        switch (state)
        {
            case GameState.SetUp:
                break;
            case GameState.Action:

                break;
            case GameState.GameOver:

                break;
            case GameState.Winer:

                break;

        }
    }
    private void UpdateState()
    {
        switch (state)
        {
            case GameState.SetUp:
                break;
            case GameState.Action:
                if (playerFaction.Hp <= 0)
                {
                    StartState(GameState.GameOver);
                }
                if (ememyFaction.Hp <= 0)
                {
                    StartState(GameState.Winer);
                }
                break;
            case GameState.GameOver:
                break;
            case GameState.Winer:
                break;
        }
    }
    private void EndState()
    {
        switch (state)
        {
            case GameState.SetUp:
                break;
            case GameState.Action:
                break;
            case GameState.GameOver:
                break;
            case GameState.Winer:
                break;

        }
    }
    #endregion











}