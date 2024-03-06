using FarmWar.ShieldFunction;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private Raidable raidable;
    public Raidable Raidable { get { return raidable; } }

    public List<Raid> Raids { get { return raidable.RaidList; } }



    [SerializeField] private List<Farmer> farmers;
    public List<Farmer> Farmers { get { return farmers; } }

    public Plantable plantable;

    private void Awake()
    {
        if (raidable == null) raidable = GetComponent<Raidable>();
        Shield.OnShieldBreak += ShieldBreakHandler;
        plantable.OnCropReady += CropReadyHandler;
        plantable.OnCropGone += CropGoneHandler;
        raidable.OnRaidEnd += RaidEndHandler;
    }

    private void RaidEndHandler(Raidable raidable)
    {
        plantable.Crop.CropStealing();
    }

    private void CropGoneHandler(Plantable plantable)
    {
        raidable.SetRaidable(false);
    }

    private void CropReadyHandler(Crop crop, Plantable plantable)
    {
        raidable.SetRaidable(true);
    }

    private void ShieldBreakHandler()
    {
        Shield.gameObject.SetActive(false);
    }

    public void ActivateShield(int maxHit)
    {
        Shield.SetMaxHit(maxHit);
        Shield.gameObject.SetActive(true);
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
