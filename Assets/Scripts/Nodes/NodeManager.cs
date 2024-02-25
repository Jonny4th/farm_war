using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] public List<Node> nodeCollcetion;
    public void SetIndexToMove(Farmer farmer, int index)
    {
        foreach (var T in nodeCollcetion) if (T.Index == index) T.Farmers.Add(farmer);
    }
    public void RemoveFarmer(Farmer farmer, int index)
    {
        foreach (var T in nodeCollcetion) if (T.Index == index) T.Farmers.Remove(farmer);
    }
    [SerializeField] private bool takeMutiUnit;

    public Node FineNode(int index)
    {
        foreach (Node T in nodeCollcetion) if (T.Index == index) return T;
        return null;
    }



    private void Start()
    {
        GameManager.instance.ResetEven += Reset;
        SetNode();
    }
    private void SetNode()
    {
        foreach (Node T in nodeCollcetion)
        {
            T.IsTakeMutiUnit = takeMutiUnit;
        }
    }
    private void Reset()
    {
        foreach (var T in nodeCollcetion)
        {
            T.Animas.Clear();
            T.Farmers.Clear();
        }
        SetNode();
    }


    private void OnDestroy()
    {
        GameManager.instance.ResetEven -= Reset;
    }





    public static implicit operator int(NodeManager nodeManager)
    {
        return nodeManager.nodeCollcetion.Count;
    }

}
