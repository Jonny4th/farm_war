using TMPro;
using UnityEngine;

public class FloatReferenceToText : MonoBehaviour
{
    [SerializeField]
    FloatReference m_Value;

    [SerializeField]
    TMP_Text m_Display;

    private void Awake()
    {
        if(m_Value == null) return;
        m_Value.OnVariableChanged += ValueChangeHandler;

        if(m_Display == null) return;
        m_Display.text = m_Value.ToString();
    }

    private void ValueChangeHandler(float value)
    {
        if(m_Display == null) return;
        m_Display.text = value.ToString();
    }
}
