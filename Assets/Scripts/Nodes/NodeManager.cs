using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] public List<Node> nodeCollcetion;






    private void Start()
    {
        GameManager.instance.ResetEven += Reset;
        // SetNode();
    }
    // private void SetNode()
    // {
    //     foreach (Node T in nodeCollcetion)
    //     {
    //         T.IsTakeMutiUnit = takeMutiUnit;
    //     }
    // }
    private void Reset()
    {
        // foreach (var T in nodeCollcetion)
        // {
        // T.Animas.Clear();
        // T.Farmers.Clear();
        // }

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
