using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


[System.Serializable]
public class FmAttackState : StateFinder
{


  private float lastTime = 0;
  [SerializeField] private float damage = 1f;
  [SerializeField] private bool lookAtUnti;
  [SerializeField] private bool attackWithTime;
  [SerializeField] private float attackTime = 0.5f;




  public override void StartState()
  {
    base.StartState();
    lastTime = (Time.time + attackTime);

    LookAt(farmer, RotaAngle(farmer.nodeToMove), lookAtSpeed, () =>
    {

      farmer.PlayerAnimation(FarmerStrate.Attack);


    });
  }
  public override void EndState()
  {

  }
  public override void LogiUpdate()
  {
    if (CheckUnitOnGround())
    {
      if (farmer.nodeToMove.Animas.Count == 0)
      {
        swichState.SwitchState(farmer.idelState);
      }
      if (attackWithTime)
      {
        if (Time.time >= lastTime)
        {
          lastTime = (Time.time + attackTime);
          Attack();
        }
      }


    }
    else
    {
      swichState.SwitchState(farmer.idelState);
    }

  }
  private void Attack()
  {
    GameManager.instance.PlayerFaction.TakeDamage(damage);
    farmer.nodeToMove.TakeDamage(damage);
  }
  public override void PhysiUpdate()
  {

  }
  public override void FormOtherColl()
  {
    Attack();
  }
}
