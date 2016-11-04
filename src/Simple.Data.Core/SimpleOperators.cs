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
            return new FloatingOperand(EqualsOperator.Instance, value);
        }

        public static FloatingOperand NotEqual<T>(T value)
        {
            return new FloatingOperand(EqualsOperator.Instance, value);
        }
    }
}
