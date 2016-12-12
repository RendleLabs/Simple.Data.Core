namespace Simple.Data.Core.Expressions
{
    public interface IExpression
    {
        
    }

    public interface IBinaryExpression
    {
        object Left { get; }
        object Right { get; }
    }
}