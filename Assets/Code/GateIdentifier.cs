using UnityEngine;

[System.Serializable]
public class GateIdentifier
{
    public Color color;
    public GateOper operation;
    public int factor;

    public GateIdentifier(Color color, GateOper operation, int factor)
    {
        this.color = color;
        this.operation = operation;
        this.factor = factor;
    }

    public override string ToString()
    {
        return operation switch
        {
            GateOper.Add => "+" + factor.ToString(),
            GateOper.Sub => "-" + factor.ToString(),
            GateOper.Multiply => "x" + factor.ToString(),
            GateOper.Divide => "/" + factor.ToString(),
            _=> "+" + factor.ToString(),
        };
    }
}


public enum GateOper
{
    Add,
    Sub,
    Multiply,
    Divide,
}
