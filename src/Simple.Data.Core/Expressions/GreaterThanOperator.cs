namespace Simple.Data.Core.Expressions
{
    public struct GreaterThanOperator : IOperator
    {
        public static GreaterThanOperator Instance { get; } = new GreaterThanOperator();
        public string StringRepresentation => "=";
        public IExpression GetExpression(object left, object right)
        {
            return SimpleExpression.GreaterThan(left, right);
        }
    }
}