using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RaidController : MonoBehaviour, IAbility
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

    [SerializeField]
    private int m_RaidActive;
    public int RaidActive => m_RaidActive;

    [SerializeField]
    private FloatReference m_Cost;
    public int Cost { get => (int)m_Cost; }

    public bool IsReady { get => Array.Exists(TargetList, x => x.IsRaidable && !x.IsFullyOccupied); }

    public event Action<Raid> OnRaidCompleted;

    private void Awake()
    {
        m_TargetList = FindObjectsOfType<Raidable>();
    }

    public Raid RandomSpawnOnGround()
    {
        var raidables = Array.FindAll(TargetList, x => x.IsRaidable && !x.IsFullyOccupied);

        var target = raidables[Random.Range(0, raidables.Length)];
        var pos = target.transform.position;

        if (CheckRaidPool(out var raid))
        {
            raid.transform.position = pos;
            raid.gameObject.SetActive(true);
        }
        else
        {
            raid = m_Spawner.Spawn(pos, m_RaidParent.rotation, m_RaidParent);
            m_RaidList.Add(raid);
            raid.OnRaidCompleted.AddListener(RaidCompleteHandler);
        }

        m_RaidActive++;
        raid.SetRaidTarget(target);

        return raid;
    }

    private void RaidCompleteHandler(Raid raid)
    {
        m_RaidActive--;
        OnRaidCompleted?.Invoke(raid);
    }

    /// <summary>
    /// return true if pool is available.
    /// </summary>
    /// <param name="raid"></param>
    /// <returns></returns>
    private bool CheckRaidPool(out Raid raid)
    {
        raid = null;
        if (m_RaidList.Count == 0) return false;
        raid = m_RaidList.Find(x => !x.gameObject.activeSelf);
        return raid != null;
    }
    public void ClearAllRaidList()
    {
        foreach (var T in m_RaidList)
        {
            Destroy(T.gameObject);
        }
        m_RaidList.Clear();
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }
}
