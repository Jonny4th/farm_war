using System.Collections.Generic;
using UnityEngine;

public class RaidTestScript : MonoBehaviour
{
    [SerializeField]
    private RaidSpawner raidController;

    public List<Raidable> m_targetList;
    public List<Raid> m_raidList = new();

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
        if(!raidable.IsRaidable || raidable.IsOccupied) return;

        var pos = hit.transform.position;
        var raid = raidController.Spawn(pos);

        raid.OnRaidComplete += RaidCompleteHandler;
        raid.SetRaidTarget(raidable);

        m_raidList.Add(raid);
    }

    private void RaidCompleteHandler(Raid raid)
    {
        m_raidList.Remove(raid);
    }
}
