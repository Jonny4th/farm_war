using FarmWar.ShieldFunction;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShieldContoller : MonoBehaviour, IAbility
{
    [SerializeField]
    Shield[] m_Shields;

    [SerializeField]
    private FloatReference m_Cost;
    public int Cost => (int)m_Cost;

    public bool IsReady => throw new NotImplementedException();

    public Shield ActivateShieldRandomly(int shieldHitPoint)
    {
        var nonActivatedShields = Array.FindAll(m_Shields, x => x.IsActivate == false);

        Debug.Log(nonActivatedShields.Length);
        if (nonActivatedShields == null) return null;
        var shield = nonActivatedShields[Random.Range(0, nonActivatedShields.Length)];
        shield.ActivateShield(shieldHitPoint);
        return shield;
    }

    public Shield ActivateShield(int index, int hit)
    {
        m_Shields[index].ActivateShield(hit);
        return m_Shields[index];
    }

    public void Execute()
    {
    }
}
