using System;
using UnityEngine;

public class RaidableHealingController : MonoBehaviour, IAbility
{
    [SerializeField]
    private Healer[] m_Healers;

    [SerializeField]
    private FloatReference m_Cost;

    public event Action<Healer> OnHealingStarted;
    public event Action<Healer> OnHealing;
    public event Action<Healer> OnHealingEnded;

    private void Awake()
    {
        Array.ForEach(m_Healers, x =>
        {
            x.OnHealingStarted += h => OnHealingStarted?.Invoke(h);
            x.OnHealing += h => OnHealing?.Invoke(h);
            x.OnHealingEnded += h => OnHealingEnded?.Invoke(h);
        });
    }

    public int Cost => (int)m_Cost;
    public bool IsReady => Array.Exists(m_Healers, x => x.Patient.IsHealingNeeded && !x.IsActivating);

    public void HealRandomly()
    {
        var healers = Array.FindAll(m_Healers, x => x.Patient.IsHealingNeeded && !x.IsActivating);

        if (healers.Length == 0)
        {
            healers = m_Healers;
        }

        healers.GetRandom().ActivateHealing();
    }

    public void Execute()
    {
        HealRandomly();
    }
}
