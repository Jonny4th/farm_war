using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public enum FarmerStrate
{
    Null,
    Idel,
    Move,
    MoveToAttack,
    Dig,
    Attack,
    Die
}
public class Farmer : MonoBehaviour
{

    [SerializeField] private float maxHp = 100;
    public float MaxHp { get { return maxHp; } }
    private float currentHp;
    public float Current { get { return currentHp; } }

    private StateManager stateManager;
    public StateManager StateManager { get { return stateManager; } set { stateManager = value; } }

    [Header("State")]
    public FmIdelState idelState;
    public FmMoveState moveState;
    public FmDigState digState;
    public FmAttackState attackState;
    public FmDieState dieState;
    public FmMoveToAttack moveToAttackState;

    private Vector3 startPosition;
    private Quaternion startRotation;
    [SerializeField] private string currentAnimation = "";

    [Space]
    [SerializeField] private Animator animator;

    [SerializeField] private NavMeshAgent nav;
    public NavMeshAgent Agent { get { if (nav == null) Debug.Log("NavMesh is Null"); return nav; } set { nav = value; } }


    public Node nodetarget;
    public CropController cropController;

    // public Raidable raidable;

    public FarmerStrate currentState;
    [Header("Tool")]
    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject hoe;
    private int currToolIndex = 0;

    void Awake()
    {
        SwicthTool(1);
    }
    void Start()
    {
        if (nav == null) nav = GetComponent<NavMeshAgent>();

        stateManager = new StateManager();

        idelState.Init(this, animator, GameManager.instance);
        moveState.Init(this, animator, GameManager.instance);
        digState.Init(this, animator, GameManager.instance);
        attackState.Init(this, animator, GameManager.instance);
        dieState.Init(this, animator, GameManager.instance);
        moveToAttackState.Init(this, animator, GameManager.instance);
        // gameManager = GameManager.instance;
        GameManager.instance.ResetEven += ResetGame;
        GameManager.instance.GameOverEven += GameOver;

        SetSpawnPosition(transform);

        stateManager.Init(idelState);
    }



    public void SetSpawnPosition(Transform tranform)
    {
        startPosition = tranform.position;
        startRotation = tranform.rotation;
    }

    void Update()
    {
        if (GameManager.instance.State != GameState.Action) return;
        stateManager.CurrentState.LogiUpdate();
    }

    void FixedUpdate()
    {
        if (GameManager.instance.State != GameState.Action) return;
        stateManager.CurrentState.PhysiUpdate();
    }


    public void FormOtherColl()
    {
        stateManager.CurrentState.FormOtherColl();
    }

    private void StopAllAnimation()
    {
        animator.SetBool("IsIdel", false);
        animator.SetBool("IsRun", false);
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsDig", false);
    }
    public void PlayerAnimation(FarmerStrate state)
    {
        StopAllAnimation();
        switch (state)
        {
            case FarmerStrate.Idel:
                animator.SetBool("IsIdel", true);
                break;
            case FarmerStrate.Move:
                animator.SetBool("IsWalk", true);
                break;
            case FarmerStrate.MoveToAttack:
                animator.SetBool("IsRun", true);
                break;
            case FarmerStrate.Attack:
            case FarmerStrate.Dig:
                animator.SetBool("IsDig", true);
                break;

        }
    }
    private void SetUpGame()
    {
        stateManager.Init(idelState);
    }
    private void ResetGame(GameManager gameManager)
    {
        StopAllAnimation();
        currentHp = maxHp;
        stateManager.Init(idelState);
        nodetarget = null;
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
    private void GameOver(GameManager gameManager)
    {
        StopAllAnimation();
    }

    private void OnDestroy()
    {
        GameManager.instance.ResetEven -= ResetGame;
        GameManager.instance.GameOverEven -= GameOver;
    }
    public static implicit operator Vector3(Farmer farmer)
    {
        return farmer.transform.position;
    }
    public static implicit operator Quaternion(Farmer farmer)
    {
        return farmer.transform.rotation;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="toolIndex">1 => Hoe,2 => axe</param>
    public void SwicthTool(int toolIndex)
    {
        if (currToolIndex == toolIndex) return;
        switch (toolIndex)
        {
            case 1:
                axe.SetActive(false);
                hoe.SetActive(true);
                currToolIndex = toolIndex;
                break;
            case 2:
                hoe.SetActive(false);
                axe.SetActive(true);
                currToolIndex = toolIndex;
                break;
        }
    }
}
