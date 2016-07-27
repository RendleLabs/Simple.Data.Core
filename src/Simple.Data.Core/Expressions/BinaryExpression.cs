using System.Reflection.Emit;

namespace Simple.Data.Core.Expressions
{
    public struct BinaryExpression<TOperator, TLeft, TRight> : IExpression
        where TOperator: struct, IOperator
    {
        public BinaryExpression(TLeft left, TRight right)
        {
            
        }
    }

    public static class BinaryExpression
    {
        public static BinaryExpression<EqualsOperator, TLeft, TRight> Equals<TLeft, TRight>(TLeft left, TRight right)
            where TLeft: IOperand
            where TRight: IOperand
        {
            return new BinaryExpression<EqualsOperator, TLeft, TRight>(left, right);
        }
    }
}