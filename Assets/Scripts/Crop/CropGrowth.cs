using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropGrowth : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private SOcrop _soCrop;
    [SerializeField] private int cropCurrentState;
    [SerializeField] private float cropTimer;
    [SerializeField] private GameObject particle;
    [SerializeField] private bool cropIsReady;
    [SerializeField] private bool particleIsPlay;
    // Start is called before the first frame update
    void Start()
    {
        particleIsPlay = false;
        cropIsReady = false;
        cropCurrentState = 0;
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = _soCrop._cropstate[cropCurrentState];
    }

    // Update is called once per frame
    void Update()
    {
        if(cropCurrentState != _soCrop._cropstate.Length - 1)
        {
            cropTimer += Time.deltaTime;
        }
        else
        {
            cropIsReady = true;
        }

        if(cropIsReady == true && particleIsPlay == false)
        {
            CropPartical();
            particleIsPlay = true;
        }
        

        if(cropTimer >= _soCrop._timegrowth)
        {
            CropGrowing();
        }
    }

    private void CropGrowing()
    {
        cropTimer = 0;
        cropCurrentState += 1;
        meshFilter.mesh = _soCrop._cropstate[cropCurrentState];
    }

    private void CropPartical()
    {
        Instantiate(particle, gameObject.transform.position,Quaternion.identity);
    }
}
