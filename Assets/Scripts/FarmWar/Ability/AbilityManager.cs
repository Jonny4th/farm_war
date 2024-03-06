using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_AbilitiesControllers;

    private IAbility[] m_Abilities;

    private void Awake()
    {
        m_Abilities = new IAbility[m_AbilitiesControllers.Length];

        for (int i = 0; i < m_AbilitiesControllers.Length; i++)
        {
            m_Abilities[i] = m_AbilitiesControllers[i].GetComponent<IAbility>();
        }
    }

    public IAbility ActivateAbility(int index)
    {

        return m_Abilities[index];
    }
}
