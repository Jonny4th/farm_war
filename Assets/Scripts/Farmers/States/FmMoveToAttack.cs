using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmMoveToAttack : StateFinder
{

    [SerializeField] private float nodeDistance = 2f;
    public override void StartState()
    {
        base.StartState();
        agent.isStopped = true;
        if (CheckUnitOnGround())
        {
            AnimalTest unitTarget = UnitNearMe();

            LookAt(farmer, unitTarget.NodeTarget, lookAtSpeed, () =>
            {
                farmer.nodeToMove = unitTarget.NodeTarget;
                agent.SetDestination(farmer.nodeToMove);
                agent.isStopped = false;
            });
        }
        else
            swichState.SwitchState(farmer.idelState);
    }
    public override void EndState()
    {
        base.EndState();
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
