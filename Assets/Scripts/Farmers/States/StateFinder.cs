using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateFinder : StateBase
{
    [SerializeField]
    [Tooltip("time to change state while onUnitOnGround")]
    protected float time = 2f;
    protected float timer = 0;
    public float Timer { get { return timer; } }
    protected int nodeIndex = -1;
    public float MoveNodeIndex { get { return nodeIndex; } }


    // public StateFinder(Farmer farmer, Animator animator, GameManager gameManager) : base(farmer, animator, gameManager)
    // {

    // }
    protected Node RandomNode(out int num)
    {
        num = Random.Range(0, manager.NodeMana);
        if (farmer.nodetarget)
            while (num == farmer.nodetarget.Index)
                num = Random.Range(0, manager.NodeMana);
        farmer.nodetarget = manager.NodeMana.FineNode(num);
        return farmer.nodetarget;
    }
}
