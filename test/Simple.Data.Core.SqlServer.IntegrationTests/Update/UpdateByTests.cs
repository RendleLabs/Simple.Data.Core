using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Simple.Data.Core.SqlServer.IntegrationTests.Update
{
    public class UpdateByTests : TestBase
    {
        private static readonly DateTimeOffset NewTime = new DateTimeOffset(1978, 3, 8, 18, 0, 0, TimeSpan.Zero);
        [Fact]
        public async Task UpdatesUsingNamedParameters()
        {
            var db = await Target();
            var record = await db.UpdateByTest.UpdateById(1, Text: "Pass", Time: NewTime);
            Assert.Equal(1, record.Id);
            Assert.Equal("Pass", record.Text);
            Assert.Equal(NewTime, record.Time);
        }

        protected override string SqlFileName { get; } = "UpdateBy.sql";
    }
}
