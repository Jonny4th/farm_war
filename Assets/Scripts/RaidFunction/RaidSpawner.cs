using UnityEngine;

public class RaidSpawner : MonoBehaviour
{
    [SerializeField]
    private Raid m_Original;

    public Raid Spawn(Vector3 location)
    {
        return Instantiate(m_Original, location, Quaternion.identity);
    }
}