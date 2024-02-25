using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class FmAttackState : StateFinder
{
  // public FmAttackState(Farmer farmer, Animator animator, GameManager gameManager) : base(farmer, animator, gameManager)
  // {
  //   stateName = FarmerStrate.Attack;
  // }
  [SerializeField] private float attackTime = 0.5f;
  private float lastTime = 0;
  [SerializeField] private float damage = 5f;

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
      if (Time.time - lastTime > attackTime)
      {
        GameManager.instance.PlayerFaction.TakeDamage(damage);
      }
    }
    else
    {
      timer += Time.deltaTime;
      if (timer >= time)
      {
        timer = 0;
        farmer.targetMoving = RandomNode(out nodeIndex);
        manager.NodeMana.SetIndexToMove(farmer, nodeIndex);
        farmer.StateManager.SwitchState(farmer.moveState);
      }
    }

  }
  public override void PhysiUpdate()
  {

  }
}
