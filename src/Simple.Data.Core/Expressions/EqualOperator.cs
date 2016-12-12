namespace Simple.Data.Core.Expressions
{
    public struct EqualOperator : IOperator
    {
        public static EqualOperator Instance { get; } = new EqualOperator();
        public string StringRepresentation => "=";
        public IExpression GetExpression(object left, object right)
        {
            return SimpleExpression.Equal(left, right);
        }
    }
}