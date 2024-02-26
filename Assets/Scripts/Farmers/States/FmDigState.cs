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
  [SerializeField] private bool finishAnimationToSwicthState;
  private bool onPro;
  private bool canDig;
  public override void StartState()
  {
    base.StartState();
    agent.isStopped = true;
    farmer.PlayerAnimation(FarmerStrate.Idel);
    onPro = false;
    canDig = false;
    LookAt(farmer, RotaAngle(farmer.nodeToMove), lookAtSpeed, () =>
    {
      farmer.PlayerAnimation(FarmerStrate.Dig);
      canDig = true;
    });

  }
  public override void EndState()
  {
    StopAllCoroutines();
    farmer.PlayerAnimation(FarmerStrate.Idel);
  }
  public override void LogiUpdate()
  {
    if (CheckUnitOnGround())
    {
      swichState.SwitchState(farmer.moveToAttackState);
    }
    else
    {
      if (finishAnimationToSwicthState && !onPro && canDig)
      {
        onPro = true;
        CountToSwicthState(time, () => CountToSwicthState(animator.GetCurrentAnimatorStateInfo(0).length, () =>
        {
          int ran = Random.Range(1, 10);
          if (ran % 2 != 0)
            swichState.SwitchState(farmer.idelState);
          else
            swichState.SwitchState(farmer.moveState);
        }));
      }
      else
        CountToSwicthState(time, () =>
        {
          int ran = Random.Range(1, 10);
          if (ran % 2 != 0)
            swichState.SwitchState(farmer.idelState);
          else
            swichState.SwitchState(farmer.moveState);
        });
    }
  }
  public override void PhysiUpdate()
  {

  }
}
