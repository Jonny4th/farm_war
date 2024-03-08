using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject setUpPanel;
    [SerializeField] private GameObject actionPanel;
    [SerializeField] private GameObject winerPanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private UIPanel playerUI;
    [SerializeField] private UIPanel ememyUI;
    [SerializeField] private UIPanel coinUI;

    [Header("Speed of hp bar")]
    // [SerializeField] private float speed = 300f;
    [SerializeField] private float speedPanelFace = 300f;


    [Header("Status UI")]
    [SerializeField] private UIStatus attackSt;
    [SerializeField] private UIStatus poisonSt;
    [SerializeField] private UIStatus healingSt;


    private GameState state;

    public static UIManager instance;
    private Color panelColor;
    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    void OnEnable()
    {
        gameManager.StateChange += StartState;
    }

    void OnDisable()
    {
        gameManager.StateChange -= StartState;
    }

    void Start()
    {
        ememyUI.curr = gameManager.EmemyFaction.Hp;
        ememyUI.maxHp = gameManager.EmemyFaction.MaxHp;

        playerUI.curr = gameManager.PlayerFaction.Hp;
        playerUI.maxHp = gameManager.PlayerFaction.MaxHp;


        state = gameManager.State;

        gameManager.PlayerFaction.UpdateHp += UpdateUiPlayer;
        gameManager.PlayerFaction.UpdateCoin += UpdateCoin;
        gameManager.PlayerFaction.AttackEvent += AttackStatus;
        gameManager.PlayerFaction.HealingEvent += HealingStatus;

        gameManager.EmemyFaction.UpdateHp += UpdateUiEmemy;

        gameManager.ResetEven += ResetGame;

        panelColor = setUpPanel.GetComponent<Image>().color;
    }

    private void HideAllPanel()
    {
        setUpPanel.SetActive(false);
        actionPanel.SetActive(false);
        winerPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }
    private void SetUpGame()
    {

    }
    private void ResetGame(GameManager gameManager)
    {

    }

    #region  State
    public void StartState(GameState gameState)
    {
        EndState(state);
        state = gameState;
        switch (gameState)
        {
            case GameState.SetUp:
                setUpPanel.SetActive(true);
                attackSt.uiObj.gameObject.SetActive(false);
                poisonSt.uiObj.gameObject.SetActive(false);
                healingSt.uiObj.gameObject.SetActive(false);
                break;
            case GameState.Action:
                actionPanel.SetActive(true);
                break;
            case GameState.GameOver:
                gameOverPanel.SetActive(true);
                break;
            case GameState.Winer:
                winerPanel.SetActive(true);
                break;
            case GameState.Restart:
                setUpPanel.GetComponent<Image>().color = panelColor;

                setUpPanel.SetActive(true);
                break;
        }
    }

    private void EndState(GameState state)
    {
        switch (state)
        {
            case GameState.SetUp:

                StartCoroutine(IEPanelAlp(setUpPanel.GetComponent<Image>(), () => setUpPanel.SetActive(false)));
                break;
            case GameState.Action:
                HideAllPanel();
                break;
            case GameState.GameOver:
                HideAllPanel();
                break;
            case GameState.Winer:
                HideAllPanel();
                break;
        }
    }
    #endregion

    #region  UI Update
    // public void UpdateUi(PlayerFaction player)
    // {
    //     if (playerUI.em != null)
    //         StopCoroutine(playerUI.em);
    //     playerUI.em = IEHPBarAnima(playerUI, playerUI.curr, GameManager.instance.PlayerFaction.Hp);

    //     StartCoroutine(playerUI.em);
    // }
    public void UpdateUiPlayer(PlayerFaction player)
    {
        if (playerUI.em != null)
            StopCoroutine(playerUI.em);
        if (Mathf.Abs(playerUI.curr - player.Hp) >= 50)
            playerUI.em = IEHPBarAnima(playerUI, playerUI.curr, player.Hp, playerUI.speed);
        else
            playerUI.em = IEHPBarAnima(playerUI, playerUI.curr, player.Hp, playerUI.minSpeed);
        StartCoroutine(playerUI.em);
    }
    public void UpdateCoin(PlayerFaction player)
    {
        if (coinUI.em != null)
            StopCoroutine(coinUI.em);
        if (Mathf.Abs(coinUI.curr - player.Coin) >= 50)
            coinUI.em = IEHPBarAnima(coinUI, coinUI.curr, player.Coin, coinUI.speed);
        else
            coinUI.em = IEHPBarAnima(coinUI, coinUI.curr, player.Coin, coinUI.minSpeed);

        StartCoroutine(coinUI.em);
    }

    public void UpdateUiEmemy(EmemyFaction ememy)
    {
        if (ememyUI.em != null)
            StopCoroutine(ememyUI.em);
        if (Mathf.Abs(coinUI.curr - ememy.Hp) >= 50)
            ememyUI.em = IEHPBarAnima(ememyUI, ememyUI.curr, ememy.Hp, ememyUI.speed);
        else
            ememyUI.em = IEHPBarAnima(ememyUI, ememyUI.curr, ememy.Hp, ememyUI.minSpeed);
        StartCoroutine(ememyUI.em);
    }
    #endregion

    #region  IEnumerator
    private IEnumerator IEHPBarAnima(UIPanel panel, float curr, float target, float speed)
    {
        float persent = 0;
        float time = Mathf.Abs(target - curr) / speed;
        float ontime = 0;

        while (persent < 1)
        {
            ontime += Time.deltaTime;
            persent = ontime / time;
            panel.curr = Mathf.Lerp(curr, target, persent);

            if (panel.hpText != null) panel.hpText.text = panel.HpString;
            if (panel.hpBar != null) panel.hpBar.fillAmount = panel.persentHp;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private IEnumerator IEPanelAlp(Image image, Action callback)
    {
        float persent = 0;
        float time = 255 / speedPanelFace;
        float ontime = 0;

        while (persent < 1)
        {
            ontime += Time.deltaTime;
            persent = ontime / time;
            var a = Mathf.Lerp(255f, 0f, persent);
            image.color = new Color(image.color.r, image.color.g, image.color.b, a / 255f);

            yield return new WaitForSeconds(Time.deltaTime);
        }
        callback?.Invoke();
    }
    #endregion

    #region  Status UI


    private void AttackStatus() => SetCoroutine(attackSt);
    private void PoisonStatus() => SetCoroutine(poisonSt);
    private void HealingStatus() => SetCoroutine(healingSt);
    private void SetCoroutine(UIStatus uIStatus)
    {
        if (uIStatus.em != null)
            StopCoroutine(uIStatus.em);
        uIStatus.em = HideStatus(uIStatus);
        StartCoroutine(uIStatus.em);
    }

    private IEnumerator HideStatus(UIStatus uistatus)
    {
        uistatus.uiObj.SetActive(true);

        if (uistatus.startScaleX == -1)
            uistatus.startScaleX = uistatus.uiObj.transform.localScale.x;

        uistatus.uiObj.transform.localScale = new Vector3(uistatus.curr, uistatus.curr, 1f);

        float persent = 0;
        float speed = 1f / uistatus.timeUP / 60f;
        float st = uistatus.curr;
        //=======UP=========

        while (persent < 1)
        {
            persent += speed;
            uistatus.curr = Mathf.Lerp(st, uistatus.startScaleX, persent);
            uistatus.uiObj.transform.localScale = new Vector3(uistatus.curr, uistatus.curr, 1f);
            yield return null;
        }
        //=======Hold========
        yield return new WaitForSeconds(uistatus.hold);
        //=======DOWN=========
        persent = 0;
        speed = 1f / uistatus.timeDown / 60f;
        st = uistatus.curr;
        while (persent < 1)
        {
            persent += speed;
            uistatus.curr = Mathf.Lerp(st, 0, persent);
            uistatus.uiObj.transform.localScale = new Vector3(uistatus.curr, uistatus.curr, 1f);
            yield return null;
        }
        uistatus.uiObj.SetActive(false);
        uistatus.em = null;
    }
    #endregion

    #region Test
    // public void UpdateTime()
    // {
    //     if (!farmer) return;
    //     var f = farmer.StateManager.CurrentState as StateFinder;
    //     if (f is StateFinder)
    //         timeState.text = $"StateTime : {(int)f.Timer}";
    //     else
    //         timeState.text = $"StateTime : xx";
    // }
    // public void UpdateNode()
    // {
    //     if (!farmer) return;
    //     if (!farmer.nodetarget) return;
    //     // nodeTarget.text = $"Index : {farmer.nodetarget.Index}";
    // }
    // public void UpdateCurrentState()
    // {
    //     if (!farmer) return;
    //     StateBase f = farmer.StateManager.CurrentState;

    //     // currState.text = $"CurreSteta : {f.StateNameStr}";

    // }
    #endregion
}
