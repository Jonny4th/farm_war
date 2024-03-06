using DG.Tweening;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public bool CropIsReady;

    [SerializeField]
    private GameObject cropContainer;

    [SerializeField]
    private GameObject[] CropStateGameObjects;

    [SerializeField]
    private float GrowthTime;

    [SerializeField]
    private GameObject particle;

    [SerializeField]
    private float cropJump;
    [SerializeField]
    private float cropJumpDuration;

    [SerializeField]
    private GameObject point;
    [SerializeField]
    private Vector3 scale;

    public Plantable currentPlot;

    private int cropCurrentState;
    private int totalState;
    private float cropTimer;
    private bool particleIsPlay;

 
    private void Awake()
    {
        totalState = CropStateGameObjects.Length;
        cropTimer = 0;
        particleIsPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(cropTimer <= GrowthTime && cropCurrentState == 0 && CropStateGameObjects[0].gameObject.transform.position.y <= point.transform.position.y)
        {
            CropStateGameObjects[0].transform.position += new Vector3(0, 0.5f, 0) * Time.deltaTime; //Try Change Y if GrowthTime Change and can't see the crop
        }

        if(CropStateGameObjects[0].gameObject.transform.position.y >= point.transform.position.y && CropStateGameObjects[0].gameObject.transform.localScale != scale)
        {
            CropStateGameObjects[0].gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;
        }


        if (cropCurrentState != totalState - 1)
        {
            cropTimer += Time.deltaTime;
        }
        else
        {
            CropIsReady = true;
        }

        if (CropIsReady == true && particleIsPlay == false)
        {
            CropPartical();
            particleIsPlay = true;
        }

        if (cropTimer >= GrowthTime)
        {
            CropGrowing();
        }
    }

    private void CropGrowing()
    {
        CropStateGameObjects[cropCurrentState].SetActive(false);
        cropTimer = 0;
        cropCurrentState += 1;
        CropStateGameObjects[cropCurrentState].SetActive(true);
    }

    private void CropPartical()
    {
        Instantiate(particle, gameObject.transform.position, Quaternion.Euler(-90f, 0f, 0f));
    }

    public void CropStealing()
    {
        var move = transform.DOMove(transform.position + cropJump * Vector3.up, cropJumpDuration);
        move.onComplete += () =>
        {
            // Reset();
            // gameObject.SetActive(false);
            currentPlot.Crop = null;
            Destroy(this.gameObject);
        };
        Instantiate(particle, gameObject.transform.position, Quaternion.identity);
    }

    private void Reset()
    {
        cropTimer = 0;
        particleIsPlay = false;
        CropIsReady = false;
        foreach (var item in CropStateGameObjects)
        { item.SetActive(false); }
        CropStateGameObjects[0].SetActive(true);
    }
}
