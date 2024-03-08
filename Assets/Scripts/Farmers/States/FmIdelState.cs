using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[System.Serializable]
public class FmIdelState : StateBase
{

    public override void StartState()
    {
        base.StartState();
        farmer.SwicthTool(1);
        agent.isStopped = true;
        if (farmer.nodetarget != null)
            LookAt(farmer, RotaAngle(farmer.nodetarget), lookAtSpeed);
        farmer.PlayerAnimation(FarmerStrate.Idel);
        CountDown(time, () => swichState.SwitchState(farmer.moveState));
    }
    public override void EndState()
    {
        StopAllCoroutines();
        ieCountDown = null;
        ieRotate = null;
    }
    public override void LogiUpdate()
    {
        if (CheckUnitOnGround())
        {
            swichState.SwitchState(farmer.moveToAttackState);
        }

    }
}
