using System.Reflection.Emit;

namespace Simple.Data.Core.Expressions
{
    public struct BinaryExpression<TOperator, TLeft, TRight> : IExpression
        where TOperator: struct, IOperator
    {
        public BinaryExpression(TLeft left, TRight right, TOperator @operator)
        {
            Left = left;
            Right = right;
            Operator = @operator;
        }

        public TLeft Left { get; }

        public TRight Right { get; }

        public TOperator Operator { get; }
    }

    public static class BinaryExpression
    {
        public static BinaryExpression<EqualsOperator, TLeft, TRight> Equals<TLeft, TRight>(TLeft left, TRight right)
            where TLeft: IOperand
            where TRight: IOperand
        {
            return new BinaryExpression<EqualsOperator, TLeft, TRight>(left, right, EqualsOperator.Instance);
        }

        public static BinaryExpression<EqualsOperator, TLeft, ValueOperand<TValue>> Equals<TLeft, TValue>(TLeft left,
            ValueOperand<TValue> right)
        {
            return new BinaryExpression<EqualsOperator, TLeft, ValueOperand<TValue>>(left, right, EqualsOperator.Instance);
        }
    }
}