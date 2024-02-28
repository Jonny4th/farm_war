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


    [SerializeField] private string currentAnimation = "";

    [Space]
    [SerializeField] private Animator animator;

    [SerializeField] private NavMeshAgent nav;
    public NavMeshAgent Agent { get { if (nav == null) Debug.Log("NavMesh is Null"); return nav; } set { nav = value; } }


    public Node nodetarget;

    // public Raidable raidable;

    public FarmerStrate currentState;

    void Awake()
    {

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
        GameManager.instance.ResetEven += Reset;
        GameManager.instance.GameOverEven += GameOver;

        stateManager.Init(idelState);

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
    private void Reset()
    {
        StopAllAnimation();
        stateManager.Init(idelState);
    }
    private void GameOver()
    {
        StopAllAnimation();
    }

    private void OnDestroy()
    {
        GameManager.instance.ResetEven -= Reset;
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
}
