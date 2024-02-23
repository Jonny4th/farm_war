using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Threading;
using System.Threading.Tasks;


public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject setUpPanel;
    [SerializeField] private GameObject actionPanel;
    [SerializeField] private GameObject winerPanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private UIPanel playerUI;
    [SerializeField] private UIPanel ememyUI;
    [SerializeField] private TextMeshProUGUI point;


    [Header("Speed of hp bar")]
    [SerializeField] private float speed = 300f;



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

        GameManager.instance.StateChange += StartState;
    }

    private void Update()
    {
        UpdateTime();
        UpdateNode();
        UpdateCurrentState();
    }

    void Start()
    {
        ememyUI.curr = GameManager.instance.EmemyFaction.Hp;
        ememyUI.maxHp = GameManager.instance.EmemyFaction.MaxHp;

        playerUI.curr = GameManager.instance.PlayerFaction.Hp;
        playerUI.maxHp = GameManager.instance.PlayerFaction.MaxHp;

        state = GameManager.instance.State;

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
    public void UpdateUi(PlayerFaction player)
    {
        if (playerUI.em != null)
            StopCoroutine(playerUI.em);
        playerUI.em = IEHPBarAnima(playerUI, playerUI.curr, GameManager.instance.PlayerFaction.Hp);

        StartCoroutine(playerUI.em);

    }
    public void UpdateUi(EmemyFaction ememy)
    {
        if (ememyUI.em != null)
            StopCoroutine(ememyUI.em);
        ememyUI.em = IEHPBarAnima(ememyUI, ememyUI.curr, GameManager.instance.EmemyFaction.Hp);

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

            panel.hpText.text = panel.HpString;
            panel.hpBar.fillAmount = panel.persentHp;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    private IEnumerator IEPanelAlp(Image image, Action callback)
    {
        float persent = 0;
        float time = 255 / 300f;
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
        nodeTarget.text = $"Index : {farmer.nodetarget.Index}";
    }
    public void UpdateCurrentState()
    {
        if (!farmer) return;
        StateBase f = farmer.StateManager.CurrentState;

        currState.text = $"CurreSteta : {f.StateNameStr}";

    }
    #endregion
}
