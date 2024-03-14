using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ToxicController : MonoBehaviour
{

    [SerializeField] private List<ToxicObject> m_toxicObj;
    private bool m_isSkillActive = false;
    public bool IsSkillActive { get { return m_isSkillActive; } set { m_isSkillActive = value; } }
    [SerializeField] private float m_timeDuration = 30f;
    void Start()
    {
        GameManager.instance.EmemyFaction.UpdateHp += Attacked;
        foreach (var T in m_toxicObj)
        {
            T.ToxicControll = this;
        }
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
        Debug.Log("FDFDFDFDFDFDFDFDFDFD");
        yield return new WaitForSeconds(m_timeDuration);
        Debug.Log("====================");
        foreach (var T in m_toxicObj)
        {

            T.UnActiveSkill();

        }

    }
}
