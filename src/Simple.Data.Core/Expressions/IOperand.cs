namespace Simple.Data.Core.Expressions
{
    public interface IOperand
    {
        string StringRepresentation { get; }
    }

    public struct ValueOperand<T> : IOperand
    {
        public T Value { get; }

        public ValueOperand(T value)
        {
            Value = value;
        }

        public string StringRepresentation => Value.ToString();
    }
}