using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToxicObject : MonoBehaviour
{
    private float m_timeDuration = 5f;
    [SerializeField] private ParticleSystem m_particleSystem;
    [SerializeField] private GameObject m_particleRoot;
    private Vector3 m_originalScale;
    [SerializeField] private float m_timeUp = 1f;
    [SerializeField] private float m_timeDown = 1f;
    private bool m_isActive = false;
    void Start()
    {
        m_originalScale = m_particleRoot.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ActiveSkill()
    {


        StartCoroutine(I_AnimationUp());

    }
    public void UnActiveSkill()
    {
        if (m_isActive) return;
        m_isActive = true;
        StartCoroutine(I_AnimationDown());
    }
    private IEnumerator I_CountDown()
    {
        yield return new WaitForSeconds(m_timeDuration);
        UnActiveSkill();
    }

    private IEnumerator I_AnimationUp()
    {
        float persent = 0;
        float lastTime = Time.time;
        m_particleRoot.transform.localScale = Vector3.zero;
        m_particleRoot.SetActive(true);
        while (persent < 1)
        {
            // Debug.Log($"{Time.time} / {timeCount} = {persent}");
            persent = (Time.time - lastTime) / m_timeUp;
            m_particleRoot.transform.localScale = Vector3.Lerp(Vector3.zero, m_originalScale, persent);
            yield return null;
        }
        m_particleSystem.Play();
        StartCoroutine(I_CountDown());
    }
    private IEnumerator I_AnimationDown()
    {
        float persent = 0;
        float lastTime = Time.time;
        while (persent < 1)
        {
            persent = (Time.time - lastTime) / m_timeDown;
            m_particleRoot.transform.localScale = Vector3.Lerp(m_originalScale, Vector3.zero, persent);
            yield return null;
        }
        m_particleRoot.SetActive(false);
        m_isActive = false;
    }
}
