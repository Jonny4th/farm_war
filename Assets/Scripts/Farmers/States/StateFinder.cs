using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateFinder : StateBase
{
    [SerializeField]
    [Tooltip("time to change state while onUnitOnGround")]

    protected int nodeIndex = -1;
    public float MoveNodeIndex { get { return nodeIndex; } }



    protected void RandomMove()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            timer = 0;
            // farmer.Agent.SetDestination(RandomNode());
            farmer.Agent.isStopped = false;
        }
    }

    protected Node RandomNodeNotCurr(Node currNode)
    {
        var node = RandomNode();
        while (node == currNode)
            node = RandomNode();
        return node;
    }
    protected Node RandomNode()
    {
        return manager.NodeMana.nodeCollcetion[Random.Range(0, manager.NodeMana)];
    }



    protected AnimalTest UnitNearMe()
    {
        AnimalTest[] unit = new AnimalTest[3];
        float[] dis = new float[3];
        foreach (var u in manager.PlayerFaction.UnitInGrouind)
        {
            for (int i = 0; i < unit.Length; i++)
            {
                if (unit[i] == null)
                {
                    unit[i] = u;
                    dis[i] = CheckDistance(farmer, u.NodeTarget);
                }
                else
                {
                    var d = CheckDistance(farmer, u.NodeTarget);
                    if (dis[i] > d)
                    {
                        unit[i] = u;
                        dis[i] = d;
                    }
                }
            }
        }
        AnimalTest final;
        if (unit[0] != null && unit[1] != null) final = unit[Random.Range(0, unit.Length)];
        else final = unit[0];
        return final == null ? unit[0] : final;
    }
    protected float CheckDistance(Vector3 origin, Vector3 target) => Vector3.Distance(origin, target);





}
