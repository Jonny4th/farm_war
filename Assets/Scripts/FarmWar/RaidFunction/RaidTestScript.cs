using System.Collections.Generic;
using UnityEngine;

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
            TrySpawnOnGround();
        }
    }

    private void TrySpawnOnGround()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(!Physics.Raycast(ray, out var hit, 1000)) return;
        if(!hit.transform.TryGetComponent(out Raidable raidable)) return;

        if(!raidable.IsRaidable || raidable.IsFullyOccupied)
        {
            Debug.Log("Cannot spawn here.");
            return;
        }

        var pos = hit.transform.position;

        if(!CheckRaidPool(out var raid))
        {
            raid = raidController.Spawn(pos, raidParent);
            m_raidList.Add(raid);
        }
        else
        {
            raid.transform.position = pos;
            raid.gameObject.SetActive(true);
        }

        raid.OnRaidCompleted += RaidCompleteHandler;
        raid.SetRaidTarget(raidable);
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
        Debug.Log(raid != null);
        return raid != null;
    }
}
