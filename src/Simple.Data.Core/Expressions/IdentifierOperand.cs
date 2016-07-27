namespace Simple.Data.Core.Expressions
{
    public struct IdentifierOperand : IOperand
    {
        public string Identifier { get; }

        public IdentifierOperand(string identifier)
        {
            Identifier = identifier;
        }

        public string StringRepresentation => Identifier;
    }
}