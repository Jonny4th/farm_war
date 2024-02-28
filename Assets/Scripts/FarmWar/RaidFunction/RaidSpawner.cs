using UnityEngine;
using UnityEngine.Pool;

public class RaidSpawner : MonoBehaviour
{
    [SerializeField]
    private Raid m_RaidPrefab;

    private ListPool<Raid> m_RaidPool;

    public Raid Spawn(Vector3 location)
    {
        return Instantiate(m_RaidPrefab, location, Quaternion.identity);
    }

    public Raid Spawn(Vector3 location, Transform parent)
    {
        return Instantiate(m_RaidPrefab, location, Quaternion.identity, parent);
    }
}