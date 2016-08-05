namespace Simple.Data.Core
{
    public struct Database
    {
        public static dynamic Open(string name)
        {
            return new Thing(name, new Wrangler());
        }
    }
}