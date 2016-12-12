namespace Simple.Data.Core.Expressions
{
    public struct GreaterThanOrEqualOperator : IOperator
    {
        public static GreaterThanOrEqualOperator Instance { get; } = new GreaterThanOrEqualOperator();
        public string StringRepresentation => "=";
        public IExpression GetExpression(object left, object right)
        {
            return SimpleExpression.GreaterThanOrEqual(left, right);
        }
    }
}