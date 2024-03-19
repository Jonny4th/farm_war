using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IHealable))]
public class Healer : MonoBehaviour
{
    [SerializeField]
    private float m_HealingDuration;

    [Tooltip("Point per Second")]
    [SerializeField]
    private float m_HealingRate;

    [SerializeField]
    private GameObject m_Visual;

    public IHealable Patient {  get; private set; }
    public bool IsActivating { get; private set; }

    public event Action<Healer> OnHealingStarted;
    public event Action<Healer> OnHealing;
    public event Action<Healer> OnHealingEnded;

    private void Awake()
    {
        Patient = GetComponent<IHealable>();
    }

    public void ActivateHealing()
    {
        StartCoroutine(nameof(Healing));
    }

    private IEnumerator Healing()
    {
        OnHealingStarted?.Invoke(this);
        IsActivating = true;
        m_Visual.SetActive(true);
        var endTime = Time.time + m_HealingDuration;

        while (Time.time < endTime)
        {
            Patient.Heal(m_HealingRate);
            OnHealing?.Invoke(this);
            yield return new WaitForSeconds(1f);
        }

        m_Visual.SetActive(false);
        IsActivating = false;
        OnHealingEnded?.Invoke(this);
    }

    public void SetPatient(IHealable patient)
    {
        Patient =  patient;
    }

}
