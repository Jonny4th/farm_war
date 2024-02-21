using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase : MonoBehaviour
{
    protected Animator animator;
    protected GameManager manager;


    public StateBase(Animator animator, GameManager gameManager)
    {
        this.animator = animator;
        this.manager = gameManager;
    }

    public void StartState()
    {

    }
    public void EndState()
    {

    }
    public void LogiUpdate()
    {

    }
    public void PhysiUpdate()
    {

    }
}
