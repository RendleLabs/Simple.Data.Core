using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple.Data.Core;
using Simple.Data.Core.SqlServer;

namespace ConsoleSandbox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Test().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }

        private static async Task Test()
        {
            var db = Database.Open(@"data source=.\SQLEXPRESS;initial catalog=Simple;integrated security=true",
                typeof(SqlServerAdapter));
            var starship = await db.Starships.GetById(42);
            Console.WriteLine(starship.Name);
        }
    }
}
