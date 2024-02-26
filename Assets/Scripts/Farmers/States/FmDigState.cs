using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class FmDigState : StateBase
{

  // public FmDigState(Farmer farmer, Animator animator, GameManager gameManager) : base(farmer, animator, gameManager)
  // {
  //   stateName = FarmerStrate.Dig;
  // }

  public override void StartState()
  {
    base.StartState();
    agent.isStopped = true;
    LookAt(farmer, RotaAngle(farmer.nodeToMove), lookAtSpeed);


  }
  public override void EndState()
  {
    StopAllCoroutines();
  }
  public override void LogiUpdate()
  {
    if (CheckUnitOnGround())
    {
      swichState.SwitchState(farmer.moveToAttackState);
    }
    else
    {
      CountToSwicthState(time, () => swichState.SwitchState(farmer.moveState));
    }
  }
  public override void PhysiUpdate()
  {

  }
}
