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

    [SerializeField] private StateManager stateManager;
    public StateManager StateManager { get { return stateManager; } set { stateManager = value; } }

    [HideInInspector] public FmIdelState idelState;
    [HideInInspector] public FmMoveState moveState;

    [HideInInspector] public FmDigState digState;
    [HideInInspector] public FmAttackState attackState;
    [HideInInspector] public FmDieState dieState;


    [SerializeField] private Animator animator;

    [SerializeField] private NavMeshAgent nav;
    public NavMeshAgent Agent { get { if (nav == null) Debug.Log("NavMesh is Null"); return nav; } set { nav = value; } }


    public Vector3 targetMoving;
    // public Vector3 TargetMove { get { return targetMoving; } set { targetMoving = value; } }


    [HideInInspector] public AnimalTest unitTarget;

     public Node nodetarget;

    public FarmerStrate currentState;
    void Awake()
    {

    }
    void Start()
    {
        if (nav == null) nav = GetComponent<NavMeshAgent>();

        stateManager = new StateManager();

        idelState = new FmIdelState(this, animator, GameManager.instance);
        moveState = new FmMoveState(this, animator, GameManager.instance);
        digState = new FmDigState(this, animator, GameManager.instance);
        attackState = new FmAttackState(this, animator, GameManager.instance);
        dieState = new FmDieState(this, animator, GameManager.instance);

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



}
