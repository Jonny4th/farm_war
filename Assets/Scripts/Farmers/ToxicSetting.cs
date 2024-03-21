using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ToxicSetting", menuName = "Enemy/ToxicSkill")]
public class ToxicSetting : ScriptableObject
{
    [Header("Toxic Controller")]
    [SerializeField] private float m_skillDuration = 60f;
    public float SkillDuration => m_skillDuration;
    [Range(0f, 1f)]
    [SerializeField] private float m_percenActive = 0.3f;
    public float PercentActive => m_percenActive;

    [Header("Toxic OBJ")]
    [SerializeField] private float m_reducTime = 10f;
    public float ReducTime => m_reducTime;
    [SerializeField] private float m_reducFrequency = 0.3f;
    public float ReducFrequency => m_reducTime;

    // [Header("ToxicList")]
    // [SerializeField] private List<ToxicObject> m_toxicList;
    // public List<ToxicObject> ToxicList => m_toxicList;
}
