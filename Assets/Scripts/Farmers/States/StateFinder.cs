using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StateFinder : StateBase
{


   
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
    protected Node RaidNodeNearMe()
    {
        Node[] unit = new Node[2];
        float[] dis = new float[2];
        Node[] raidables = manager.NodeMana.nodeCollcetion.FindAll(x => x.Raids.Count > 0).ToArray();
        foreach (var u in raidables)
        {
            for (int i = 0; i < unit.Length; i++)
            {
                if (unit[i] == null)
                {
                    unit[i] = u;
                    dis[i] = CheckDistance(farmer, u.transform.position);
                }
                else
                {
                    var d = CheckDistance(farmer, u.transform.position);
                    if (dis[i] > d)
                    {
                        unit[i] = u;
                        dis[i] = d;
                    }
                }
            }
        }
        Node r = unit[Random.Range(0, unit.Length)];
        return r == null ? unit[0] : r;
    }
    protected Raid UnitNearMe1(Raidable raidable)
    {
        Raid[] unit = new Raid[3];
        float[] dis = new float[3];
        foreach (var u in raidable.RaidList)
        {
            for (int i = 0; i < unit.Length; i++)
            {
                if (unit[i] == null)
                {
                    unit[i] = u;
                    dis[i] = CheckDistance(farmer, u.transform.position);
                }
                else
                {
                    var d = CheckDistance(farmer, u.transform.position);
                    if (dis[i] > d)
                    {
                        unit[i] = u;
                        dis[i] = d;
                    }
                }
            }
        }
        Raid final;
        if (unit[0] != null && unit[1] != null) final = unit[Random.Range(0, unit.Length)];
        else final = unit[0];
        return final == null ? unit[0] : final;
    }
    protected float CheckDistance(Vector3 origin, Vector3 target) => Vector3.Distance(origin, target);





}
