using System;
using System.Reflection;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core.Sql
{
    public class CriteriaHelper
    {
        private static readonly Type FuncT1TReturn = typeof(Func<,>);
        public WherePart ToWherePart<TExpression>(TExpression expression)
            where TExpression: IExpression
        {
            var actualType = expression.GetType();
            var typeInfo = actualType.GetTypeInfo();
            if (typeInfo.IsGenericType)
            {
                if (typeInfo.GetGenericTypeDefinition() == typeof(BinaryExpression<,,>))
                {
                    var genericTypes = typeInfo.GenericTypeArguments;
                    var rightTypeInfo = genericTypes[2].GetTypeInfo();
                    if (rightTypeInfo.IsGenericType &&
                        rightTypeInfo.GetGenericTypeDefinition() == typeof(ValueOperand<>))
                    {
                        var rightOperandType = rightTypeInfo.GenericTypeArguments[0];
                        var methodInfo = typeof(CriteriaHelper).GetTypeInfo().GetMethod("BinaryExpressionToWherePart");
                        var method = methodInfo.MakeGenericMethod(genericTypes[0], rightOperandType);
                        var funcType = FuncT1TReturn.MakeGenericType(typeof(TExpression), typeof(WherePart));
                        var func = method.CreateDelegate(funcType);
                        return (WherePart) func.DynamicInvoke(expression);
                    }

                    method.Invoke(this, expression);
                }
            }
        }

        public WherePart BinaryExpressionToWherePart<TOperator, TValue>(BinaryExpression<TOperator, Column, ValueOperand<TValue>>  expression)
            where TOperator : struct, IOperator
        {
            return new WherePart(expression.Left, Format(expression.Operator), CreateParameter(expression.Right, expression.Left.Name));
        }

        public string Format(IOperator @operator)
        {
            if (@operator is EqualsOperator)
            {
                return "=";
            }
            throw new InvalidOperationException();
        }

        public Parameter<TValue> CreateParameter<TValue>(ValueOperand<TValue> operand, string nameHint)
        {
            return new Parameter<TValue>(nameHint, operand.Value);
        }
    }
}