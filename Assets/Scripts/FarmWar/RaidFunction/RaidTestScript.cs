using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RaidTestScript : MonoBehaviour
{
    public Raidable[] m_targetList;
    public List<Raid> m_raidList = new();

    [SerializeField]
    private RaidSpawner raidController;

    [SerializeField]
    private Transform raidParent;

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            RandomSpawnOnGround();
        }
    }

    private void RandomSpawnOnGround()
    {
        var raidables = Array.FindAll(m_targetList, x => x.IsRaidable && !x.IsFullyOccupied);
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
            raid = raidController.Spawn(pos, raidParent);
            m_raidList.Add(raid);
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
        if(m_raidList.Count == 0) return false;
        raid = m_raidList.Find(x => !x.gameObject.activeSelf);
        return raid != null;
    }
}
