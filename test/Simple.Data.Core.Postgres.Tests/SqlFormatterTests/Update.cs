using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Simple.Data.Core.Expressions;
using Simple.Data.Core.Sql.Insert;
using Xunit;

namespace Simple.Data.Core.Postgres.Tests.SqlFormatterTests
{
    public class Update
    {
        private static readonly DataContext Context = new DataContext();
        [Fact]
        public void InsertAddsReturningClause()
        {
            var data = ImmutableDictionary<string, object>.Empty.Add("Name", "Heart of Gold");
            var insertStatement = InsertStatement.Create(new Table("Starship", null), data, Context);
            var actual = Postgres.SqlFormatter.FormatInsert(insertStatement);
            const string expected = @"INSERT INTO Starship (Name) VALUES (@Name) RETURNING *";
            Assert.Equal(expected, actual);
        }
    }
}
