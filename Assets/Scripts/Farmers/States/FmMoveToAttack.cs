using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmMoveToAttack : StateFinder
{

    [SerializeField] private float nodeDistance = 2f;
    public AnimalTest hhh;
    public Node jjj;
    public override void StartState()
    {
        base.StartState();
        farmer.PlayerAnimation(FarmerStrate.Idel);
        agent.isStopped = true;
        ieRotate = null;
        if (CheckUnitOnGround())
        {
            AnimalTest unitTarget = UnitNearMe();
            hhh = unitTarget;
            jjj = unitTarget.NodeTarget;
            LookAt(farmer, RotaAngle(unitTarget.NodeTarget), lookAtSpeed, () =>
            {
                farmer.nodeToMove = unitTarget.NodeTarget;
                agent.SetDestination(farmer.nodeToMove);
                farmer.PlayerAnimation(stateName);
                agent.isStopped = false;
            });
        }
        else
            swichState.SwitchState(farmer.idelState);


    }
    public override void EndState()
    {
        StopAllCoroutines();
        ieRotate = null;
    }
    public override void LogiUpdate()
    {
        if (CheckUnitOnGround())
        {
            if (!farmer.Agent.isStopped && CheckDistance(farmer, farmer.nodeToMove) <= nodeDistance)
            {
                if (farmer.nodeToMove.Animas.Count > 0)
                    swichState.SwitchState(farmer.attackState);
                else
                    swichState.SwitchState(farmer.idelState);
            }
        }
        else
        {
            swichState.SwitchState(farmer.idelState);
        }
    }
}
