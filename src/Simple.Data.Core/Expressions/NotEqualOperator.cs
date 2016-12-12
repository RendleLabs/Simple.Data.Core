namespace Simple.Data.Core.Expressions
{
    public struct NotEqualOperator : IOperator
    {
        public static NotEqualOperator Instance { get; } = new NotEqualOperator();
        public string StringRepresentation => "=";
        public IExpression GetExpression(object left, object right)
        {
            return SimpleExpression.NotEqual(left, right);
        }
    }
}