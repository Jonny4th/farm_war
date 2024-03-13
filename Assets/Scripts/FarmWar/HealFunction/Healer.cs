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
        IsActivating = true;
        m_Visual.SetActive(true);
        var endTime = Time.time + m_HealingDuration;

        while (Time.time < endTime)
        {
            Patient.Heal(m_HealingRate);
            yield return new WaitForSeconds(1f);
        }

        m_Visual.SetActive(false);
        IsActivating = false;
    }

    public void SetPatient(IHealable patient)
    {
        Patient =  patient;
    }

}
