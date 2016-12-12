namespace Simple.Data.Core.Expressions
{
    public struct LessThanOrEqualOperator : IOperator
    {
        public static LessThanOrEqualOperator Instance { get; } = new LessThanOrEqualOperator();
        public string StringRepresentation => "=";
        public IExpression GetExpression(object left, object right)
        {
            return SimpleExpression.LessThan(left, right);
        }
    }
}