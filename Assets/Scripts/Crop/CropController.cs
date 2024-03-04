using System;
using System.Collections.Generic;
using UnityEngine;

public class CropController : MonoBehaviour
{
    [SerializeField]
    Plantable[] plantables;
    public Plantable[] Plantables => plantables;

    [SerializeField]
    private Crop CropPrefab;

    [SerializeField]
    private List<Crop> m_Pool = new();

    [SerializeField]
    private Transform m_Container;

    public void RandomPlant()
    {
        var plot = Array.Find(plantables, x => x.Crop == null);

        if (plot == null) return;

        Spawn(plot);
    }

    public void Spawn(Plantable targetPlot)
    {
        // var plant = m_Pool.Find(x => !x.gameObject.activeSelf);

        // if (plant == null)
        // {
        //     plant = Instantiate(CropPrefab, targetPlot.transform.position, targetPlot.transform.rotation, m_Container);
        //     m_Pool.Add(plant);
        // }
        // else
        // {
        //     plant.gameObject.SetActive(true);
        //     plant.transform.position = targetPlot.transform.position;
        // }

        // plant.currentPlot = targetPlot;
        // targetPlot.Crop = plant;

        var plant = Instantiate(CropPrefab, targetPlot.transform.position, targetPlot.transform.rotation, m_Container);
        plant.currentPlot = targetPlot;
        targetPlot.Crop = plant;
    }
}
