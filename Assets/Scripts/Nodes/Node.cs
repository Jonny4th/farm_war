using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{

    [SerializeField] private int index;
    public int Index { get { return index; } }

    [SerializeField] private List<AnimalTest> animas;
    public List<AnimalTest> Animas { get { return animas; } }
    [SerializeField] private List<Farmer> farmers;
    public List<Farmer> Farmers { get { return farmers; } }
    [SerializeField] private bool isTakeMutiUnit;
    public bool IsTakeMutiUnit { get { return isTakeMutiUnit; } set { isTakeMutiUnit = value; } }
    public void RemoveAnimal(AnimalTest animalTest)
    {
        animas.Remove(animalTest);
        animalTest.Des();
    }

    private void OnTriggerEnter(Collider other)
    {
        // var f = other.GetComponent<Farmer>();
        // if (FarmerInLIst(f))
        // {
        //     if (animas.Count > 0)
        //         f.StateManager.SwitchState(f.attackState);
        //     else
        //     {
        //         int ran = Random.Range(1, 10);
        //         if (ran % 2 == 0)
        //             f.StateManager.SwitchState(f.digState);
        //         else
        //             f.StateManager.SwitchState(f.idelState);
        //     }
        // }
    }



    public void TakeDamage(float damage)
    {
        foreach (var T in animas.ToArray())
        {
            T.TakeDamage(damage);
        }
        // animas[0].TakeDamage(damage);
    }








    private bool FarmerInLIst(Farmer farmer)
    {
        return farmers.Contains(farmer);
    }
    public static implicit operator Vector3(Node node)
    {
        return node.transform.position;
    }
    public static implicit operator Quaternion(Node node)
    {
        return node.transform.rotation;
    }
}
