using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] public List<Node> nodeCollcetion;
    public void SetIndexToMove(Farmer farmer, int index)
    {
        foreach (var T in nodeCollcetion) if (T.Index == index) T.farmers.Add(farmer);
    }
    public void RemoveFarmer(Farmer farmer, int index)
    {
        foreach (var T in nodeCollcetion) if (T.Index == index) T.farmers.Remove(farmer);
    }
    public Node FineNode(int index)
    {
        foreach (Node T in nodeCollcetion) if (T.Index == index) return T;
        return null;
    }

    public static implicit operator int(NodeManager nodeManager)
    {
        return nodeManager.nodeCollcetion.Count;
    }

}
