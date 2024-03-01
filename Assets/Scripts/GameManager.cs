using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

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
    public event Action<GameManager> ActionEven;
    public event Action<GameManager> GameOverEven;
    public event Action<GameManager> WinerEven;
    public event Action<GameManager> ResetEven;

    // public RaidController raidController;
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
            // StartState(GameState.Restart);
            // raidController.RandomSpawnOnGround();
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
                StartCoroutine(IEDelay(setUpTime));
                break;
            case GameState.Action:
                ActionEven?.Invoke(this);
                break;
            case GameState.GameOver:
                GameOverEven?.Invoke(this);
                break;
            case GameState.Winer:
                WinerEven?.Invoke(this);
                break;
            case GameState.Restart:
                ResetEven?.Invoke(this);
                StartState(GameState.SetUp);
                Debug.Log("Reset");
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

    //use by ui btn
    public void PlayAgainBTN()
    {
        // StartState(GameState.Restart);
        SceneManager.LoadScene(0);
    }
    public void TryAgainBTN()
    {
        // StartState(GameState.Restart);
        SceneManager.LoadScene(0);
    }
    public void QuaitAgain()
    {
        Application.Quit();
    }








}
