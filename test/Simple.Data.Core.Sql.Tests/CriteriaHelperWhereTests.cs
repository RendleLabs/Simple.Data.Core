using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple.Data.Core.Expressions;
using Xunit;

namespace Simple.Data.Core.Sql.Tests
{
    public class CriteriaHelperWhereTests
    {
        [Fact]
        public void CreatesWherePartFromEqualsExpression()
        {
            var criteriaHelper = new CriteriaHelper();
            var criteria = new EqualsExpression(new Column("Name", new Table("Spaceship")), "Heart of Gold");
            var wherePart = criteriaHelper.ToWherePart(criteria);
            Assert.Equal("Spaceship.Name", $"{wherePart.Column.Table.Name}.{wherePart.Column.Name}");
        }
    }
}
