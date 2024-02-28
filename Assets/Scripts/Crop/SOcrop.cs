using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Crop", menuName = "ScriptableObject/CropScriptableObject")]
public class SOcrop : ScriptableObject
{
    [SerializeField] private string cropName;
    [SerializeField] private float timeGrowth;
    [SerializeField] private Mesh[] cropState;

    public float _timegrowth => timeGrowth;
    public Mesh[] _cropstate => cropState;
}
