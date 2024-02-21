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

    private FmIdelState idelState;
    private FmMoveState moveState;
    private FmDigState digState;
    private FmAttackState attackState;
    private FmDieState dieState;

    [SerializeField] private Animator animator;

    [SerializeField] private NavMeshAgent nav;
    public NavMeshAgent Agent { get { if (nav == null) Debug.Log("NavMesh is Null"); return nav; } set { nav = value; } }


    private Vector3 movePosition;
    public Vector3 MovePosition { get { return player.position; } set { movePosition = value; } }
    public Transform player;
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

        stateManager.Init(moveState);

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
