using System;

[Serializable]
public class WandSpellOverride
{
    public enum OperationType
    {
        Add,
        Multiply,
        Set
    }
    public string PropertyName;
    public OperationType Operation;
    public double Value;
}
