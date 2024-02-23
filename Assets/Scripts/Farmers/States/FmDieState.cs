using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FmDieState : StateBase
{
    public FmDieState(Farmer farmer, Animator animator, GameManager gameManager) : base(farmer, animator, gameManager)
    {
        stateName = FarmerStrate.Die;
    }

    public override void StartState()
    {
        base.StartState();
    }
    public override void EndState()
    {

    }
    public override void LogiUpdate()
    {

    }
    public override void PhysiUpdate()
    {

    }
}
