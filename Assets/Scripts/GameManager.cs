using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum GameState
{
    Action,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameState state = GameState.Action;
    [SerializeField] private Canvas actionCanvas;
    [SerializeField] private Canvas gameOverCanvas;
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
    public List<AnimalTest> AliveAnimal { get { return aliveAnimal; } }
    [SerializeField] private List<EmenTest> aliveEmemy = new List<EmenTest>();
    public List<EmenTest> AliveEmemy { get { return aliveEmemy; } }

    [SerializeField] private List<float> deadAnimal = new List<float>(); //Collect dead animals for removing maxHealth while healing.




    void Awake()
    {
        if (instance != null && instance != this) Destroy(this.gameObject);
        else instance = this;
    }

    void Start()
    {
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
                actionCanvas.enabled = true;
                gameOverCanvas.enabled = false;
                break;
            case GameState.GameOver:
                actionCanvas.enabled = true;
                gameOverCanvas.enabled = true;
                break;

        }
    }
    private void UpdateState()
    {
        switch (state)
        {
            case GameState.Action:
                break;
            case GameState.GameOver:
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

        }
    }
    #endregion

    #region Player
    public void AddAliveAnimal(AnimalTest animal)
    {
        aliveAnimal.Add(animal);
        maxPlayerHp += animal.maxHp;
        UpdateHealth();
    }
    public void RemoveAnimal(AnimalTest animal)
    {
        if (aliveAnimal.Count == 0) return;
        maxPlayerHp -= animal.maxHp;
        aliveAnimal.Remove(animal);
        UpdateHealth();
    }
    public void UpdateHealth()
    {
        float chp = 0;
        foreach (var T in aliveAnimal)
        {
            chp += T.currentHp;
        }
        currentPlayerHp = chp;
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
        aliveEmemy.Add(ememy);
        maxEmemyHp += ememy.maxHp;
        UpdateEmenyHealth();
    }
    public void RemoveEmemy(EmenTest ememy)
    {
        if (aliveEmemy.Count == 0) return;
        maxEmemyHp -= ememy.maxHp;
        aliveEmemy.Remove(ememy);
        UpdateEmenyHealth();
    }

    public void UpdateEmenyHealth()
    {
        float chp = 0;
        foreach (var T in aliveEmemy)
        {
            chp += T.currentHp;
        }
        currentEmemyHp = chp;
    }
    #endregion

}
