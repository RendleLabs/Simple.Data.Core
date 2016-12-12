namespace Simple.Data.Core.Expressions
{
    public struct LessThanOperator : IOperator
    {
        public static LessThanOperator Instance { get; } = new LessThanOperator();
        public string StringRepresentation => "=";
        public IExpression GetExpression(object left, object right)
        {
            return SimpleExpression.LessThan(left, right);
        }
    }
}