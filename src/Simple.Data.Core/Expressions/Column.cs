namespace Simple.Data.Core.Expressions
{
    public struct Column : IOperand
    {
        public string Name { get; }
        public Table Table { get; }

        public Column(string name, Table table)
        {
            Name = name;
            Table = table;
        }

        public string StringRepresentation => $"{Table.Name}.{Name}";
    }
}