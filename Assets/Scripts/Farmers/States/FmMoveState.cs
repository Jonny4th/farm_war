using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class FmMoveState : StateBase
{
    [SerializeField] protected float timeToRotate = 2f;



    protected IEnumerator enumera;



    // public FmMoveState(Farmer farmer, Animator animator, GameManager gameManager) : base(farmer, animator, gameManager)
    // {
    //     stateName = FarmerStrate.Move;
    // }

    public override void StartState()
    {
        base.StartState();
        farmer.Agent.isStopped = true;
        enumera = null;
    }
    public override void EndState()
    {
        farmer.Agent.isStopped = true;
        manager.NodeMana.RemoveFarmer(farmer, farmer.nodetarget.Index);
        if (enumera != null) StopCoroutine(enumera);
    }
    public override void LogiUpdate()
    {
        if (CheckUnitOnGround())
        {

        }
        else
        {
            farmer.Agent.isStopped = false;
            // if (enumera != null)
            //     StartCoroutine(RotateTO(farmer.transform.rotation, RotateTarget(farmer.nodetarget.transform.position), timeToRotate));
        }
    }
    public override void PhysiUpdate()
    {
        farmer.Agent.SetDestination(farmer.targetMoving);
    }
    private Quaternion RotateTarget(Vector3 pos)
    {
        Vector3 dir = (pos - farmer.transform.position).normalized;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, angle, 0f);
    }
    private IEnumerator RotateTO(Quaternion start, Quaternion target, float duration)
    {
        float timer = 0;

        while (timer < duration)
        {
            farmer.transform.rotation = Quaternion.Lerp(start, target, timer / duration);
            yield return null;
        }
        farmer.transform.rotation = target;
        farmer.Agent.isStopped = false;
        enumera = null;
    }
}
