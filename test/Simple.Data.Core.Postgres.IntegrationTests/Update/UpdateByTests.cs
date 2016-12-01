using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Simple.Data.Core.Postgres.IntegrationTests.Select
{
    public class UpdateByTests : TestBase
    {
        [Fact]
        public async Task GetsResultSet()
        {
            var db = await Target();
            List<dynamic> records = await db.FindByTest.FindByType("Original").ToList();

            Assert.Equal(2, records.Count);
            Assert.Equal(1, records[0].Id);
            Assert.Equal("Primary Phase", records[0].Text);
            Assert.Equal(new DateTimeOffset(1978, 3, 8, 0, 0, 0, TimeSpan.Zero), records[0].Time);
            Assert.Equal(2, records[1].Id);
            Assert.Equal("Secondary Phase", records[1].Text);
            Assert.Equal(new DateTimeOffset(1978, 12, 24, 0, 0, 0, TimeSpan.Zero), records[1].Time);
        }

        protected override string SqlFileName => "FindBy.sql";
    }
}