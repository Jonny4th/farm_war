using System;
using UnityEngine;

public abstract class ReferenceVariable<T> : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    private string _Name;
#endif

    [SerializeField]
    protected bool _UseConstant;

    [SerializeField]
    protected T _ConstantValue;

    [SerializeField]
    protected ScriptableVariable<T> _Variable;

    public event Action<T> OnVariableChanged;

    public virtual T Value
    {
        get
        {
            if(_UseConstant) { return _ConstantValue; }
            else return _Variable.Value;
        }

        set
        {
            if(_UseConstant) _ConstantValue = value;
            else _Variable.SetValue(value);
            OnVariableChanged?.Invoke(value);
        }
    }

    public static implicit operator T(ReferenceVariable<T> variable) => variable.Value;
}
