namespace Simple.Data.Core.Expressions
{
    public struct ValueOperand<T> : IOperand
    {
        public T Value { get; }

        public ValueOperand(T value)
        {
            Value = value;
        }

        public string StringRepresentation => Value.ToString();

        public static implicit operator ValueOperand<T>(T value)
        {
            return new ValueOperand<T>(value);
        }
    }

    public static class ValueOperand
    {
        public static ValueOperand<T> Create<T>(T value)
        {
            return new ValueOperand<T>(value);
        }
    }
}