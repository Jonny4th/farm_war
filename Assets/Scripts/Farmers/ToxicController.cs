using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ToxicController : MonoBehaviour
{

    [SerializeField] private List<ToxicObject> m_toxicObj;
    public List<ToxicObject> ToxicObj { get { return m_toxicObj; } set { m_toxicObj = value; } }
    private bool m_isSkillActive = false;
    public bool IsSkillActive { get { return m_isSkillActive; } set { m_isSkillActive = value; } }
    [SerializeField] private ToxicSetting m_setting;
    private float m_timeDuration = 10f;
    private float m_percenActive = 0.3f;
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
        if (ememyFaction.Hp <= (ememyFaction.MaxHp * 0.8f) && !m_isSkillActive)
        {
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
     //   m_isSkillActive = false;
    }
}
