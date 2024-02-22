using UnityEngine;

public abstract class ScriptableVariable<T> : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    [SerializeField]
    protected T _Value;
    public T Value => _Value;

    public void SetValue(T value)
    {
        _Value = value;
    }

    public static implicit operator T(ScriptableVariable<T> variable) => variable._Value;
}
