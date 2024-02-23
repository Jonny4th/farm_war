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

    private FmIdelState idelState;
    public FmIdelState Idel { get { return idelState; } private set { } }
    private FmMoveState moveState;
    public FmMoveState Move { get { return moveState; } private set { } }
    private FmDigState digState;
    public FmDigState Dig { get { return digState; } private set { } }
    private FmAttackState attackState;
    public FmAttackState Attack { get { return attackState; } private set { } }
    private FmDieState dieState;
    public FmDieState Did { get { return dieState; } private set { } }

    [SerializeField] private Animator animator;

    [SerializeField] private NavMeshAgent nav;
    public NavMeshAgent Agent { get { if (nav == null) Debug.Log("NavMesh is Null"); return nav; } set { nav = value; } }


    private Vector3 movePosition;
    public Vector3 MovePosition { get { return player.position; } set { movePosition = value; } }
    public Transform player;

    private Vector3 targetMoving;
    public Vector3 TargetMove { get { return targetMoving; } set { targetMoving = value; } }



    private AnimalTest unitTarget;
    public AnimalTest UnitTarget { get { return unitTarget; } set { unitTarget = value; } }

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

        stateManager.Init(Idel);

    }


    void Update()
    {

        stateManager.CurrentState.LogiUpdate();
    }

    void FixedUpdate()
    {
        stateManager.CurrentState.PhysiUpdate();
    }



}
