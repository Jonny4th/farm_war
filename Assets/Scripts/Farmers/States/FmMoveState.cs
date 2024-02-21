using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FmMoveState : StateBase
{

    public FmMoveState(Farmer farmer, Animator animator, GameManager gameManager) : base(farmer, animator, gameManager)
    {

    }

    public override void StartState()
    {

    }
    public override void EndState()
    {

    }
    public override void LogiUpdate()
    {

    }
    public override void PhysiUpdate()
    {
        farmer.Agent.SetDestination(farmer.MovePosition);
    }
}
