using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicController : MonoBehaviour
{

    [SerializeField] private List<ToxicObject> m_toxicObj;
    private bool m_isSkillActive = false;
    void Start()
    {
        GameManager.instance.EmemyFaction.UpdateHp += Attacked;
    }

    // Update is called once per frame
  
    private void Attacked(EmemyFaction ememyFaction)
    {
        if (ememyFaction.Hp <= (ememyFaction.MaxHp / 2f) && !m_isSkillActive)
        {
            m_isSkillActive = true;
            foreach (var T in m_toxicObj)
            {
                if (T != null)
                {
                    T.ActiveSkill();
                }
            }
        }
    }
}
