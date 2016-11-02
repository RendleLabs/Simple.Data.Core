using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple.Data.Core;
using Simple.Data.Core.SqlServer;
using Simple.Data.Core.Postgres;
using Npgsql;

namespace ConsoleSandbox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                TestSqlServer().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static async Task TestSqlServer()
        {
            var db = new SimpleData().Open(@"data source=.\SQLEXPRESS;initial catalog=Simple;integrated security=true",
                typeof(SqlServerAdapter));

            var starship = await db.Starships.GetById(42);

            Console.WriteLine(starship.Name);
        }

        private static async Task TestPostgres()
        {
            try
            {
                using (var cn = new NpgsqlConnection("Host=localhost;Username=postgres;Password=secretsquirrel"))
                {
                    await cn.OpenAsync();

                    using (var cmd = new NpgsqlCommand("CREATE DATABASE simple", cn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    System.Console.WriteLine("Created database");
                }
            } catch {}

            try
            {
                using (var cn = new NpgsqlConnection("Host=localhost;Username=postgres;Password=secretsquirrel;Database=simple"))
                {
                    await cn.OpenAsync();

                    using (var cmd = new NpgsqlCommand("CREATE TABLE Starships (Id integer not null, Name varchar(100) not null)", cn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    using (var cmd = new NpgsqlCommand("INSERT INTO Starships VALUES (42, 'Heart of Gold')", cn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    System.Console.WriteLine("Created table");
                }
            } catch {}
            
            var db = new SimpleData().Open(@"Host=localhost;Username=postgres;Password=secretsquirrel;Database=simple",
                typeof(PostgresAdapter));
            
            var starship = await db.Starships.GetById(42);

            Console.WriteLine(starship.Name);
        }
    }
}
