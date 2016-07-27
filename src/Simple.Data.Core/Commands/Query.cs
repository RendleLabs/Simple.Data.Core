using System.Collections.Generic;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core.Commands
{
    public class Query
    {
        private readonly Table _table;
        private readonly IExpression _criteria;
        private readonly bool _single;

        public Query(Table table, IExpression criteria, bool single = false)
        {
            _table = table;
            _criteria = criteria;
            _single = single;
        }
    }
}