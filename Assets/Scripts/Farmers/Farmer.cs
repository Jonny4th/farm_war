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

    [SerializeField] private StateManager stateManager;
    public StateManager StateManager { get { return stateManager; } set { stateManager = value; } }

    [Header("State")]
    public FmIdelState idelState;
    public FmMoveState moveState;
    public FmDigState digState;
    public FmAttackState attackState;
    public FmDieState dieState;
    public FmMoveToAttack moveToAttackState;

    [Space]
    [SerializeField] private Animator animator;

    [SerializeField] private NavMeshAgent nav;
    public NavMeshAgent Agent { get { if (nav == null) Debug.Log("NavMesh is Null"); return nav; } set { nav = value; } }


    public Vector3 targetMoving;
    // public Vector3 TargetMove { get { return targetMoving; } set { targetMoving = value; } }


    [HideInInspector] public AnimalTest unitTarget;

    public Node nodeToMove;

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

    public static implicit operator Vector3(Farmer farmer)
    {
        return farmer.transform.position;
    }
    public static implicit operator Quaternion(Farmer farmer)
    {
        return farmer.transform.rotation;
    }

}
