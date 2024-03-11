using FarmWar.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmMoveToAttack : StateFinder
{

    [SerializeField] private float nodeDistance = 2f;

    //  public override void StartState()
    //     {
    //         base.StartState();
    //         farmer.PlayerAnimation(FarmerStrate.Idel);
    //         agent.isStopped = true;
    //         ieRotate = null;
    //         if (CheckUnitOnGround())
    //         {
    //             Raid unitTarget = UnitNearMe1();

    //             LookAt(farmer, RotaAngle(unitTarget.transform.position), lookAtSpeed, () =>
    //             {
    //                 farmer.nodeToMove = unitTarget.transform;
    //                 agent.SetDestination(farmer.nodeToMove);
    //                 farmer.PlayerAnimation(stateName);
    //                 agent.isStopped = false;
    //             });
    //         }
    //         else
    //             swichState.SwitchState(farmer.idelState);


    //     }
    public override void StartState()
    {
        base.StartState();
        farmer.SwicthTool(2);
        farmer.PlayerAnimation(FarmerStrate.Idel);
        agent.isStopped = true;
        ieRotate = null;
        CountDown(0.2f, () =>
        {
            if (CheckUnitOnGround())
            {
                var unitTargets = FindNodeObj(manager.NodeMana.nodeCollcetion.FindAll(x => x.Raids.Count > 0));
                Node unitTarget = unitTargets[Random.Range(0, unitTargets.Count)];
                if (unitTarget == null) unitTarget = unitTargets[0];
                LookAt(farmer, RotaAngle(unitTarget), lookAtSpeed, () =>
                {
                    farmer.nodetarget = unitTarget;
                    agent.SetDestination(unitTarget);
                    farmer.PlayerAnimation(FarmerStrate.MoveToAttack);
                    agent.isStopped = false;
                });
            }
            else
                swichState.SwitchState(farmer.idelState);
        });



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
            if (!farmer.Agent.isStopped && CheckDistance(farmer, farmer.nodetarget) <= nodeDistance)
            {
                agent.isStopped = true;
                if (farmer.nodetarget.Raids.Count > 0)
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
