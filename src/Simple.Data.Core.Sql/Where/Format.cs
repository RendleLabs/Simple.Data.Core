namespace Simple.Data.Core.Sql.Where
{
    public class Format
    {
        public string WherePart(WherePart part)
        {
            return $"{string.Join(".", part.Column.QualifiedName)} {part.Operator} {part.Parameter.Name}";
        }
    }
}