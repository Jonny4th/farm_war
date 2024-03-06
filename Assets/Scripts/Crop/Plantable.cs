using System;
using UnityEngine;

public class Plantable : MonoBehaviour
{
    public Crop Crop = null;

    public bool IsEmpty => Crop == null;
    public bool IsCropReady => Crop != null && Crop.IsReady;

    public event Action<Crop, Plantable> OnCropReady;
    public event Action<Plantable> OnCropStolen;

    public void AssignCrop(Crop crop)
    {
        Crop = crop;
        Crop.OnCropReady += CropReadyHandler;
        Crop.OnCropStolen += CropStolenHandler;
    }

    private void CropStolenHandler(Crop crop)
    {
        OnCropStolen?.Invoke(this);
        Crop = null;
    }

    private void CropReadyHandler(Crop crop)
    {
        OnCropReady?.Invoke(crop, this);
    }
}
