using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicObject : MonoBehaviour
{
    private float m_timeDuration = 5f;
    [SerializeField] private ParticleSystem m_particleSystem;
    [SerializeField] private GameObject m_particleRoot;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ActiveSkill()
    {
        m_particleRoot.SetActive(true);
        m_particleSystem.Play();
        StartCoroutine(I_CountDown());
    }
    private void UnActiveSkill()
    {
        m_particleRoot.SetActive(false);
    }
    private IEnumerator I_CountDown()
    {
        yield return new WaitForSeconds(m_timeDuration);
        UnActiveSkill();
    }
}
