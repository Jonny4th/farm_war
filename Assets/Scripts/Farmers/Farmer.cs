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

    private StateManager stateManager;

    private FmIdelState idelState;
    private FmMoveState moveState;
    private FmDigState digState;
    private FmAttackState attackState;
    private FmDieState dieState;

    [SerializeField] private NavMeshAgent nav;
    public NavMeshAgent Agent { get { if (nav == null) Debug.Log("NavMesh is Null"); return nav; } set { nav = value; } }




    void Awake()
    {

    }
    void Start()
    {
        if (nav == null) nav = GetComponent<NavMeshAgent>();
    }


    void Update()
    {


    }

    void FixedUpdate()
    {

    }



}
