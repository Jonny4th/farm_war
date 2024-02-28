using System;
using System.Collections;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI point;

    [Header("Speed of hp bar")]
    [SerializeField] private float speed = 300f;
    [SerializeField] private float speedPanel = 300f;

    [Header("Test")]
    [SerializeField] private TextMeshProUGUI timeState;
    [SerializeField] private TextMeshProUGUI nodeTarget;
    [SerializeField] private TextMeshProUGUI currState;
    [SerializeField] private Farmer farmer;

    private GameState state;

    public static UIManager instance;

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
        gameManager.EmemyFaction.UpdateHp += UpdateUiEmemy;
    }

    private void HideAllPanel()
    {
        setUpPanel.SetActive(false);
        actionPanel.SetActive(false);
        winerPanel.SetActive(false);
        gameOverPanel.SetActive(false);
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

        playerUI.em = IEHPBarAnima(playerUI, playerUI.curr, player.Hp);
        StartCoroutine(playerUI.em);
    }

    public void UpdateUiEmemy(EmemyFaction ememy)
    {
        if (ememyUI.em != null)
            StopCoroutine(ememyUI.em);

        ememyUI.em = IEHPBarAnima(ememyUI, ememyUI.curr, ememy.Hp);
        StartCoroutine(ememyUI.em);
    }
    #endregion

    #region  IEnumerator
    private IEnumerator IEHPBarAnima(UIPanel panel, float curr, float target)
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
        float time = 255 / speedPanel;
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

    #region Test
    public void UpdateTime()
    {
        if (!farmer) return;
        var f = farmer.StateManager.CurrentState as StateFinder;
        if (f is StateFinder)
            timeState.text = $"StateTime : {(int)f.Timer}";
        else
            timeState.text = $"StateTime : xx";
    }
    public void UpdateNode()
    {
        if (!farmer) return;
        if (!farmer.nodetarget) return;
        // nodeTarget.text = $"Index : {farmer.nodetarget.Index}";
    }
    public void UpdateCurrentState()
    {
        if (!farmer) return;
        StateBase f = farmer.StateManager.CurrentState;

        // currState.text = $"CurreSteta : {f.StateNameStr}";

    }
    #endregion
}
