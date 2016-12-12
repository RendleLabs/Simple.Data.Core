namespace Simple.Data.Core.Expressions
{
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