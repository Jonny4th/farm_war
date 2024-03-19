using System;
using System.Collections.Generic;
using UnityEngine;

public class Raidable : MonoBehaviour, IHealable
{
    [SerializeField]
    private List<Raid> m_RaidList = new();
    public List<Raid> RaidList => m_RaidList;

    [SerializeField]
    private int m_RaidLimit;
    public int RaidLimit => m_RaidLimit;

    [SerializeField]
    private bool m_IsRaidable;
    public bool IsRaidable => m_IsRaidable;

    public bool IsFullyOccupied => m_RaidList.Count >= m_RaidLimit;

    public bool IsHealingNeeded => m_RaidList.Count > 0;

    public event Action<Raid, Raidable> OnRaidStart;
    public event Action<Raidable> OnRaidEnd;

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

        raid.OnRaidCompleted.AddListener(CurrentRaidCompleteHandler);
        m_RaidList.Add(raid);
    }

    private void CurrentRaidCompleteHandler(Raid raid)
    {
        raid.OnRaidCompleted.RemoveListener(CurrentRaidCompleteHandler);
        OnRaidEnd?.Invoke(this);
    }

    public void RemoveFromRaidList(Raid raid)
    {
        if (m_RaidList.Contains(raid)) m_RaidList.Remove(raid);
    }

    public void Heal(float healPoints)
    {
        foreach(var r in m_RaidList)
        {
            r.Heal(healPoints);
        }
    }
}
