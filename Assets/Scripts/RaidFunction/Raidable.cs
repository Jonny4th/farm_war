using UnityEngine;

public class Raidable : MonoBehaviour
{
    public bool IsRaidable => m_IsRaidable;
    public bool IsOccupied => m_IsOccupied;
    
    [SerializeField]
    private bool m_IsRaidable;

    [SerializeField]
    private bool m_IsOccupied;

    public void SetOccupied(bool isOccupied) => m_IsOccupied = isOccupied;

    public void SetRaidable(bool isRaidable) => m_IsRaidable = isRaidable;
}
