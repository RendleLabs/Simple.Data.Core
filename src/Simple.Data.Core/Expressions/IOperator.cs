namespace Simple.Data.Core.Expressions
{
    public interface IOperator
    {
        string StringRepresentation { get; }
        IExpression GetExpression(object left, object right);
    }
}