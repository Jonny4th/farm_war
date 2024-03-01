using System.Collections.Generic;
using UnityEngine;

public class Raidable : MonoBehaviour
{
    [SerializeField]
    private List<Raid> m_RaidList = new();
    public List<Raid> RaidList => m_RaidList;

    [SerializeField]
    private int m_RaidLimit;
    public int RaidLimit => m_RaidLimit;

    [SerializeField]
    private bool m_IsRaidable;
    public bool IsRaidable => plantable.Crop == null ? false : plantable.Crop.CropIsReady;

    public bool IsFullyOccupied => m_RaidList.Count >= m_RaidLimit;

    public Plantable plantable;




    private void Awake()
    {
        plantable = GetComponent<Plantable>();
    }


    public void SetRaidable(bool isRaidable)
    {
        m_IsRaidable = isRaidable;
    }

    public void AddToRaidList(Raid raid)
    {
        if (!IsRaidable) return;
        if (m_RaidList.Contains(raid))
        {
            Debug.Log("Raid already in list");
            return;
        }

        m_RaidList.Add(raid);
    }

    public void RemoveFromRaidList(Raid raid)
    {
        if (m_RaidList.Contains(raid)) m_RaidList.Remove(raid);
    }
}
