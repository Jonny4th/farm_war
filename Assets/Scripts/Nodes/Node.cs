using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{

    [SerializeField] private int index;
    public int Index { get { return index; } }

    public List<AnimalTest> animalTests;
    public List<Farmer> farmers;



    private void OnTriggerEnter(Collider other)
    {
        var f = other.GetComponent<Farmer>();
        if (FarmerInLIst(f))
        {
            if (animalTests.Count > 0)
                f.StateManager.SwitchState(f.attackState);
            else
            {
                int ran = Random.Range(1, 10);
                if (ran % 2 == 0)
                    f.StateManager.SwitchState(f.digState);
                else
                    f.StateManager.SwitchState(f.idelState);
            }
        }
    }
    private bool FarmerInLIst(Farmer farmer)
    {
        return farmers.Contains(farmer);
    }
    public static implicit operator Vector3(Node node)
    {
        return node.transform.position;
    }
}
