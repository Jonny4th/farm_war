using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


[System.Serializable]
public class FmAttackState : StateFinder
{


  private float lastTime = 0;
  [SerializeField] private float damage = 1f;
  [SerializeField] private float lifeTimeReduce = 1f;
  [SerializeField] private bool lookAtUnti;
  [SerializeField] private bool attackWithTime;
  [SerializeField] private float attackTime = 0.5f;


  [SerializeField]
  [Tooltip("T => farmer will look at node,F => farmer will look at raid 0")]
  private bool lookAtNode;


  protected Raid raidTarget;

  public override void StartState()
  {
    base.StartState();
    farmer.SwicthTool(2);
    lastTime = (Time.time + attackTime);
    raidTarget = FindRaidInNode(farmer.nodetarget);
    var targetRotate = lookAtNode ? RotaAngle(farmer.nodetarget) : RotaAngle(raidTarget.transform.position);
    LookAt(farmer, targetRotate, lookAtSpeed, () =>
    {
      farmer.PlayerAnimation(FarmerStrate.Attack);
    });
  }

  public override void LogiUpdate()
  {
    if (CheckUnitOnGround())
    {
      if (farmer.nodetarget.Raids.Count == 0)
      {
        swichState.SwitchState(farmer.moveToAttackState);
      }


      // if (attackWithTime)
      // {
      //   if (Time.time >= lastTime)
      //   {
      //     lastTime = (Time.time + attackTime);
      //     Attack();
      //   }
      // }
    }
    else
    {
      swichState.SwitchState(farmer.moveToAttackState);
    }
  }
  private void Attack()
  {
    GameManager.instance.PlayerFaction.TakeDamage(damage);
    raidTarget.TakeDamage(lifeTimeReduce);
  }
  public override void FormOtherColl()
  {
    Attack();
  }
}
