namespace Simple.Data.Core
{
    public static class Database
    {
        public static dynamic Open(string name)
        {
            return new Thing(name, new Wrangler());
        }
    }
}