namespace Simple.Data.Core.Expressions
{
    public struct EqualsOperator : IOperator
    {
        public static EqualsOperator Instance { get; } = new EqualsOperator();
        public string StringRepresentation => "=";
    }
}