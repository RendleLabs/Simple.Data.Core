using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple.Data.Core.Expressions;
using Simple.Data.Core.Sql;
using Xunit;

namespace Simple.Data.Core.SqlServer.Tests
{
    public class SqlFormatterTests
    {
        [Fact]
        public void CreatesWherePartFromEqualsExpression()
        {
            var criteriaHelper = new CriteriaHelper();
            var criteria = SimpleExpression.Equal(new Column("Name", new Table("Spaceship", new Container("db"))), "Heart of Gold");
            var wherePart = criteriaHelper.ToWherePart(criteria);

            var actual = SqlFormatter.FormatWherePart(wherePart);

            Assert.Equal("[Spaceship].[Name] = @Name", actual);
        }
    }
}
