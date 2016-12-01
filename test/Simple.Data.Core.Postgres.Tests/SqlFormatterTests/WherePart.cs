using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple.Data.Core.Expressions;
using Simple.Data.Core.Postgres;
using Simple.Data.Core.Sql;
using Simple.Data.Core.Sql.Where;
using Xunit;

namespace Simple.Data.Core.Postgres.Tests.SqlFormatterTests
{
    public class WherePart
    {
        [Fact]
        public void CreatesWherePartFromEqualsExpression()
        {
            var criteriaHelper = new CriteriaHelper();
            var criteria = SimpleExpression.Equal(new Column("Name", new Table("Spaceship", null)), "Heart of Gold");
            var wherePart = criteriaHelper.ToWherePart(criteria);

            var actual = Postgres.SqlFormatter.FormatWherePart(wherePart);

            Assert.Equal("Spaceship.Name = @Name", actual);
        }
    }
}
