using System;

[Serializable]
public class FloatReference : ReferenceVariable<float>
{
    public bool UseConstant => _UseConstant;
    public float ConstantValue => _ConstantValue;
    public ScriptableVariable<float> Variable => _Variable;
}
