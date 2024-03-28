using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToxicController : MonoBehaviour
{

    [SerializeField] private List<ToxicObject> m_toxicObj;
    public List<ToxicObject> ToxicObj { get { return m_toxicObj; } set { m_toxicObj = value; } }
    private bool m_isSkillActive = false;
    public bool IsSkillActive { get { return m_isSkillActive; } set { m_isSkillActive = value; } }
    [SerializeField] private ToxicSetting m_setting;
    private float m_timeDuration = 10f;
    private float m_percenActive = 0.3f;
    private int m_numberFoskill = 1;
    private Action m_onSkillActive;
    public Action OnSkillActive { get { return m_onSkillActive; } set { m_onSkillActive = value; } }
    private Action m_onSkillEnd;
    public Action OnSkillEnd { get { return m_onSkillEnd; } set { m_onSkillEnd = value; } }
    private void Awake()
    {
        m_timeDuration = m_setting.SkillDuration;
        m_percenActive = m_setting.PercentActive;
    }
    void Start()
    {
        GameManager.instance.EmemyFaction.UpdateHp += Attacked;
        // foreach (var T in m_toxicObj)
        // {
        //   T.ToxicControll = this;
        // T.SetSeting(m_setting);
        //  }
        //  if (m_setting != null)
        //  {

        // }
    }

    // Update is called once per frame

    private void Attacked(EmemyFaction ememyFaction)
    {
        if (m_numberFoskill == 0) return;
        if (ememyFaction.Hp <= (ememyFaction.MaxHp * m_setting.PercentActive) && !m_isSkillActive)
        {
            m_numberFoskill--;
            m_onSkillActive?.Invoke();
            m_isSkillActive = true;
            foreach (var T in m_toxicObj)
            {
                if (T != null && !T.IsActive)
                {
                    T.ActiveSkill();
                }
            }
            StartCoroutine(I_CountDown());
        }
    }
    private IEnumerator I_CountDown()
    {
        yield return new WaitForSeconds(m_timeDuration);
        foreach (var T in m_toxicObj)
        {
            T.UnActiveSkill();
        }
        m_isSkillActive = false;
        m_onSkillEnd?.Invoke();
    }
}
