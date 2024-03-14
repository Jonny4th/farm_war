using System.Collections;
using System.Collections.Generic;
using FarmWar.Core;
using UnityEngine;
using System;

public class ToxicObject : MonoBehaviour
{

    [SerializeField] private ParticleSystem m_particleSystem;
    [SerializeField] private GameObject m_particleRoot;
    private Vector3 m_originalScale;
    [SerializeField] private float m_timeUp = 1f;
    [SerializeField] private float m_timeDown = 1f;
    private List<Raid> m_raidAlreadyHit = new List<Raid>();
    private ToxicController m_toxicController;
    public ToxicController ToxicControll { get { return m_toxicController; } set { m_toxicController = value; } }
    private Node m_node;

    private bool m_isActive = false;
    public bool IsActive => m_isActive;
    [SerializeField] private float m_reducTime = 50f;
    [SerializeField] private float m_reducFrequency = 0.3f;

    private void Awake()
    {
        m_node = transform.parent.GetComponent<Node>();
    }
    void Start()
    {
        m_originalScale = m_particleRoot.transform.localScale;
    }

    void Update()
    {

    }
    private void FixedUpdate()
    {

    }
    public void ActiveSkill()
    {
       
        m_isActive = true;
        StartCoroutine(I_AnimationUp());
    }
    public void UnActiveSkill()
    {
        m_isActive = false;
        StartCoroutine(I_AnimationDown());
    }

    private IEnumerator I_SkillActive()
    {
        while (true)
        {
            if (m_node.Raidable.RaidList.Count > 0)
            {
                foreach (var T in m_node.Raidable.RaidList)
                {
                    T.TakeDamage(m_reducTime);
                }
            }
            yield return new WaitForSeconds(m_reducFrequency);
        }
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
        m_node.IsToxic = true;
        m_particleSystem.Play();
        StartCoroutine(I_SkillActive());
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
        m_node.IsToxic = false;
        m_toxicController.IsSkillActive = false;
    }
}
