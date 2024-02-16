using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum GameState
{
    Action,
    GameOver,
    Winer
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameState state = GameState.Action;
    public GameState State { get { return state; } }
    [Header("Canvas")]
    [SerializeField] private GameObject actionCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject winerCanvas;
    [Space]
    [Header("PlayerHP")]
    [SerializeField] private float maxPlayerHp;
    public float MaxPlayerHp { get { return maxPlayerHp; } }
    [SerializeField] private float currentPlayerHp;
    public float PlayerHp { get { return currentPlayerHp; } }

    [Space]
    [Header("EmenyHP")]
    [SerializeField] private float maxEmemyHp;
    public float MaxEmemyHp { get { return maxEmemyHp; } }
    [SerializeField] private float currentEmemyHp;
    public float EmenyHp { get { return currentEmemyHp; } }

    [Space]
    [Header("Point")]
    [SerializeField] private float currenPoint;
    public float CurrentPoint { get { return currenPoint; } set { currenPoint = value; } }



    [Space]
    [Header("Collect Animals")]
    [SerializeField] private List<AnimalTest> aliveAnimal = new List<AnimalTest>();
    public List<AnimalTest> AliveAnimal { get { return aliveAnimal; } set { aliveAnimal = value; } }
    [SerializeField] private List<EmenTest> aliveEmemy = new List<EmenTest>();
    public List<EmenTest> AliveEmemy { get { return aliveEmemy; } }

    [SerializeField] private List<float> deadAnimal = new List<float>(); //Collect dead animals for removing maxHealth while healing.


    public Action playerHealthUpdate;
    public Action ememyHealthUpdate;
    public Action pointUpdate;

    void Awake()
    {
        if (instance != null && instance != this) Destroy(this.gameObject);
        else instance = this;
    }

    void Start()
    {
        UpdateHealth();
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

        switch (state)
        {
            case GameState.Action:
                actionCanvas.SetActive(true);
                gameOverCanvas.SetActive(false);
                winerCanvas.SetActive(false);
                break;
            case GameState.GameOver:
                actionCanvas.SetActive(false);
                gameOverCanvas.SetActive(true);
                winerCanvas.SetActive(false);
                break;
            case GameState.Winer:
                actionCanvas.SetActive(false);
                gameOverCanvas.SetActive(false);
                winerCanvas.SetActive(true);
                break;

        }
    }
    private void UpdateState()
    {
        switch (state)
        {
            case GameState.Action:
                if (currentPlayerHp <= 0)
                    StartState(GameState.GameOver);
                if (currentEmemyHp <= 0)
                    StartState(GameState.Winer);
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
            case GameState.Action:
                break;
            case GameState.GameOver:
                break;
            case GameState.Winer:
                break;

        }
    }
    #endregion

    #region Player
    public void AddAliveAnimal(AnimalTest animal)
    {
        if (aliveAnimal.Contains(animal)) return;
        maxPlayerHp += animal.maxHp;
        aliveAnimal.Add(animal);
        UpdateHealth();
    }
    public void RemoveAnimal(AnimalTest animal)
    {
        if (aliveAnimal.Count == 0) return;
        if (!aliveAnimal.Contains(animal)) return;
        maxPlayerHp -= animal.maxHp;
        UpdateHealth();
        aliveAnimal.Remove(animal);
    }
    public void UpdateHealth()
    {
        float chp = 0;
        foreach (var T in aliveAnimal)
        {
            chp += T.currentHp;
        }
        currentPlayerHp = chp;
        playerHealthUpdate?.Invoke();
    }
    public void Health()
    {
        if (deadAnimal.Count <= 0) return;
        maxPlayerHp -= deadAnimal[0];
        deadAnimal.RemoveAt(0);
        UpdateHealth();
    }
    #endregion

    #region Ememy
    public void AddAliveEmemy(EmenTest ememy)
    {
        if (aliveEmemy.Contains(ememy)) return;
        maxEmemyHp += ememy.maxHp;
        aliveEmemy.Add(ememy);
        UpdateEmenyHealth();
    }
    public void RemoveEmemy(EmenTest ememy)
    {
        if (aliveEmemy.Count == 0) return;
        if (!aliveEmemy.Contains(ememy)) return;
        maxEmemyHp -= ememy.maxHp;
        UpdateEmenyHealth();
        aliveEmemy.Remove(ememy);
    }

    public void UpdateEmenyHealth()
    {
        float chp = 0;
        foreach (var T in aliveEmemy)
        {
            chp += T.currentHp;
        }
        currentEmemyHp = chp;
        ememyHealthUpdate?.Invoke();
    }
    #endregion
    public void UpdatePoint(float point)
    {
        currenPoint += point;
        pointUpdate?.Invoke();
    }
}
