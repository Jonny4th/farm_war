using UnityEngine;

public class RaidSpawner : MonoBehaviour
{
    [SerializeField]
    private Raid m_RaidPrefab;

    public Raid Spawn(Vector3 location)
    {
        return Instantiate(m_RaidPrefab, location, Quaternion.identity);
    }

    public Raid Spawn(Vector3 location, Quaternion rotation)
    {
        return Instantiate(m_RaidPrefab, location, rotation);
    }

    public Raid Spawn(Vector3 location, Transform parent)
    {
        return Instantiate(m_RaidPrefab, location, Quaternion.identity, parent);
    }

    public Raid Spawn(Vector3 location, Quaternion rotation, Transform parent)
    {
        return Instantiate(m_RaidPrefab, location, rotation, parent);
    }
}