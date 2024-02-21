using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class StateBase
{
    protected Animator animator;
    protected GameManager manager;
    protected Farmer farmer;

    public StateBase(Farmer farmer, Animator animator, GameManager gameManager)
    {
        this.farmer = farmer;
        this.animator = animator;
        this.manager = gameManager;
    }

    public virtual void StartState() { }
    public virtual void EndState() { }
    public virtual void LogiUpdate() { }
    public virtual void PhysiUpdate() { }

}
