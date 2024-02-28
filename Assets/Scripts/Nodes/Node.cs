using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{

    [SerializeField] private Raidable raidable;
    public Raidable raidablee { get { return raidable; } }
    [SerializeField] private List<AnimalTest> animas;
    public List<Raid> Raids { get { return raidablee.RaidList; } }
    [SerializeField] private List<Farmer> farmers;
    public List<Farmer> Farmers { get { return farmers; } }






    private void Awake()
    {
        if (raidable == null) raidable = GetComponent<Raidable>();
    }

    public void RemoveAnimal(AnimalTest Raid)
    {
        animas.Remove(Raid);
        Raid.Des();
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
