using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class FmIdelState : StateFinder
{
    // public FmIdelState(Farmer farmer, Animator animator, GameManager gameManager) : base(farmer, animator, gameManager)
    // {
    //     stateName = FarmerStrate.Idel;
    // }

    public override void StartState()
    {
        base.StartState();
        farmer.currentState = stateName;
        timer = 0;
        nodeIndex = -1;
    }
    public override void EndState()
    {

    }
    public override void LogiUpdate()
    {
        if (CheckUnitOnGround())
        {
            UnitNearMe();
            farmer.StateManager.SwitchState(farmer.moveState);
        }
        else
        {

            timer += Time.deltaTime;
            if (timer >= time)
            {
                timer = 0;
                farmer.targetMoving = RandomNode(out nodeIndex);
                manager.NodeMana.SetIndexToMove(farmer, nodeIndex);
                farmer.StateManager.SwitchState(farmer.moveState);
            }
        }
    }
    public override void PhysiUpdate()
    {
        base.PhysiUpdate();
    }
}
