using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class FmMoveState : StateBase
{

    public FmMoveState(Farmer farmer, Animator animator, GameManager gameManager) : base(farmer, animator, gameManager)
    {
        stateName = FarmerStrate.Move;
    }

    public override void StartState()
    {
        base.StartState();
        farmer.Agent.isStopped = true;
    }
    public override void EndState()
    {
        farmer.Agent.isStopped = true;
        manager.NodeMana.RemoveFarmer(farmer, farmer.nodetarget.Index);
    }
    public override void LogiUpdate()
    {
        if (CheckUnitOnGround())
        {

        }
        else
        {

            RotateToTarget(farmer.nodetarget.transform.position);

            farmer.Agent.isStopped = false;


        }
    }
    public override void PhysiUpdate()
    {
        farmer.Agent.SetDestination(farmer.targetMoving);
    }
    private void RotateToTarget(Vector3 pos)
    {
        Vector3 dir = (pos - farmer.transform.position).normalized;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        farmer.transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}
