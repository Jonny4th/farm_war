using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class StateBase : MonoBehaviour
{
    protected Animator animator;
    protected GameManager manager;
    protected Farmer farmer;
    protected FarmerStrate stateName;
    public string StateNameStr { get { return stateName.ToString(); } }


    // public StateBase(Farmer farmer, Animator animator, GameManager gameManager)
    // {
    //     this.farmer = farmer;
    //     this.animator = animator;
    //     this.manager = gameManager;

    // }
    public void Init(Farmer farmer, Animator animator, GameManager gameManager)
    {
        this.farmer = farmer;
        this.animator = animator;
        this.manager = gameManager;
    }

    public virtual void StartState()
    {
        farmer.currentState = stateName;
    }
    public virtual void EndState() { }
    public virtual void LogiUpdate() { }
    public virtual void PhysiUpdate() { }

    protected bool CheckUnitOnGround()
    {
        if (manager.PlayerFaction.UnitInGrouind.Count > 0)
            return true;
        else
            return false;
    }
    protected void UnitNearMe()
    {
        float dis = float.MaxValue;
        foreach (var u in manager.PlayerFaction.UnitInGrouind)
        {
            var d = CheckDistance(farmer.transform.position, u.transform.position);
            if (d < dis)
            {
                dis = d;
                farmer.unitTarget = u;
            }
        }
    }
    protected float CheckDistance(Vector3 origin, Vector3 target) => Vector3.Distance(origin, target);


}
