using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropGrowth : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private SOcrop _soCrop;
    [SerializeField] private int cropCurrentState;
    [SerializeField] private float cropTimer;
    [SerializeField] private GameObject particle;
    [SerializeField] private bool cropIsReady;
    [SerializeField] private bool particleIsPlay;
    [SerializeField] private float CropJump;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

    private void CropStealing()
    {
        rb.MovePosition(gameObject.transform.position + new Vector3(0,CropJump));
        Instantiate(particle, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject.GetComponent<CapsuleCollider>(), 0.1f);
        Destroy(this.gameObject, 0.5f);
    }

    private void OnMouseDown()
    {
        if(cropIsReady && particleIsPlay)
        CropStealing();
    }
}
