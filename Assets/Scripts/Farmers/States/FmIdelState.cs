using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmIdelState : StateBase
{
    private float time = 5f;
    private float timer = 0;



    public FmIdelState(Farmer farmer, Animator animator, GameManager gameManager) : base(farmer, animator, gameManager)
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
        if (CheckUnitOnGround())
        {
            UnitNearMe();
            farmer.StateManager.SwitchState(farmer.Move);
        }
        else
        {

            time += Time.deltaTime;
            







        }
    }
    public override void PhysiUpdate()
    {

    }
}
