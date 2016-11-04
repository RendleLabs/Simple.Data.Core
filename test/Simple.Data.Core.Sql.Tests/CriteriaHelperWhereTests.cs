using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple.Data.Core.Expressions;
using Simple.Data.Core.Sql.Where;
using Xunit;

namespace Simple.Data.Core.Sql.Tests
{
    public class CriteriaHelperWhereTests
    {
        [Fact]
        public void CreatesWherePartFromEqualsExpression()
        {
            var criteriaHelper = new CriteriaHelper();
            var criteria = SimpleExpression.Equal(new Column("Name", new Table("Spaceship", new Container("db"))), "Heart of Gold");
            var wherePart = criteriaHelper.ToWherePart(criteria);
            Assert.Equal("Spaceship.Name", $"{wherePart.Column.Table.Name}.{wherePart.Column.Name}");
        }
    }
}
