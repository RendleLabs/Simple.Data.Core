namespace Simple.Data.Core.Expressions
{
    public struct EqualsOperator : IOperator
    {
        public static EqualsOperator Instance { get; } = new EqualsOperator();
        public string StringRepresentation => "=";
        public IExpression GetExpression(object left, object right)
        {
            return SimpleExpression.Equal(left, right);
        }
    }

    public struct FloatingOperand
    {
        public FloatingOperand(IOperator @operator, object right)
        {
            Operator = @operator;
            Right = right;
        }

        public IExpression GetExpression(object left)
        {
            return Operator.GetExpression(left, Right);
        }

        public IOperator Operator { get; }
        public object Right { get; }
    }
}