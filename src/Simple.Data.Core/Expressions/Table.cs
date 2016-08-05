namespace Simple.Data.Core.Expressions
{
    public struct Table
    {
        public Table(string name, Container container = default(Container))
        {
            Name = name;
            Container = container;
        }

        public string Name { get; }

        public Container Container { get; }
    }

    public struct Container
    {
        public string Name { get; }

        public Container(string name)
        {
            Name = name;
        }
        
    }
}