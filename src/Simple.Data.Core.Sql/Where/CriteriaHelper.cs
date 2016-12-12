using System;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core.Sql.Where
{
    public class CriteriaHelper
    {
        public WherePart ToWherePart(IExpression expression)
        {
            var expr = EqualExpressionToWherePart(expression as EqualExpression)
                       ?? NotEqualExpressionToWherePart(expression as NotEqualExpression)
                       ?? GreaterThanExpressionToWherePart(expression as GreaterThanExpression)
                       ?? GreaterThanOrEqualExpressionToWherePart(expression as GreaterThanOrEqualExpression)
                       ?? LessThanExpressionToWherePart(expression as LessThanExpression)
                       ?? LessThanOrEqualExpressionToWherePart(expression as LessThanOrEqualExpression);

            if (expr != null)
            {
                return expr;
            }
            throw new InvalidOperationException();
        }

        public WherePart EqualExpressionToWherePart(EqualExpression expression)
        {
            return BinaryExpressionToWherePart(expression, "=");
        }

        public WherePart NotEqualExpressionToWherePart(NotEqualExpression expression)
        {
            return BinaryExpressionToWherePart(expression, "!=");
        }

        public WherePart GreaterThanExpressionToWherePart(GreaterThanExpression expression)
        {
            return BinaryExpressionToWherePart(expression, ">");
        }

        public WherePart GreaterThanOrEqualExpressionToWherePart(GreaterThanOrEqualExpression expression)
        {
            return BinaryExpressionToWherePart(expression, ">=");
        }

        public WherePart LessThanExpressionToWherePart(LessThanExpression expression)
        {
            return BinaryExpressionToWherePart(expression, "<");
        }

        public WherePart LessThanOrEqualExpressionToWherePart(LessThanOrEqualExpression expression)
        {
            return BinaryExpressionToWherePart(expression, "<=");
        }

        private WherePart BinaryExpressionToWherePart<TExpression>(TExpression expression, string @operator)
            where TExpression : IBinaryExpression
        {
            if (expression == null) return null;

            var column = expression.Left as Column;
            if (column != null)
            {
                return new WherePart(column, @operator, CreateParameter(expression.Right, column.Name));
            }
            throw new InvalidOperationException();
        }

        public string Format(IOperator @operator)
        {
            if (@operator is EqualOperator)
            {
                return "=";
            }
            throw new InvalidOperationException();
        }

        public Parameter CreateParameter(object operand, string nameHint)
        {
            return new Parameter(nameHint + "_criteria", operand);
        }
    }
}