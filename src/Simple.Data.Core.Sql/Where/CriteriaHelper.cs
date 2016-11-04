using System;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core.Sql.Where
{
    public class CriteriaHelper
    {
        public WherePart ToWherePart(IExpression expression)
        {
            var equals = expression as EqualsExpression;
            if (equals != null)
            {
                return EqualsExpressionToWherePart(equals);
            }
            throw new InvalidOperationException();
        }

        public WherePart EqualsExpressionToWherePart(EqualsExpression expression)
        {
            var column = expression.Left as Column;
            if (column != null)
            {
                return new WherePart(column, "=", CreateParameter(expression.Right, column.Name));
            }
            throw new InvalidOperationException();
        }

        public string Format(IOperator @operator)
        {
            if (@operator is EqualsOperator)
            {
                return "=";
            }
            throw new InvalidOperationException();
        }

        public Parameter CreateParameter(object operand, string nameHint)
        {
            return new Parameter(nameHint, operand);
        }
    }
}