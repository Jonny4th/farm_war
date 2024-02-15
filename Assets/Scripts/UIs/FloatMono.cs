using UnityEngine;
using UnityEngine.Events;

public class FloatMono : MonoBehaviour
{
    [SerializeField]
    private float m_Value;
    public float Value => m_Value;

    public UnityEvent<float> OnValueChanged;

    public void SetValue(float value)
    {
        m_Value = value;
        OnValueChanged?.Invoke(value);
    }

    public void Increment(float increment)
    {
        m_Value += increment;
    }

    public static implicit operator float(FloatMono f) => f.Value;
}
