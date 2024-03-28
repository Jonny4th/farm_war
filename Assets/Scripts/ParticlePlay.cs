using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlay : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_particleSystem;

    private void Awake()
    {
        if (m_particleSystem == null)
            m_particleSystem = GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        m_particleSystem.Play();
    }

}
