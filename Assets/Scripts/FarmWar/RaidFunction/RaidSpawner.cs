using UnityEngine;
using UnityEngine.Pool;

public class RaidSpawner : MonoBehaviour
{
    [SerializeField]
    private Raid m_Original;

    private ListPool<Raid> m_RaidPool;

    public Raid Spawn(Vector3 location)
    {
        return Instantiate(m_Original, location, Quaternion.identity);
    }

    public Raid Spawn(Vector3 location, Transform parent)
    {
        return Instantiate(m_Original, location, Quaternion.identity, parent);
    }
}