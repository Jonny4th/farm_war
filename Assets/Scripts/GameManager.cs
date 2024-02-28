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
    Winer,
    Restart
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
    [SerializeField] private NodeManager nodeManager;
    public NodeManager NodeMana { get { if (nodeManager == null) Debug.Log("Set NodeManager"); return nodeManager; } set { nodeManager = value; } }

    // [SerializeField] private bool isRatInArea = false;
    // public bool IsRatInArea { get { return isRatInArea; } set { isRatInArea = value; } }
    public event Action<GameManager> SetUpEven;
    public event Action ActionEven;
    public event Action GameOverEven;
    public event Action WinerEven;
    public event Action ResetEven;

    [SerializeField] private bool immortal;
    public bool Immortal { get { return immortal; } }

    [SerializeField] private float setUpTime = 2f;
    void Awake()
    {
        if (instance != null && instance != this) Destroy(this.gameObject);
        else instance = this;
    }

    void Start()
    {
        StartState(GameState.SetUp);
        StartCoroutine(IEDelay(setUpTime));
    }
    private IEnumerator IEDelay(float time)
    {
        yield return new WaitForSeconds(time);
        StartState(GameState.Action);
    }

    private void ResetValu()
    {
        StartState(GameState.Action);
    }
    void Update()
    {
        UpdateState();
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartState(GameState.Restart);
        }
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
                SetUpEven?.Invoke(this);
                break;
            case GameState.Action:
                ActionEven?.Invoke();
                break;
            case GameState.GameOver:
                GameOverEven?.Invoke();
                break;
            case GameState.Winer:
                WinerEven?.Invoke();
                break;
            case GameState.Restart:
                ResetEven?.Invoke();
                StartState(GameState.Action);
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
                if (!immortal && playerFaction.Hp <= 0)
                {
                    StartState(GameState.GameOver);
                }
                if (!immortal && ememyFaction.Hp <= 0)
                {
                    StartState(GameState.Winer);
                }
                break;
            case GameState.GameOver:
                break;
            case GameState.Winer:
                break;
            case GameState.Restart:

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
            case GameState.Restart:

                break;
        }
    }
    #endregion











}
