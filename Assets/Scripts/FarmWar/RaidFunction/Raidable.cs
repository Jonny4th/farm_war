using System.Collections.Generic;
using UnityEngine;

public class Raidable : MonoBehaviour
{
    [SerializeField]
    private HashSet<Raid> m_RaidList = new();
    public HashSet<Raid> RaidList => m_RaidList;

    [SerializeField]
    private int m_RaidLimit;
    public int RaidLimit => m_RaidLimit;

    [SerializeField]
    private bool m_IsRaidable;
    public bool IsRaidable => m_IsRaidable; 

    public bool IsFullyOccupied => m_RaidList.Count >= m_RaidLimit;

    public void SetRaidable(bool isRaidable)
    {
        m_IsRaidable = isRaidable;
    }

    public void AddToRaidList(Raid raid)
    {
        if(!IsRaidable) return;
        if(m_RaidList.Contains(raid)) return;
        m_RaidList.Add(raid);
    }

    public void RemoveFromRaidList(Raid raid)
    {
        if(m_RaidList.Contains(raid)) m_RaidList.Remove(raid);
    }
}
