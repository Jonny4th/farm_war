using System;
using System.Collections;
using UnityEngine;

public class Raid : MonoBehaviour
{
    public event Action<Raid> OnRaidComplete;

    [Tooltip("Life span of the prefab in seconds.")]
    [SerializeField]
    private float m_LifeTime;

    [SerializeField]
    private float m_RemainLifeTime;
    public float RemainLifeTime => m_RemainLifeTime;

    [SerializeField]
    private Animator m_Animator;

    private Raidable currentTarget;

    void Awake()
    {
        m_RemainLifeTime = m_LifeTime;
        StartCoroutine(UpdateLife());
    }

    IEnumerator UpdateLife()
    {
        while(m_RemainLifeTime > 0)
        {
            m_RemainLifeTime -= Time.deltaTime;
            yield return null;
        }
        OnRaidComplete?.Invoke(this);
        m_Animator.SetTrigger("Done");
    }

    public void SetRaidTarget(Raidable target)
    {
        target.SetOccupied(true);
        currentTarget = target;
    }

    public void OnAnimationOutDone()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if(currentTarget == null) return;
        currentTarget.SetOccupied(false);
    }
}