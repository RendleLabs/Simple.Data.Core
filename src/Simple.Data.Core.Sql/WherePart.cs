using Simple.Data.Core.Expressions;

namespace Simple.Data.Core.Sql
{
    public class WherePart
    {
        public WherePart(Column column, string @operator, IParameter parameter)
        {
            Column = column;
            Operator = @operator;
            Parameter = parameter;
        }

        public Column Column { get; }
        public string Operator { get; }
        public IParameter Parameter { get; }
    }
}