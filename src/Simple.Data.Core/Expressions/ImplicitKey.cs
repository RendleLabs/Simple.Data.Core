namespace Simple.Data.Core.Expressions
{
    public struct ImplicitKey : IOperand
    {
        public Table Table { get; }

        public ImplicitKey(Table table)
        {
            Table = table;
        }

        public string StringRepresentation => "ImplicitKey";
    }
}