using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core
{
    public static class SimpleOperators
    {
        public static FloatingOperand Equal<T>(T value)
        {
            return new FloatingOperand(EqualOperator.Instance, value);
        }

        public static FloatingOperand NotEqual<T>(T value)
        {
            return new FloatingOperand(NotEqualOperator.Instance, value);
        }

        public static FloatingOperand GreaterThan<T>(T value)
        {
            return new FloatingOperand(GreaterThanOperator.Instance, value);
        }

        public static FloatingOperand GreaterThanOrEqual<T>(T value)
        {
            return new FloatingOperand(GreaterThanOrEqualOperator.Instance, value);
        }

        public static FloatingOperand LessThan<T>(T value)
        {
            return new FloatingOperand(LessThanOperator.Instance, value);
        }

        public static FloatingOperand LessThanOrEqual<T>(T value)
        {
            return new FloatingOperand(LessThanOrEqualOperator.Instance, value);
        }
    }
}
