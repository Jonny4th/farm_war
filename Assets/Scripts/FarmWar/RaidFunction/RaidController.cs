using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RaidController : MonoBehaviour
{
    [SerializeField]
    private Raidable[] m_TargetList;
    public Raidable[] TargetList => m_TargetList;

    [SerializeField]
    private List<Raid> m_RaidList = new();
    public List<Raid> RaidList => m_RaidList;

    [SerializeField]
    private RaidSpawner m_Spawner;

    [SerializeField]
    private Transform m_RaidParent;

    public void RandomSpawnOnGround()
    {
        var raidables = Array.FindAll(TargetList, x => x.IsRaidable && !x.IsFullyOccupied);
        if(raidables.Length == 0) return;

        var target = raidables[Random.Range(0, raidables.Length)];
        var pos = target.transform.position;

        if(CheckRaidPool(out var raid))
        {
            raid.transform.position = pos;
            raid.gameObject.SetActive(true);
        }
        else
        {
            raid = m_Spawner.Spawn(pos, m_RaidParent);
            m_RaidList.Add(raid);
        }

        raid.OnRaidCompleted.AddListener(RaidCompleteHandler);
        raid.SetRaidTarget(target);
    }

    private void RaidCompleteHandler(Raid raid)
    {
    }

    /// <summary>
    /// return true if pool is available.
    /// </summary>
    /// <param name="raid"></param>
    /// <returns></returns>
    public bool CheckRaidPool(out Raid raid)
    {
        raid = null;
        if(m_RaidList.Count == 0) return false;
        raid = m_RaidList.Find(x => !x.gameObject.activeSelf);
        return raid != null;
    }
}
