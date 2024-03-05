using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StateFinder : StateBase
{

    protected Node RandomNodeNotCurr(Node currNode)
    {
        var node = RandomNode();
        while (node == currNode)
            node = RandomNode();
        return node;
    }

    protected Node RandomNode()
    {
        var n = manager.NodeMana.nodeCollcetion.FindAll(x => x.plantable.Crop == null);
        return n[Random.Range(0, n.Count)];
        // return manager.NodeMana.nodeCollcetion[Random.Range(0, manager.NodeMana)];
    }

    protected List<Node> FindNodeObj(List<Node> noodeList, int count = 2)
    {
        List<Node> nodeTarget = new List<Node>();
        List<float> closeNode = new List<float>();

        foreach (var T in noodeList)
        {
            float dis = CheckDistance(T, farmer);

            for (int i = 0; i < nodeTarget.Count; i++)
            {
                if (dis < closeNode[i])
                {
                    closeNode.Insert(i, dis);
                    nodeTarget.Insert(i, T);
                }

                if (nodeTarget.Count > count)
                {
                    closeNode.RemoveAt(count);
                    nodeTarget.RemoveAt(count);
                }
                break;
            }
            if (nodeTarget.Count < count)
            {
                nodeTarget.Add(T);
                closeNode.Add(dis);
            }
        }
        return nodeTarget;
    }
    protected Raid FindRaidInNode(Node node)
    {
        return node.Raids[Random.Range(0, node.Raids.Count)];
    }

    protected float CheckDistance(Vector3 origin, Vector3 target) => Vector3.Distance(origin, target);

}
