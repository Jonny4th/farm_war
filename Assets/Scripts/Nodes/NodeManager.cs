using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] public List<Node> nodeCollcetion;

    private void Start()
    {
        GameManager.instance.ResetEven += ResetGame;
        // SetNode();
    }

    private void ResetGame(GameManager gameManager)
    {
        // foreach (var T in nodeCollcetion)
        // {
        // T.Animas.Clear();
        // T.Farmers.Clear();
        // }

    }

    private void OnDestroy()
    {
        GameManager.instance.ResetEven -= ResetGame;
    }

    public static implicit operator int(NodeManager nodeManager)
    {
        return nodeManager.nodeCollcetion.Count;
    }
}
