using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CropTestScript : MonoBehaviour
{
    [SerializeField]
    CropController controller;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            controller.RandomPlant();
        }

        if(Input.GetKeyUp(KeyCode.A))
        {
            var plots = Array.FindAll(controller.Plantables, x => x.Crop != null && x.Crop.CropIsReady);
            if (plots.Length == 0) return;

            var plot = plots[Random.Range(0, plots.Length)];
            plot.Crop.CropStealing();
        }
    }
}