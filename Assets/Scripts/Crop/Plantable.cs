using System;
using UnityEngine;

public class Plantable : MonoBehaviour
{
    public Crop Crop = null;

    public bool IsEmpty => Crop == null;
    public bool IsCropReady => Crop != null && Crop.IsReady;

    public event Action<Crop, Plantable> OnCropReady;
    public event Action<Plantable> OnCropGone;

    public void AssignCrop(Crop crop)
    {
        Crop = crop;
        Crop.OnCropReady += CropReadyHandler;
        Crop.OnCropGone += CropGoneHandler;
    }

    private void CropGoneHandler(Crop crop)
    {
        OnCropGone?.Invoke(this);
        Crop = null;
    }

    private void CropReadyHandler(Crop crop)
    {
        OnCropReady?.Invoke(crop, this);
    }
}
