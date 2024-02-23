using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{

    [SerializeField] private int index;
    public float Index { get { return index; } }





    private void OnTriggerEnter(Collider other)
    {
        var f = other.GetComponent<Farmer>();
        if (f)
        {
            
        }
    }

}
