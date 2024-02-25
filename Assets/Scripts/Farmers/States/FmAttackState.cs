using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class FmAttackState : StateFinder
{

  [SerializeField] private float attackTime = 0.5f;
  private float lastTime = 0;
  [SerializeField] private float damage = 1f;

  public override void StartState()
  {
    base.StartState();
    lastTime = 0;
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
        swichState.SwitchState(farmer.moveToAttackState);
      }
      if (Time.time - lastTime > attackTime)
      {
        lastTime = Time.time;
        GameManager.instance.PlayerFaction.TakeDamage(damage);
        farmer.nodeToMove.TakeDamage(damage);
      }
    }
    else
    {
      swichState.SwitchState(farmer.idelState);
    }

  }
  public override void PhysiUpdate()
  {

  }
}
