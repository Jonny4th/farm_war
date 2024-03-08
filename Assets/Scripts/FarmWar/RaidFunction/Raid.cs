using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Raid : MonoBehaviour, IDamageable
{
    public UnityEvent<Raid> OnRaidCompleted; // send when raid stops.
    public UnityEvent<Raid> OnOutAnimationDone; // send when animaiton out had done.

    [Tooltip("Life span of the prefab in seconds.")]
    [SerializeField]
    private float m_LifeTime;

    [SerializeField]
    private float m_RemainLifeTime;
    public float RemainLifeTime => m_RemainLifeTime;

    [SerializeField]
    private FloatReference m_NormalizedTime;

    [SerializeField]
    private Animator m_Animator;

    private Raidable currentTarget;

    void OnEnable()
    {
        m_RemainLifeTime = m_LifeTime;
        StartCoroutine(UpdateLife());
    }

    IEnumerator UpdateLife()
    {
        while (m_RemainLifeTime > 0)
        {
            m_RemainLifeTime -= Time.deltaTime;
            m_NormalizedTime.Value = (m_RemainLifeTime / m_LifeTime);
            yield return null;
        }

        OnRaidCompleted?.Invoke(this);
        m_Animator.SetTrigger("Done");
    }

    public void SetRaidTarget(Raidable target)
    {
        target.AddToRaidList(this);
        currentTarget = target;
    }

    public void OnAnimationOutDone() // Used by animation event
    {
        if (currentTarget != null)
        {
            currentTarget.RemoveFromRaidList(this);
            currentTarget = null;
        }

        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        m_RemainLifeTime -= damage;
    }
}