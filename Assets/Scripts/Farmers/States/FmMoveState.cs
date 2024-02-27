using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class FmMoveState : StateFinder
{
    [SerializeField] private float nodeDistance = 2f;
    public override void StartState()
    {
        base.StartState();
        agent.isStopped = true;

        if (farmer.nodetarget == null)
            farmer.nodetarget = RandomNode();
        else
            farmer.nodetarget = RandomNodeNotCurr(farmer.nodetarget);



        LookAt(farmer.transform.rotation, RotaAngle(farmer.nodetarget), lookAtSpeed, () =>
        {
            agent.SetDestination(farmer.nodetarget);
            farmer.PlayerAnimation(stateName);
            agent.isStopped = false;
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
            swichState.SwitchState(farmer.moveToAttackState);
        }
        else
        {
            if (!farmer.Agent.isStopped && CheckDistance(farmer, farmer.nodetarget) <= nodeDistance)
            {
                if (farmer.nodetarget.Raids.Count > 0)
                    swichState.SwitchState(farmer.attackState);
                else
                {
                    int ran = Random.Range(1, 10);
                    if (ran % 2 != 0)
                        swichState.SwitchState(farmer.digState);
                    else
                        swichState.SwitchState(farmer.idelState);
                }
            }

        }
    }
}
